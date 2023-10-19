using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Pet : BaseEntity
{

    public int OwnerId { get; set; }

    public int BreedId { get; set; }

    public string Name { get; set; }

    public DateTime BirthDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Breed Breed { get; set; }

    public virtual Owner Owner { get; set; }
}
