using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Species
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();
}
