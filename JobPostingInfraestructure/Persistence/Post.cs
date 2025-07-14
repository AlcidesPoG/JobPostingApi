using System;
using System.Collections.Generic;

namespace JobPostingInfraestructure.Persistence;

public partial class Post
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string? Location { get; set; }

    public string? Type { get; set; }

    public string? Category { get; set; }

    public string? Description { get; set; }

    public string? Requirements { get; set; }

    public string? Salary { get; set; }
     
    public string? ApplyUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public int CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}
