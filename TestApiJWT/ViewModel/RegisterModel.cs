using System.ComponentModel.DataAnnotations;

namespace TestApiJWT.ViewModel
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string UserName { get; set; }
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }


        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(265)]
        public string Password { get; set; }


    }
}
