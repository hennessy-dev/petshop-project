﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Soldmedicine
{
    public int Id { get; set; }

    public int MedicineId { get; set; }

    public int Amount { get; set; }

    public double Price { get; set; }

    public DateTime SoldDate { get; set; }

    public virtual Medicine Medicine { get; set; }
}