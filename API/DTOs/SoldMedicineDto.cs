namespace API.DTOs;
public class SoldMedicineDto
{
    public int Id { get; set; }
    public int MedicineId { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public DateTime SoldDate { get; set; }
}