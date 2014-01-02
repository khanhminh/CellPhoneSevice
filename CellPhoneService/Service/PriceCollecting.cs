using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CellPhoneService
{
    public class PriceCollecting
    {
        private ProductDAO pdata = new ProductDAO();
        private WebsiteDAO wdata = new WebsiteDAO();
        private PriceCompareDAO prdata = new PriceCompareDAO();

        public void Update()
        {
            int offset = 0;
            int count = 10;
            List<SanPham> listProduct = pdata.GetProducts(offset, count);
            List<Website> listWebsite = wdata.GetListWebsite();
            while (listProduct.Count > 0)
            {
                foreach (SanPham sp in listProduct)
                {
                    foreach (Website web in listWebsite)
                    {
                        SoSanhGia ss = new SoSanhGia();
                        ss.MaSP = sp.MaSP;
                        ss.MaWebsite = web.MaSo;
                        ss.Gia = GetPrice(web, sp.TenSP.Trim());
                        prdata.Update(ss);
                    }
                }

                offset += count;
                listProduct = pdata.GetProducts(offset, count);
            }

        }

        private string GetPrice(Website web, string productName)
        {
            string result = "";
            try
            {
                switch (web.MaSo)
                {
                    case "TGDD":
                        result = TGDD(productName, web.LinkTimKiem);
                        break;
                    case "MN":
                        result = MN(productName, web.LinkTimKiem);
                        break;
                    case "NK":
                        result = NK(productName, web.LinkTimKiem);
                        break;
                }
            }
            catch (Exception) { }

            return result;
        }


        private string TGDD(string productName, string query)
        {
            string url = string.Format(query, HttpUtility.UrlEncode(productName));
            HtmlNode root = GetRoot(url);
            HtmlNode list = root.SelectSingleNode("//*[@id='ulcontentListSearch']");
            if (list != null && list.ChildNodes.Count > 0)
            {
                HtmlNode sp = list.ChildNodes[0];
                HtmlNode info = sp.SelectSingleNode(".//*[@class='name clearfix']");
                if (info != null)
                {
                    string name = info.SelectSingleNode("h3").InnerText.Trim();
                    string price = info.SelectSingleNode(".//*[@class='price']").InnerText.Trim();
                    if (name.ToLower().Contains(productName.ToLower()) || productName.ToLower().Contains(name.ToLower()))
                    {
                        return GetPrice(price);
                    }                  
                }
                
            }

            return string.Empty;
        }

        private string MN(string productName, string query)
        {
            string url = string.Format(query, HttpUtility.UrlEncode(productName));

            HtmlNode root = GetRoot(url);
            HtmlNode div = root.SelectSingleNode("//*[@class='left_block show_prod']");
            HtmlNodeCollection listProduct = div.SelectNodes(".//*[@class='prod_item grid_112']");
            if (listProduct != null && listProduct.Count > 0)
            {
                HtmlNode sp = listProduct[0];
                string name = sp.SelectSingleNode("h4/a").InnerText.Trim();
                string price = sp.SelectSingleNode("p").InnerText.Trim();
                if (name.ToLower().Contains(productName.ToLower()) || productName.ToLower().Contains(name.ToLower()))
                {
                    return GetPrice(price);
                }                
            }

            return string.Empty;
        }

        private string NK(string productName, string query)
        {
            string url = string.Format(query, HttpUtility.UrlEncode(productName));

            HtmlNode root = GetRoot(url);
            HtmlNode div = root.SelectSingleNode("//*[@id='pagination_contents']");
            if (div != null)
            {
                HtmlNode node = div.SelectSingleNode(".//*[@class='block_right_cate2_new_ver2']");
                string name = node.SelectSingleNode(".//*[@class='block_title_sp_home_new']/a").InnerText.Trim();
                string price = node.SelectSingleNode(".//*[@class='block_price_sp_home_new']").InnerText.Trim();
                if (name.ToLower().Contains(productName.ToLower()) || productName.ToLower().Contains(name.ToLower()))
                {
                    return GetPrice(price);
                }
            }

            return string.Empty;
        }


        private HtmlNode GetRoot(string url)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(url);

            return doc.DocumentNode;
        }

        private string GetPrice(string src)
        {
            Regex reg = new Regex("\\d*");
            Match match = reg.Match(src.Replace(",", "").Replace(".", ""));

            return match.Value;
        }
    }
}