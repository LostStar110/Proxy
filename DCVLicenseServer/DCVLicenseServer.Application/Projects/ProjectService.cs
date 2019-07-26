using DCVLicenseServer.Core;
using Sbp.Application;
using Sbp.Domain.Repositories;
using Sbp.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCVLicenseServer.Application
{

    public class ProjectService:ApplicationService
    {

        private readonly IRepository<Project> _repository;
        public ProjectService(IRepository<Project> repository)
        {
            _repository = repository;
        }


        public List<ProjectDto> GetAll()
        {
            return ObjectMapper.Map<List<ProjectDto>>(_repository.GetAll());
        }


        public bool CreateProject(ProjectDto input)
        {
            var query = _repository.FirstOrDefault(p => p.Name == input.Name);
            if (query!=null)
                return false;

            var pro = ObjectMapper.Map<Project>(input);

            _repository.Insert(pro);

            _repository.SaveChanges();

            return true;
        }

        public void DeleteProject(string name)
        {
            _repository.Delete(p => p.Name == name);
            _repository.SaveChanges();

        }

        public ProjectDto Get(string name)
        {
            var query = _repository.FirstOrDefault(p => p.Name == name);

            if (query!=null)
            {
                return ObjectMapper.Map<ProjectDto>(query);
            }

            return null;
        }


    }
}
