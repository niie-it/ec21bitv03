using System.ComponentModel.DataAnnotations;

namespace MyEStore.Models
{
    public class LoginVM
    {
        [Display(Name ="Tên đăng nhập")]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Display(Name ="Mật khẩu")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
