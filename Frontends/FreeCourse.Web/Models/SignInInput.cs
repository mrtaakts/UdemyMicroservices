using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SignInInput
    {
        [Required(ErrorMessage ="Email adresini kontrol ediniz")]
        [Display(Name ="Email adresiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrenizi kontrol ediniz")]
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        
        [Display(Name = "Beni Hatırla")]
        public bool IsRemember { get; set; }
    }
}
