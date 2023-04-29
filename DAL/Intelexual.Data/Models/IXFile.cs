using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intelexual.Data.Models
{
	public class IXFile
	{
        [Key()]
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Type { get; set; }

        [StringLength(1000, MinimumLength = 1)]
        public string FilePath { get; set; }
    }
}

