using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chimera.Entities.Admin;
using Chimera.DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.Test
{
    [TestClass]
    public class AdminUserTests
    {
        [TestMethod]
        public void AdminUsers_CreateAll()
        {
            List<AdminUserRole> RoleList = AdminUserRoleDAO.LoadAll();

            if (RoleList != null && RoleList.Count > 0)
            {
                foreach (var UserRole in RoleList)
                {
                    var DeleteQuery = Query<AdminUser>.EQ(e => e.Username, UserRole.Name);

                    Execute.Delete<AdminUser>("AdminUsers", DeleteQuery);

                    AdminUser NewUser = new AdminUser();

                    NewUser.Active = true;
                    NewUser.Username = UserRole.Name;
                    NewUser.Hashed_Password = "qwqwqw";
                    NewUser.RoleList.Add(UserRole.Name);

                    AdminUserDAO.Save(NewUser);
                }
            }
        }

        [TestMethod]
        public void AdminUsers_CreateTheClient()
        {
            var DeleteQuery = Query<AdminUser>.EQ(e => e.Username, "theclient");

            Execute.Delete<AdminUser>("AdminUsers", DeleteQuery);

            AdminUser NewUser = new AdminUser();

            NewUser.Active = true;
            NewUser.Username = "theclient";
            NewUser.Hashed_Password = "qwqwqw";

            NewUser.RoleList.Add("page-view");
            NewUser.RoleList.Add("page-edit");
            NewUser.RoleList.Add("product-view");
            NewUser.RoleList.Add("product-edit");
            NewUser.RoleList.Add("setting-view");
            NewUser.RoleList.Add("setting-value-edit");
            NewUser.RoleList.Add("nav-menu-edit");
            NewUser.RoleList.Add("purchase-order-view");
            NewUser.RoleList.Add("purchase-order-edit");

            AdminUserDAO.Save(NewUser);
        }
    }
}
