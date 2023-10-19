using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Appointment : BaseEntity
{
    public int PetId { get; set; }

    public int VeterinarianId { get; set; }

    public DateTime Date { get; set; }

    public string Reason { get; set; }

    public virtual ICollection<Medicaltreatment> Medicaltreatments { get; set; } = new List<Medicaltreatment>();

    public virtual Pet Pet { get; set; }

    public virtual Veterinarian Veterinarian { get; set; }
}
