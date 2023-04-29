using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intelexual.Data.Models
{
	public class User
	{
        [Key()]
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string? Email { get; set; }

        public virtual IList<ProjectUser>? ProjectUsers { get; set; }
    }
}