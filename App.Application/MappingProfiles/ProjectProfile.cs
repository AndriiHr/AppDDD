using System.Collections.Generic;
using System.Linq;
using App.Application.Projects;
using App.Application.Projects.Commands;
using App.Domain.Projects;
using AutoMapper;

namespace App.Application.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                    .ForMember(x => x.UsedTechnologies, c => c.MapFrom(z => z.UsedTechnologies.Split(',', System.StringSplitOptions.None).ToList()));

            CreateMap<ProjectDto, Project>()
                    .ForMember(x => x.UsedTechnologies, c => c.MapFrom(z => string.Join(',',z.UsedTechnologies)));

            CreateMap<List<Project>, List<ProjectDto>>();
            CreateMap<List<ProjectDto>, List<Project>>();

            CreateMap<CreateProjectRequest, ProjectDto>();
        }
    }
}