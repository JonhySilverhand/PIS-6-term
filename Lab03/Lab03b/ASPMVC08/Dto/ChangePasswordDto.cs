using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Dto;

public class ChangePasswordDto
{
    public string Password { get; set; }

    [Length(7, 64, ErrorMessage = "Длина пароля должна быть от 7 до 64 символов")]
    public string NewPassword { get; set; }
}
