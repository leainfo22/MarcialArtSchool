using System.ComponentModel.DataAnnotations;
namespace Account.DTO.LoginDTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Email cant't be blank")]
    [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
    [DataType(DataType.EmailAddress)]
    public string? Username { get; set; }


    [Required(ErrorMessage = "Password can't be blank")]
    [DataType(DataType.Password)]
    public string? UserPassword { get; set; }

}

