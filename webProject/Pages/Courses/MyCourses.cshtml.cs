using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class MyCoursesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyCoursesModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Course> Courses { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            Courses = await _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Course)
                .ToListAsync();
        }
    }
}
