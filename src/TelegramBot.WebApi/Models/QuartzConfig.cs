using System;
using System.Collections.Generic;
using Quartz;

namespace TelegramBot.WebApi.Models
{
    public class QuartzConfig
    {
        public List<Tuple<IJobDetail, ITrigger>> Jobs { get; set; } = new List<Tuple<IJobDetail, ITrigger>>();
    }
}