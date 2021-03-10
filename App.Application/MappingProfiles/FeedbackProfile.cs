using App.Application.Feedbacks;
using App.Domain.Feedbacks;
using AutoMapper;

namespace App.Application.MappingProfiles
{
    public class FeedbackProfile: Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>();
        }
        
    }
}