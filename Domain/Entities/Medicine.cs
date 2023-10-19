using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Medicine : BaseEntity
{

    public int SupplierId { get; set; }

    public string Name { get; set; }

    public int Stock { get; set; }

    public double Price { get; set; }

    public string Laboratory { get; set; }

    public virtual ICollection<Medicaltreatment> Medicaltreatments { get; set; } = new List<Medicaltreatment>();

    public virtual ICollection<Purchasedmedicine> Purchasedmedicines { get; set; } = new List<Purchasedmedicine>();

    public virtual ICollection<Soldmedicine> Soldmedicines { get; set; } = new List<Soldmedicine>();

    public virtual Supplier Supplier { get; set; }
}
