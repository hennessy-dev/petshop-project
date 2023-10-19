using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Owner : BaseEntity
{

    public string Name { get; set; }

    public string Telephone { get; set; }

    public string Email { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
