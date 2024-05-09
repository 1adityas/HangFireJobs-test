using Hangfire;
using Hangfire.Common;
using HangFireLearn.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace HangFireLearn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobScheduleController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobScheduleController(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;

        }

        [HttpPost]
        [Route("fire-and-forget")]
        public Task<string> FireAndForget([FromBody] string text)
        {
            _backgroundJobClient.Enqueue(() => Console.WriteLine(text));
            return Task.FromResult(text);
        }

        [HttpPost]
        [Route("delayed")]
        public Task<string> Delayed([FromBody] string text)
        {
            _backgroundJobClient.Schedule(() => Console.WriteLine(text), TimeSpan.FromMinutes(1));
            return Task.FromResult(text);
        }

        [HttpPost]
        [Route("continuation")]
        public Task<string> Continuation([FromBody] string text)
        {
            var jobId = _backgroundJobClient.Schedule(() =>
                Console.WriteLine(text), TimeSpan.FromMinutes(1));

            _backgroundJobClient.ContinueJobWith(jobId, () => Console.WriteLine(text));
            return Task.FromResult(text);
        }

        [HttpPost]
        [Route("SendMail")]
        public Task<string> SendMail([FromBody] string text)
        {
            var manager = new RecurringJobManager();
            manager.AddOrUpdate(Guid.NewGuid().ToString(), Job.FromExpression(() => MailSender.SendSingleMail()), Cron.Minutely());
            return Task.FromResult(text);
        }

    }
}
