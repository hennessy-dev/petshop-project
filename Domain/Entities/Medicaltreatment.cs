using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Medicaltreatment : BaseEntity
{

    public int AppointmentId { get; set; }

    public int MedicineId { get; set; }

    public int Dosage { get; set; }

    public DateTime AdministrationDate { get; set; }

    public string Comment { get; set; }

    public virtual Appointment Appointment { get; set; }

    public virtual Medicine Medicine { get; set; }
}
