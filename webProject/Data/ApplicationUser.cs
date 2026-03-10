using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace webProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserCourseProgress1> UserCourseProgress1s { get; set; }
    }
}
