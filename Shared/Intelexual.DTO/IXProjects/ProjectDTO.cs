using System;
using System.ComponentModel.DataAnnotations;

namespace Intelexual.DTO.IXProjects
{
	public class ProjectDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public List<IXFileDTO>? Files { get; set; }
        public List<UserDTO>? Users { get; set; }
    }
}

