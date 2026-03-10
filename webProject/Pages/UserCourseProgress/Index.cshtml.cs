using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webProject.Data;
using webProject.Models;

namespace webProject.Pages.UserCourseProgress
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserCourseProgress1> UserCourseProgress1s { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            UserCourseProgress1s = await _context.UserCourseProgress1s
                .Include(ucp => ucp.Course)
                .Where(ucp => ucp.UserId == userId)
                .ToListAsync();
        }
    }
}
