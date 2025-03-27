using System.ComponentModel.DataAnnotations;

namespace assignement_3.PL.dto
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required ! !")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
