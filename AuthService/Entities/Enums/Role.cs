namespace AuthService.Entities.Enums
{
    public enum Role
    {
        SuperAdmin, //This role has all permissions, including the ability to manage other admins.
        Admin, //This role has permissions to manage employees, but not other admins.
        Employee, //This role has limited permissions defined by admin or superadmin.
    }
}
