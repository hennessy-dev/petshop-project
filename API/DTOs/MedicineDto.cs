namespace API.DTOs;
public class MedicineDto
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int MedicineId { get; set; }
    public int Dosage { get; set; }
    public DateTime AdministrationDate { get; set; }
    public string Comment { get; set; }
}