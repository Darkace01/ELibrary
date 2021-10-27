using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Core
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Users fullname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Users matric no
        /// </summary>
        public string? MatricNo { get; set; }
    }
}
