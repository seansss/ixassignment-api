using System;
using Intelexual.DTO.IXProjects;

namespace Intelexual.Interface.IXProjects
{
	public interface IProjectService
	{
        Task<List<ProjectDTO>> GetAllProjects();
        Task<RecordsDTO<ProjectDTO>> GetProjects(int? page, int? pageSize, List<Filter>? filters, Sorting? sorting);
        Task<ProjectDTO> GetProject(Guid projectId);
    }
}