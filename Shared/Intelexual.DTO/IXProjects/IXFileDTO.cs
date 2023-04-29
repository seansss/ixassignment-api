using System;
using System.ComponentModel.DataAnnotations;

namespace Intelexual.DTO.IXProjects
{
	public class IXFileDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
    }
}

