using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace assignement_3.PL.dto
{
    public class SignInDtocs
    {
        [Required(ErrorMessage = "Email is Required ! !")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required ! !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "agree policy is Required ! !")]

        public bool Remember { get; set; }
        public string? ReturnUrl { get; set; }
        // AuthenticationScheme is in Microsoft.AspNetCore.Authentication namespace
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
    }
}
