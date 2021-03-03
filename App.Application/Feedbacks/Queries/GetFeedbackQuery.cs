using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;

namespace App.Application.Feedbacks.Queries
{
    public class GetFeedbackQuery : IQuery<FeedbackDto>
    {
        public int Id { get; }
        public GetFeedbackQuery(int id)
        {
            Id = id;
        }
    }
}