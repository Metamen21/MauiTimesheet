
using MauiTimesheet.Data;
using MauiTimesheet.Data.Entities;
using MauiTimesheet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTimesheet.Services
{
    public class ProjectService
    {
        private readonly DatabaseService _database;

        public ProjectService(DatabaseService database)
        {
            _database = database;
        }

        public List<ProjectModel> Projects { get; set; } = [];

        public async Task LoadProjectAsync()
        {

            Projects = [..(await _database.GetProjects())
           .Select(p => new ProjectModel
           {
               Id = p.Id,
               Name = p.Name,
               Description = p.Description,
               Color = p.Color
           })];
        }


        public async Task<bool> SaveProjectAsync(ProjectModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var project = new Project
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Color = model.Color
                    };

                    await _database.AddProject(project);
                    var newProjectModel = model.Clone();
                    newProjectModel.Id = model.Id;
                    Projects = [newProjectModel, .. Projects];

                }
                else
                {
                    var dbProject = await _database.GetProject(model.Id);
                    if (dbProject is null)
                    {
                        await MauiInterop.AlertAsync("Project does not exist", "Error");
                        return false;
                    }
                    dbProject.Name = model.Name;
                    dbProject.Description = model.Description;
                    dbProject.Color = model.Color;
                    await _database.UpdateProject(dbProject);

                    var existingProjectInTheList = Projects.First(p => p.Id == model.Id);
                    existingProjectInTheList.Name = model.Name;
                    existingProjectInTheList.Description = model.Description;
                    existingProjectInTheList.Color = model.Color;


                }
                return true;
            }
            catch (Exception Ex)
            {
               await MauiInterop.AlertAsync(Ex.Message, "Error");
                
            }
            return false;
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            try
            {
                var project =await _database.GetProject(projectId);
                if(project is null)
                {
                    await MauiInterop.AlertAsync("Project does not exist", "Error");
                    return false;
                }
                await _database.DeleteProject(project);
                var projectIndex = Projects.FindIndex(p => p.Id == projectId);
                if (projectIndex > -1)
                {
                    Projects.RemoveAt(projectIndex);
                }
                return true;
            
            }
            catch(Exception Ex)
            {

            }
            return false;

        }
    }
}
