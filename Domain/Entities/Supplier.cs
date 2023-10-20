using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Supplier : BaseEntity
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string Telephone { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual ICollection<Purchasedmedicine> Purchasedmedicines { get; set; } = new List<Purchasedmedicine>();
}
