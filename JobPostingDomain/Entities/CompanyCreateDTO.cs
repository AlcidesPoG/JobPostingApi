﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Entities
{
    public class CompanyCreateDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public IFormFile PhotoFile { get; set; } = null;

        public string? PageUrl { get; set; }

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Description { get; set; }
    }
}
