using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace smnetcoreseed.core.Extensions.Identity
{
    public static class CoreApplicationPermissions
    {
        public static ReadOnlyCollection<CoreApplicationPermission> AllPermissions;

        public const string UsersPermissionGroupName = "User Permissions";
        public static CoreApplicationPermission ViewUsers = new CoreApplicationPermission("View Users", "users.view", UsersPermissionGroupName, "Permission to view other users account details");
        public static CoreApplicationPermission ManageUsers = new CoreApplicationPermission("Manage Users", "users.manage", UsersPermissionGroupName, "Permission to create, delete and modify other users account details");

        public const string RolesPermissionGroupName = "Role Permissions";
        public static CoreApplicationPermission ViewRoles = new CoreApplicationPermission("View Roles", "roles.view", RolesPermissionGroupName, "Permission to view available roles");
        public static CoreApplicationPermission ManageRoles = new CoreApplicationPermission("Manage Roles", "roles.manage", RolesPermissionGroupName, "Permission to create, delete and modify roles");
        public static CoreApplicationPermission AssignRoles = new CoreApplicationPermission("Assign Roles", "roles.assign", RolesPermissionGroupName, "Permission to assign roles to users");

        static CoreApplicationPermissions()
        {
            List<CoreApplicationPermission> allPermissions = new List<CoreApplicationPermission>()
            {
                ViewUsers,
                ManageUsers,

                ViewRoles,
                ManageRoles,
                AssignRoles
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static CoreApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
        }

        public static CoreApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).FirstOrDefault();
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        public static string[] GetAdministrativePermissionValues()
        {
            return new string[] { ManageUsers, ManageRoles, AssignRoles };
        }
    }

    public class CoreApplicationPermission
    {
        public CoreApplicationPermission()
        { }

        public CoreApplicationPermission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(CoreApplicationPermission permission)
        {
            return permission.Value;
        }
    }
}