using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using webProject.Data;
using webProject.Models;
using Microsoft.AspNetCore.Authorization;


namespace webProject.Pages.Courses
{

    [Authorize(Policy = "RequireAdminRole")]
    public class CourseCreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly HtmlSanitizer _htmlSanitizer;

        public CourseCreateModel(ApplicationDbContext context)
        {
            _context = context;
            _htmlSanitizer = new HtmlSanitizer();
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Sanitize the content before saving
            Course.Content = _htmlSanitizer.Sanitize(Course.Content);

            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}


