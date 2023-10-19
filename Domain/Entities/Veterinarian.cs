using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Veterinarian
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Telephone { get; set; }

    public string Speciality { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
