using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Rol> Rols { get; set; } = new List<Rol>();
    public ICollection<UserRol> UsersRols { get; set; }
}
