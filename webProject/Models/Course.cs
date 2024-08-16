using System;
using System.Collections.Generic;

namespace webProject.Models
{
    public class Course
    {
        public int CourseId { get; set; } 
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Content { get; set; } 
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public ICollection<UserCourseProgress1> UserCourseProgresses { get; set; } = new List<UserCourseProgress1>();
    }
}



