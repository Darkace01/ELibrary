using Microsoft.AspNetCore.Identity;

namespace ELibrary.Core
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {

        }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
