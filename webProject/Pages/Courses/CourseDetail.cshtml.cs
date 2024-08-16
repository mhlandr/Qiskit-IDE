using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using webProject.Data;
using webProject.Models;

namespace webProject.Pages.Courses
{
    public class CourseDetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CourseDetailModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
