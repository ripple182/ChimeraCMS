using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Chimera.Entities.Report;
using UAParser;
using ChimeraWebsite.Helpers;
using Chimera.DataAccess;
using System.Text.RegularExpressions;

namespace ChimeraWebsite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Parser UAParser = Parser.GetDefault();

            ClientInfo ClientInfo = UAParser.Parse(Request.UserAgent);

            UserSessionInformation UserInfo = new UserSessionInformation();

            UserInfo.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            UserInfo.BrowserNameAndVersion = ClientInfo.UserAgent.Family + " " + ClientInfo.UserAgent.Major;
            UserInfo.OperatingSystem = ClientInfo.OS.Family + " " + ClientInfo.OS.Major;
            UserInfo.SessionId = Guid.NewGuid();

            UserInfo.IsBot = Regex.IsMatch(Request.UserAgent, @"bot|crawler|baiduspider|80legs|ia_archiver|voyager|curl|wget|yahoo! slurp|mediapartners-google", RegexOptions.IgnoreCase);

            SiteContext.UserSessionInfo = UserInfo;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try  
            {
                UserSessionInformation UserInfo = (UserSessionInformation)Session[SiteContext.USER_SESSION_INFO] ?? new UserSessionInformation();

                ReportDAO.UpdateLastPageUserWasOn(AppSettings.AllowPageReportRecording, UserInfo);
            }
            catch (Exception ex)
            {

            }
        }
    }
}