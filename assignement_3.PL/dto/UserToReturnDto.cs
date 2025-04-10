using System.ComponentModel.DataAnnotations;

namespace assignement_3.PL.dto
{
    public class UserToReturnDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "UserName is Required ! !")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is Required ! !")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required ! !")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required ! !")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is Required ! !")]
        [RegularExpression("^1[0-2]\\d{1,8}$", ErrorMessage = "add phone number in from 1XXXXXXXXX example 1020304050")]
        public string? PhonNumber { get; set; }
        public string? Role { get; set; }


        public IEnumerable<string>? Roles { get; set; }
    }
}
