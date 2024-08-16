using System;
using webProject.Data;

namespace webProject.Models
{
    public class UserCourseProgress1
    {
        public int UserCourseProgress1Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
