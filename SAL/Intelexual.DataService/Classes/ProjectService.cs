using System;
using Intelexual.Data;
using Intelexual.DTO.IXProjects;
using Intelexual.Interface.IXProjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Intelexual.Data.Models;
using System.Linq.Expressions;

namespace Intelexual.DataService.Classes
{
    public class ProjectService : IProjectService
    {
        object _lock = new object();

        private ProjectDbContext dbContext;

        private ProjectDbContext LockedDbContext
        {
            get
            {
                lock (_lock)
                {
                    return this.dbContext;
                }
            }
        }

        public ProjectService(string connection)
        {
            this.dbContext = new ProjectDbContext(connection);
        }

        public async Task<ProjectDTO> GetProject(Guid projectId)
        {
            ProjectDTO projectDTO = new ProjectDTO();

            var project = await this.LockedDbContext.Projects
                .Include(a => a.ProjectUsers).ThenInclude(b => b.User)
                .Include(a => a.Files)
                .Where(a => a.Id == projectId)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return null; 
            }

            projectDTO.Id = projectId;
            projectDTO.Name = project.Name;
            projectDTO.StartDate = project.StartDate;

            if (project.ProjectUsers != null && project.ProjectUsers.Count > 0)
            {
                projectDTO.Users = new List<UserDTO>();

                foreach (var projectUser in project.ProjectUsers)
                {
                    if (projectUser.User != null)
                    {
                        projectDTO.Users.Add(new UserDTO() {
                            Id = projectUser.Id,
                            Name = projectUser.User.Name,
                            Email = projectUser.User.Email,
                        });
                    }
                }
            }

            if (project.Files != null && project.Files.Count > 0)
            {
                projectDTO.Files = new List<IXFileDTO>();

                foreach (var IXFile in project.Files)
                {
                        projectDTO.Files.Add(new IXFileDTO()
                        {
                            Id = IXFile.Id,
                            Name = IXFile.Name,
                            Type = IXFile.Type,
                            FilePath = IXFile.FilePath
                        });
                }
            }

            return projectDTO;
        }

        public async Task<RecordsDTO<ProjectDTO>> GetProjects(int? page, int? pageSize, List<Filter>? filters, Sorting? sorting)
        {
            RecordsDTO<ProjectDTO> projectDTOs = new RecordsDTO<ProjectDTO>();

            if (page.HasValue && pageSize.HasValue)
            {
                // Alternative for performance is to write a stored procedure that returns both records and record size instead of calling the database twice.

                IQueryable<Project> qProjects = this.LockedDbContext.Projects;

                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        if (!string.IsNullOrEmpty(filter.value) && filter.value.Trim() != "")
                        switch (filter.id)
                        {
                            case "name":
                                qProjects = qProjects.Where(a => a.Name.ToLower().Contains(filter.value.ToLower()));
                                break;
                            case "startDate":
                                // code block
                                break;
                            default:
                                // code block
                                break;
                        }
                    }
                }

                if (sorting != null)
                {
                    switch (sorting.id)
                    {
                        case "name":
                            qProjects = sorting.desc ? qProjects.OrderByDescending(a => a.Name) : qProjects.OrderBy(a => a.Name);
                            break;
                        case "startDate":
                            qProjects = sorting.desc ? qProjects.OrderByDescending(a => a.StartDate) : qProjects.OrderBy(a => a.StartDate);
                            break;
                        default:
                            // code block
                            break;
                    }
                }

                var projects = await qProjects
                   .Skip((page.Value) * pageSize.Value)
                   .Take(pageSize.Value)
                   .ToListAsync();

                var totalProjects = await qProjects.CountAsync();

                if (projects != null)
                {
                    projectDTOs.records = new List<ProjectDTO>();
                    // Alternative is to use Automapper 
                    foreach (var project in projects)
                    {
                        projectDTOs.records.Add(new ProjectDTO()
                        {
                            Id = project.Id,
                            Name = project.Name,
                            StartDate = project.StartDate
                        });
                    }

                    projectDTOs.TotalRecords = totalProjects;
                    projectDTOs.TotalPages = (int)Math.Ceiling((double)totalProjects / (double)pageSize.Value);
                    // projectDTOs.CurrentPage = page.Value;

                }
            }

            return projectDTOs;
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();
            var projects = await this.LockedDbContext.Projects.ToListAsync();

            // Alternative is to use Automapper 
            foreach (var project in projects)
            {
                projectDTOs.Add(new ProjectDTO() {
                    Id = project.Id,
                    Name = project.Name,
                    StartDate = project.StartDate
                });
            }

            return projectDTOs;
        }
    }
}

