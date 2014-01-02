using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public partial class TaiKhoan
    {
        public string MatKhau { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Nhớ mật khẩu")]
        public bool NhoMatKhau { get; set; }
    }

    public class UserMembershipModel
    {
        public string username { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }    
}