﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using Microsoft.Extensions.Logging;
using RestSharp;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.DB.Services;
using TelegramBot.WebApi.Extensions;
using TelegramBot.WebApi.Models;

namespace TelegramBot.WebApi.Services
{
    public class SchoolNewService
    {
        private readonly ILogger _logger;

        public SchoolNewService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(SchoolNewsService));
        }

        private const string DATE_ATTR = "map";

        public async Task<IEnumerable<SchoolNews>> GetNewsAsync(DateTime dateTime)
        {
            var client = new RestClient("http://sch1.sharkovschina.edu.by");
            var request = new RestRequest("main.aspx", Method.GET);
            request.AddParameter("guid", "1031");
            request.AddParameter("map", $"{dateTime.Year}{dateTime.Month:00}");
            var response = await client.ExecuteTaskAsync(request);
            var html = response.Content;
            return ParseNews(client.BaseUrl.AbsoluteUri, html);
        }

        private const string SELECTOR = "#news-element";

        private IEnumerable<SchoolNews> ParseNews(string domain, string html)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(html);
            foreach (var element in document.QuerySelectorAll(SELECTOR))
            {
                yield return new SchoolNews
                {
                    ImageUrl = domain + element.ChildNodes[0].TextContent,
                    Title = element.ChildNodes[1].TextContent,
                    Url = domain + element.ChildNodes[2].TextContent,
                    Date = DateTime.ParseExact(element.ChildNodes[3].TextContent, "dd.MM.yyyy", CultureInfo.InvariantCulture)
                };
            }
        }
    }
}