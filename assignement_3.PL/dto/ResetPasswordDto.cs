using System.ComponentModel.DataAnnotations;

namespace assignement_3.PL.dto
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is Required ! !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required ! !")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword dosenot match the password ! !")]
        public string ConfirmPassword { get; set; }
    }
}
