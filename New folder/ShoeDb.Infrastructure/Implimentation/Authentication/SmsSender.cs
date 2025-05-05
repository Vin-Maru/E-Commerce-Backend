

using ShoeStore.Application.Abstractions.Iservices;

namespace ShoeStore.Infrastructure.Implimentation.Authentication
{
    public class SmsSender : ISmsSender
    {
        public SmsSender()
        {
              
        }
        public Task SendSmsAsync(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }
    }
}
