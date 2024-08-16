using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webProject.Pages
{

    public class compileModel : PageModel
    {
        [HttpPost]
        public IActionResult OnPostCompile([FromBody] CompileRequest request)
        {
            // Access the input code from the request
            string inputCode = request.Code;

            // Process the input code (compile and run)

            // For demonstration, echoing the input code back as output
            string output = "This is the output of the compiled code:\n" + inputCode;

            // Return the output with status code 200 (OK)
            return Content(output);
        }


        public class CompileRequest
        {
            public string Code { get; set; }
        }

        public void OnGet()
        {

        }

    }
    
}
