using System;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Spi;
using TelegramBot.WebApi.Jobs;
using TelegramBot.WebApi.Models;
using TelegramBot.WebApi.Utils;

namespace TelegramBot.WebApi.Extensions
{
    public static class QuartzExtensions
    {
        public static void AddQuartz(this IServiceCollection services)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            services.AddTransient<ISchedulerFactory, StdSchedulerFactory>();
            services.AddTransient<IJobFactory, DIJobFactory>();
            services.AddTransient<GetNewsJob>();

            var config = new QuartzConfig();
            var getNewsJob = JobBuilder.Create<GetNewsJob>()
                .WithIdentity(QuartzSettings.GETNEWSJOB_IDENTITY, QuartzSettings.GETNEWSGROUP_IDENTITY)
                .Build();
            var getNewsTrigger = TriggerBuilder.Create()
                .WithIdentity(QuartzSettings.GETNEWSTRIGGER_IDENTITY, QuartzSettings.GETNEWSGROUP_IDENTITY)
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();
            config.Jobs.Add(new Tuple<IJobDetail, ITrigger>(getNewsJob, getNewsTrigger));
            services.AddSingleton(config);
        }

        public static IApplicationBuilder UseQuartz(this IApplicationBuilder app)
        {
            var schedulerFactory = app.ApplicationServices.GetService<ISchedulerFactory>();
            var config = app.ApplicationServices.GetService<QuartzConfig>();
            var scheduler = schedulerFactory.GetScheduler().Sync();
            scheduler.JobFactory = app.ApplicationServices.GetService<IJobFactory>();
            config.Jobs.ForEach(x => scheduler.ScheduleJob(x.Item1, x.Item2).Sync());
            scheduler.Start().Sync();
            return app;
        }
    }
}