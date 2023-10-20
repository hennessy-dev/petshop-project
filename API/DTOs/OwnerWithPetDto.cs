namespace API.DTOs;
public class OwnerWithPetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public ICollection<PetDto> Pets { get; set; }
}