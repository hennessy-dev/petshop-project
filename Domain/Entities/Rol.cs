using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Rol : BaseEntity
{

    public string RolName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
