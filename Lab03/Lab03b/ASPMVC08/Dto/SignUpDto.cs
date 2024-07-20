using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Dto;

public class SignUpDto
{
    [Length(2, 20, ErrorMessage = "Имя должно быть от 2 до 20 символов")]
    public string Username { get; set; }

    [Length(7, 64, ErrorMessage = "Пароль должен быть от 7 до 64 символов")]
    public string Password { get; set; }

    public override string ToString() => $"Name: {Username}";
}
