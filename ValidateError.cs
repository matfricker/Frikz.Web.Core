using System.Web;
using System.Web.UI;

namespace Frikz.Web.Core
{
    public class ValidationError : IValidator
    {
        private ValidationError(string message)
        {
            ErrorMessage = message;
            IsValid = false;
        }

        public string ErrorMessage { get; set; }

        public string ValidationGroup { get; set; }

        public bool IsValid { get; set; }

        public void Validate()
        {
            // no action required
        }

        public static void Display(string message)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            currentPage.Validators.Add(new ValidationError(message));
        }
    }
}