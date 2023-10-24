using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Rol : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<UserRol> UsersRols { get; set; }
}
