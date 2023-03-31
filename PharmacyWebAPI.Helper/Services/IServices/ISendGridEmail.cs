namespace PharmacyWebAPI.Utility.Services.IServices
{
    public interface ISendGridEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}