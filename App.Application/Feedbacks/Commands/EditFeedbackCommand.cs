using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;

namespace App.Application.Feedbacks.Commands
{
    public class EditFeedbackCommand : CommandBase
    {
        public FeedbackDto Feedback { get; }

        public EditFeedbackCommand(FeedbackDto feedback)
        {
            Feedback = feedback;
        }
    }
}