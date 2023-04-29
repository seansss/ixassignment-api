using System;
using System.ComponentModel.DataAnnotations;

namespace Intelexual.DTO.IXProjects
{
	public class UserDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public List<ProjectDTO>? ProjectDTOs { get; set; }
    }
}