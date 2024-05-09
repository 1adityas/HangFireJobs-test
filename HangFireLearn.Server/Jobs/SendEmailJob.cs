using HangFireLearn.Producer.Interface;

namespace HangFireLearn.Server.Jobs
{
    public class SendEmailJob : ISendEmailJob
    {
        public SendEmailJob()
        {

        }
        public async Task Execute()
        {
            Console.WriteLine("executed");
        }
    }
}
