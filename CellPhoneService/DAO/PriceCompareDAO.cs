using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class PriceCompareDAO : DataDAO
    {
        public void Update(SoSanhGia ss)
        {
            SoSanhGia ssg = db.SoSanhGias.Where(x => x.MaSP == ss.MaSP && x.MaWebsite == ss.MaWebsite).FirstOrDefault();
            if (ssg != null)
            {
                ssg.Gia = ss.Gia;
                db.Entry(ssg).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                Insert(ss);
            }
        }

        public void Insert(SoSanhGia ss)
        {
            db.SoSanhGias.Add(ss);
            db.SaveChanges();
        }

        public List<SoSanhGia> GetListPriceCompare(int id)
        {
            var query = from ss in db.SoSanhGias.Include("Website")
                        where ss.MaSP == id
                        select ss;
            List<SoSanhGia> result = query.ToList();
            foreach (SoSanhGia ssg in result)
            {
                ssg.Website.SoSanhGias = null;
            }

            return result;
        }
    }
}