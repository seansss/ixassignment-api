using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intelexual.Data.Models
{
	public class Project
	{
        [Key()]
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public virtual IList<IXFile>? Files { get; set; }

        public virtual IList<ProjectUser>? ProjectUsers { get; set; }

    }
}

