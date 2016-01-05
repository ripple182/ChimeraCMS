using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChimeraWebsite.Areas.Admin.Attributes;
using MongoDB.Bson;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.UI;
using System.IO;
using CompanyCommons.FileManagement;
using Chimera.Entities.Uploads;
using Chimera.DataAccess;
using Chimera.Entities.Property;
using Chimera.Entities.Admin.Role;
using CM = System.Configuration.ConfigurationManager;
using CCAWS = CompanyCommons.AWS.S3.Execute;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class UploadController : ApiController
    {
        /// <summary>
        /// From the edit product page upload a new property key to be added to the static property list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminUserAccess]
        public HttpResponseMessage StaticProperty(string key, string name)
        {
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.StatusCode = 200;

            try
            {
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(name))
                {
                    StaticProperty CurrentStaticProperty = StaticPropertyDAO.LoadByKeyName(key);

                    string PropertyAlreadyExists = CurrentStaticProperty.PropertyNameValues.Where(e => e.ToUpper().Equals(name.ToUpper())).FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(PropertyAlreadyExists))
                    {
                        CurrentStaticProperty.PropertyNameValues.Add(name);

                        if (StaticPropertyDAO.Save(CurrentStaticProperty))
                        {
                            HttpContext.Current.Response.Write(name);
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("Property Already Exists");

                    }
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Admin.UploadController.StaticProperty(): ", e);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        //TODO: Reimplement whenever ready for real admin mode edit
        [AdminUserAccess(Admin_User_Roles = PageRoles.EDIT)]
        public HttpResponseMessage ColorHex(string colorHex)
        {
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.StatusCode = 200;

            try
            {
                ColorHex ColorHex = new ColorHex(colorHex);

                if (ColorHexDAO.Save(ColorHex))
                {
                    HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(ColorHex));
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Admin.UploadController.ColorHex(): ", e);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [HttpPost]
        //TODO: Reimplement whenever ready for real admin mode edit
        [AdminUserAccess]
        public HttpResponseMessage Image()
        {
            try
            {
                string origFileName = !string.IsNullOrWhiteSpace(HttpContext.Current.Request["origFileName"]) ? HttpContext.Current.Request["origFileName"] : string.Empty;

                //if true then the file upload is done and we can rename the file.
                if (!string.IsNullOrWhiteSpace(origFileName))
                {
                    string OriginalFileExtension = Path.GetExtension(origFileName);

                    string NewFileName = Guid.NewGuid().ToString().ToUpper() + OriginalFileExtension;

                    string FullAmazonS3URL = CM.AppSettings["AWSUrl"] + CM.AppSettings["AWSBucketName"] + "/" + CM.AppSettings["AWSBucketFolderPath"];

                    CCAWS.Rename.RenameFile(origFileName, NewFileName);

                    CCAWS.ImageUtil.CopyAndResizeImage(NewFileName, NewFileName.Replace(OriginalFileExtension, "") + "thumb" + OriginalFileExtension, 75, 75);
                    
                    Chimera.Entities.Uploads.Image Image = new Chimera.Entities.Uploads.Image();

                    Image.Id = ObjectId.GenerateNewId();
                    Image.ModifiedDateUTC = DateTime.UtcNow;
                    Image.FileName = System.IO.Path.GetFileNameWithoutExtension(NewFileName);
                    Image.FileExtension = OriginalFileExtension;
                    Image.Url = FullAmazonS3URL;

                    if (Chimera.DataAccess.ImageDAO.Save(Image))
                    {
                        HttpContext.Current.Response.ContentType = "text/plain";

                        HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Image));
                        HttpContext.Current.Response.StatusCode = 200;

                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                //else continue saving the data
                else
                {

                    HttpPostedFile file = HttpContext.Current.Request.Files[0];

                    CCAWS.Upload.UploadFile(file.InputStream, file.FileName);

                    HttpContext.Current.Response.ContentType = "text/plain";
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var result = new { name = file.FileName };

                    HttpContext.Current.Response.Write(serializer.Serialize(result));
                    HttpContext.Current.Response.StatusCode = 200;

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Admin.UploadController.Image(): ", e);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
