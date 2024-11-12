namespace BL.Users.Entitiy;

public class FilterUserModel
{
    public string? LoginPart { get; set; }
    public string? SurnamePart { get; set; }
    public string? MailPart { get; set; }
    
    public DateTime? DateOfBirthPart { get; set; }
    
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    public int? Role { get; set; }
}