using System;
using Microsoft.AspNetCore.Mvc;
using Intelexual.DTO.IXProjects;
using Intelexual.Interface.IXProjects;
using static Intelexual.API.Models.HttpModels;
using System.Net;
using Newtonsoft.Json;
using System.Security.Principal;

namespace Intelexual.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
		{
            this._projectService = projectService;
        }

        [HttpGet("Query")]
        public async Task<JsonResult> Get([FromQuery] string? filters)
        {
            if (filters != null)
            {
                List<Filter> filtersobj = JsonConvert.DeserializeObject<List<Filter>>(filters);
            }

            return new JsonResult("FAILED");
        }


        [HttpGet("projects")]
        public async Task<JsonResult> Get([FromQuery] int? start, [FromQuery] int? size, [FromQuery] string? filters, [FromQuery] string? sorting)
        {
            HttpCoreResponse<RecordsDTO<ProjectDTO>> response = new HttpCoreResponse<RecordsDTO<ProjectDTO>>();

            try
            {
                List<Filter>? filtersobj = null;

                if (filters != null)
                {
                    filtersobj = JsonConvert.DeserializeObject<List<Filter>>(filters);
                }

                Sorting? objSorting = null;

                if (sorting != null)
                {
                    List<Sorting> sortingsArr = JsonConvert.DeserializeObject<List<Sorting>>(sorting);

                    if (sortingsArr != null && sortingsArr.Count > 0)
                    {
                        objSorting = sortingsArr[0];
                    }
                }

                response.results = await this._projectService.GetProjects(start, size, filtersobj, objSorting);

                if (response.results == null)
                {
                    response.status = (int)HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.status = 500;
            }

            return new JsonResult(response);
        }

        [HttpGet("projects/{id}")]
        public async Task<JsonResult> GetProject(string id)
        {
            HttpCoreResponse<ProjectDTO> response = new HttpCoreResponse<ProjectDTO>();

            try
            {
                Guid guid;

                if (Guid.TryParse(id, out guid))
                {
                    response.results = await this._projectService.GetProject(guid);

                    if (response.results == null)
                    {
                        response.status = (int)HttpStatusCode.NotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.status = 500;
            }

            return new JsonResult(response);
        }


        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            HttpCoreResponse<List<ProjectDTO>> response = new HttpCoreResponse<List<ProjectDTO>>();

            response.results = await this._projectService.GetAllProjects();

            if (response.results == null)
            {
                response.status = (int)HttpStatusCode.NotFound;
            }

            return new JsonResult(response);
        }
    }
}

