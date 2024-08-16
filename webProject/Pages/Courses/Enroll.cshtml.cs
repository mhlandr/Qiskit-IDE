using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webProject.Data;
using webProject.Models;
using Microsoft.AspNetCore.Authorization;



namespace webProject.Pages.Courses
{
    [Authorize]
    public class EnrollModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Courses = new List<Course>(); // Initialize Courses to avoid null reference
        }

        public IList<Course> Courses { get; set; }

        [BindProperty]
        public int CourseId { get; set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Challenge();
            }

            var userCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == CourseId);

            if (userCourse != null)
            {
                ModelState.AddModelError(string.Empty, "You are already enrolled in this course.");
                Courses = await _context.Courses.ToListAsync(); // Re-populate the Courses list
                return Page();
            }

            userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = CourseId
            };

            _context.UserCourses.Add(userCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Courses/MyCourses");
        }
    }
}
