using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class WebsiteDAO : DataDAO
    {
        public List<Website> GetListWebsite()
        {
            return db.Websites.ToList();
        }
    }
}