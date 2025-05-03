namespace MarcialArtSchool.DTO.Pupils;

public class Pupils
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserType { get; set; }
    public string? Grade { get; set; }
    public string? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Phone { get; set; }
    public string DNI { get; set; } = string.Empty; // Assuming not nullable based on the image
    public string? Adress { get; set; }
    public string? ProvinceState { get; set; }
    public string? Country { get; set; }
    public string? BodyWeight { get; set; }
    public string? Height { get; set; }
    public string? Pathology { get; set; }
    public string? Judge { get; set; }
    public Guid Id { get; set; } // Assuming not nullable based on the image
}


