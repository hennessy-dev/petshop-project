using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Breed : BaseEntity
{
    public int SpeciesId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual Species Species { get; set; }
}
