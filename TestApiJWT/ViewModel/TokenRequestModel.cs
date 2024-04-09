using System.ComponentModel.DataAnnotations;

namespace TestApiJWT.ViewModel
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
