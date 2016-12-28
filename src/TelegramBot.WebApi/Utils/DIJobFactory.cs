using System;
using Quartz;
using Quartz.Spi;
using StructureMap;

namespace TelegramBot.WebApi.Utils
{
    public class DIJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DIJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            var jobType = jobDetail.JobType;
            return (IJob) _serviceProvider.GetService(jobType);
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}