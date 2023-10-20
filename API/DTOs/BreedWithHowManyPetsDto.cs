namespace API.DTOs;
public class BreedWithManyPetsDto
{
    public BreedDto Breed { get; set; }
    public IEnumerable<int> PetIds { get; set; }
    public int Total { get; set; }
}