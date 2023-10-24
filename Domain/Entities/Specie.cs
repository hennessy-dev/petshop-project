using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Specie : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();
}
