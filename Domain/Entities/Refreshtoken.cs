using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class RefreshToken : BaseEntity
{

    public int UserId { get; set; }
    public User User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Revoked { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => Revoked == null && !IsExpired;
}
