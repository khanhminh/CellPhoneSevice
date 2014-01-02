using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace CellPhoneService
{
    public class NewsCollecting
    {
        private NewsDAO data = new NewsDAO();
        private string newsUrl = ConfigurationManager.AppSettings["newsurl"].ToString();
        private string newsUrlBase = ConfigurationManager.AppSettings["newsurlbase"].ToString();

        public void Update()
        {
            List<TinTuc> ListNews = GetNews();
            foreach (TinTuc tt in ListNews)
            {
                data.InsertNews(tt);
            }
        }

        private List<TinTuc> GetNews()
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(newsUrl);
            HtmlNode root = doc.DocumentNode;
            HtmlNode content = root.SelectSingleNode("//*[@id='category']/div/section/section[2]");
            HtmlNodeCollection listNews = content.SelectNodes("article");
            List<TinTuc> result = new List<TinTuc>();
            foreach (HtmlNode news in listNews)
            {
                HtmlNode divImage = news.SelectSingleNode("div[@class='cover']");
                string style = divImage.GetAttributeValue("style", "");
                string img = GetImage(style);
                string link = divImage.SelectSingleNode("a").GetAttributeValue("href", "#");
                string title = news.SelectSingleNode("header/h1/a").InnerText.Trim();
                string time = news.SelectSingleNode("header/time").GetAttributeValue("datetime", "");
                string sumary = news.SelectSingleNode("header/p[@class='summary']").InnerText.Trim();
                DateTime date = DateTime.Parse(time);

                TinTuc tt = new TinTuc()
                {
                    TieuDe = title,
                    Link = newsUrlBase + link,
                    Hinh = img,
                    MoTa = sumary,
                    Ngay = date
                };
                result.Add(tt);
            }

            return result;
        }

        private string GetImage(string src)
        {
            Regex reg = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)");
            Match match = reg.Match(src);

            return match.Value;
        }
    }
}