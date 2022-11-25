using MessagingSystemApp.Domain.Enums;
namespace MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse
{
    public class CreatePostCommandResponse
    {
        public string FileName { get; set; } = null!;
        public string Type { get; set; }=null!;
        public string Path { get; set; } = null!;
        public int PostId { get; set; }
    }
}
