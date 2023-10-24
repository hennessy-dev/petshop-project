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

    public virtual ICollection<MedicalTreatment> MedicalTreatments { get; set; } = new List<MedicalTreatment>();

    public virtual ICollection<PurchasedMedicine> PurchasedMedicines { get; set; } = new List<PurchasedMedicine>();

    public virtual ICollection<SoldMedicine> SoldMedicines { get; set; } = new List<SoldMedicine>();

    public virtual Supplier Supplier { get; set; }
}
