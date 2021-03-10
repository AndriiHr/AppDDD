using App.Application.Feedbacks;
using System;
using System.Collections.Generic;

namespace App.Application.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<string> UsedTechnologies { get; set; }

        public List<FeedbackDto> Feedbacks { get; set; }
    }
}