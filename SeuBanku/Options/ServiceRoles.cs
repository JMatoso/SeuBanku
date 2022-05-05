#nullable disable

using Microsoft.AspNetCore.Identity;

namespace SeuBanku.Options
{
    public enum ServiceRolesEnumerator
    {
        ROLE_ADMIN = 1,
        ROLE_CLIENT = 2
    }

    public static class ServiceRoles
    {
        public static string[] ROLES
        {
            get
            {
                var arrRoles = Enum.GetValues(typeof(ServiceRolesEnumerator));
                var roles = new string[arrRoles.Length];

                int i = 0;

                foreach (var role in arrRoles)
                {
                    roles[i] = role.ToString();
                    i++;
                }

                return roles;
            }
        }

        public static List<IdentityRole> GetIdentityRoles()
        {
            var roles = new List<IdentityRole>();

            foreach (var role in ROLES)
            {
                roles.Add(new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }

            return roles;
        }
    }
}
