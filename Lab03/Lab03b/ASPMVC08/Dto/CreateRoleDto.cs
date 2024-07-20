using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Dto;

public class CreateRoleDto
{
    [Length(2, 20, ErrorMessage = "Имя должно быть от 2 до 20 символов")]
    public string Name { get; set; }
}
