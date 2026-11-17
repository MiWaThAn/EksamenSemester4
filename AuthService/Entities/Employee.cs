using AuthService.Entities.Enums;
using AuthService.Entities.ValueObjects;

namespace AuthService.Entities
{
    public class Employee : User
    {
        public Department Department { get; private set; }
        public Role Role { get; private set; }
        private readonly List<string> _permissions = new();
        public IReadOnlyCollection<string> Permissions => _permissions.AsReadOnly();
        

        protected Employee() { } // For EF Core

        private Employee(Name name, string email, string passwordHashed, bool mfaEnabled, Department department, Role role, List<string> permissions)
            : base(name, email, passwordHashed, mfaEnabled)
        {
            UpdateDepartment(department);
            UpdateRole(role);
            AddPermissions(permissions);
        }

        public static Employee Create(Name name, string email, string passwordHashed, bool mfaEnabled, Department department, Role role, List<string> permissions)
        {
            return new Employee(name, email, passwordHashed, mfaEnabled, department, role, permissions);
        }

        public void UpdateDepartment(Department department)
        {
            Department = department;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateRole(Role role)
        {
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddPermissions(List<string> permissions)
        {
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

        

        public void RemovePermission(List<string> permissions)
        {
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
