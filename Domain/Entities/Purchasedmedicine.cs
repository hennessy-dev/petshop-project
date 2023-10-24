using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PurchasedMedicine : BaseEntity
{

    public int SupplierId { get; set; }

    public int MedicineId { get; set; }

    public int Amount { get; set; }

    public double Price { get; set; }

    public DateTime PurchaseDate { get; set; }

    public virtual Medicine Medicine { get; set; }

    public virtual Supplier Supplier { get; set; }
}
