using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage ="Email không được để trống!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password không được để trống!")]
        public string Password { get; set; }
    }
}
