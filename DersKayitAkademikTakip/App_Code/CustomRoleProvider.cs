using System;
using System.Web.Security;

namespace DersKayitAkademikTakip
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            // Session'dan rolü al
            if (System.Web.HttpContext.Current.Session["Rol"] != null)
            {
                return new string[] { System.Web.HttpContext.Current.Session["Rol"].ToString() };
            }
            return new string[] { };
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = GetRolesForUser(username);
            foreach (string role in roles)
            {
                if (role.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        // Diğer gerekli metodlar - basit implementasyon
        public override string ApplicationName { get; set; }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames) { }
        public override void CreateRole(string roleName) { }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) { return false; }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch) { return new string[0]; }
        public override string[] GetAllRoles() { return new string[] { "admin", "ogrenci", "hoca" }; }
        public override string[] GetUsersInRole(string roleName) { return new string[0]; }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) { }
        public override bool RoleExists(string roleName) { return true; }
    }
}