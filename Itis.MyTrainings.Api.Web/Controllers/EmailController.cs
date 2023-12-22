using Itis.MyTrainings.Api.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Тестовый контроллер для проверки работы EmailSenderService
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmailController: Controller
{
    private IEmailSenderService _emailSenderService;

    public EmailController(IEmailSenderService senderService)
    {
        _emailSenderService = senderService;
    }
    
    [HttpPost("sendEmail")]
    public async Task SendEmail(
        [FromQuery] string subject,
        [FromQuery] string body,
        [FromQuery] string sendTo)
        => await _emailSenderService.SendMessageAsync(subject, body, sendTo, null);
}