using System;
using System.Collections.Generic;

namespace JobPostingInfraestructure.Persistence;

public partial class User
{
    public int UserId { get; set; }

    public int CompanyId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfileImage { get; set; }

    public string Role { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;
}
