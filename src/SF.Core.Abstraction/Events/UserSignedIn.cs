using MediatR;

namespace SF.Core.Abstraction.Events
{
    public class UserSignedIn : INotification
    {
        public long UserId { get; set; }
    }
}
