using System;
using System.Collections.Generic;

namespace App.Application.Projects.Commands
{
    public class CreateProjectRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public List<string> UsedTechnologies { get; set; }
    }
}