using AuthService.Entities.Enums;
namespace AuthService.Entities
{
    public class Employee : User
    {
        public Department Department { get; private set; }
        private readonly List<string> _permissions = new();
        public IReadOnlyCollection<string> Permissions => _permissions.AsReadOnly();
        

        protected Employee() { } // For EF Core

        private Employee(string firstName, string lastName, string email, string passwordHashed, bool mfaEnabled, Role role, Department department, List<string>? permissions = null)
            : base(firstName, lastName, email, passwordHashed, mfaEnabled, role)
        {
            UpdateDepartment(department);
            if (permissions is { Count: > 0 })
            {
                AddPermissions(permissions);
            }
        }

        public static Employee Create(string firstName, string lastName, string email, string passwordHashed, bool mfaEnabled, Role role, Department department, List<string>? permissions = null)
        {
            return new Employee(firstName, lastName, email, passwordHashed, mfaEnabled, role, department, permissions);
        }

        public void UpdateDepartment(Department department)
        {
            Department = department;
            UpdatedAt = DateTime.UtcNow;
        }

        public void PromoteOrDemote(Role newRole, Role updaterRole)
        {

            if (updaterRole != Role.Admin && updaterRole != Role.SuperAdmin)
            {
                throw new UnauthorizedAccessException("Kun administratorer kan ændre medarbejderroller.");
            }

            
            if (newRole == Role.SuperAdmin && updaterRole != Role.SuperAdmin)
            {
                throw new UnauthorizedAccessException("Kun en SuperAdmin kan uddele SuperAdminroller.");
            }


            Role = newRole;
            UpdatedAt = DateTime.UtcNow;
        }


        public void AddPermissions(IEnumerable<string>? permissions)
        {
            if (permissions is null)
                return;

            foreach (var permission in permissions)
            {
                if (string.IsNullOrWhiteSpace(permission))
                    throw new ArgumentException("Permission cannot be empty.", nameof(permission));

                if (!_permissions.Contains(permission))
                {
                    _permissions.Add(permission);
                }
            }
            UpdatedAt = DateTime.UtcNow;
        }

        

        public void RemovePermission(IEnumerable<string>? permissions)
        {

            if (permissions is null)
                return;

            foreach (var permission in permissions)
            {
                if (string.IsNullOrWhiteSpace(permission))
                    throw new ArgumentException("Permission cannot be empty.", nameof(permission));
                if (_permissions.Contains(permission))
                {
                    _permissions.Remove(permission);
                }
                
            }
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
