using ShoeStore.Application.Abstractions.Iservices;

namespace ShoeDb.Application.Configuration
{
    public class SmsSender : ISmsSender
    {
        public Task SendSmsAsync(string phoneNumber, string message)
        {
            // Mock or integrate with an actual SMS provider like Twilio, Africa's Talking, etc.
            Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
            return Task.CompletedTask;
        }
    }
}
