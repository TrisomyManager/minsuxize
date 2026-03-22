using System.ComponentModel.DataAnnotations;

namespace MinsuXize.Web.ViewModels;

public sealed class AdminLoginViewModel
{
    [Required(ErrorMessage = "请输入管理员账号")]
    [Display(Name = "管理员账号")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "请输入管理员密码")]
    [DataType(DataType.Password)]
    [Display(Name = "管理员密码")]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}
