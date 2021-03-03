using Microsoft.AspNetCore.Http;

namespace App.Application.Users.Commands
{
    public class UpdateImageProfileRequest
    {
        public int UserId { get; set; }

        public IFormFile ImageProfile { get; set; }

    }
}
