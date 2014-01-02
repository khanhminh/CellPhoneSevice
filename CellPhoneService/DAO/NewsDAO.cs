using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class NewsDAO : DataDAO
    {
        public void InsertNews(TinTuc news)
        {
            db.TinTucs.Add(news);
            db.SaveChanges();
        }

        public List<TinTuc> GetListNews(int offset, int count)
        {
            return db.TinTucs.OrderByDescending(x => x.Ngay).Skip(offset).Take(count).ToList();
        }
    }
}