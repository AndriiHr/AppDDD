using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;

namespace App.Application.Projects.Commands
{
    public class DeleteProjectCommand : CommandBase
    {
        public int Id { get; }

        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
    }
}