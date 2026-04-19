using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace webProject.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int StatusCode { get; set; }
        public string ErrorTitle { get; set; } = "Something went wrong";
        public string ErrorMessage { get; set; } = "An unexpected error occurred. Please try again.";
        public string ErrorIcon { get; set; } = "warning";

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int? id)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            // Status code passed via the route: /Error/{statusCode}
            StatusCode = id ?? HttpContext.Response.StatusCode;

            // Also check the exception handler feature for 500s
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            switch (StatusCode)
            {
                case 404:
                    ErrorTitle = "Page not found";
                    ErrorMessage = "The page you're looking for doesn't exist or has been moved.";
                    ErrorIcon = "notfound";
                    break;
                case 403:
                    ErrorTitle = "Access denied";
                    ErrorMessage = "You don't have permission to access this resource.";
                    ErrorIcon = "lock";
                    break;
                case 401:
                    ErrorTitle = "Unauthorized";
                    ErrorMessage = "You need to sign in to access this page.";
                    ErrorIcon = "lock";
                    break;
                case 400:
                    ErrorTitle = "Bad request";
                    ErrorMessage = "The request couldn't be understood. Please check your input and try again.";
                    ErrorIcon = "warning";
                    break;
                default:
                    ErrorTitle = "Something went wrong";
                    ErrorMessage = "An unexpected error occurred on our end. Please try again in a moment.";
                    ErrorIcon = "warning";
                    if (exceptionFeature?.Error != null)
                        _logger.LogError(exceptionFeature.Error, "Unhandled exception");
                    break;
            }
        }
    }
}
