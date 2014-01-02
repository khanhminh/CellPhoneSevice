using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class RatingDAO : DataDAO
    {
        public bool UpdateRating(DanhGia model)
        {
            DanhGia dg = db.DanhGias.Find(model.MaSP, model.NguoiDung);
            if (dg == null)
            {
                return InsertRating(model);
            }
            try
            {
                dg.Diem = model.Diem;
                db.Entry(dg).State = EntityState.Modified;
                db.SaveChanges();
                UpdateRatingProduct(model.MaSP);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool InsertRating(DanhGia model)
        {
            try
            {
                db.DanhGias.Add(model);
                db.SaveChanges();
                UpdateRatingProduct(model.MaSP);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public RatingModel GetRatingProduct(int id)
        {
            RatingModel result = new RatingModel();
            result.id = id;
            var querySP = from sp in db.SanPhams
                          where sp.MaSP == id
                          select sp;
            SanPham p = querySP.FirstOrDefault();
            if (p != null)
            {
                result.score = (float)p.DiemDanhGia;
                result.count = (from dg in db.DanhGias
                                where dg.MaSP == id
                                select dg).Count();
            }

            return result;
        }

        private void UpdateRatingProduct(int id)
        {
            ProductDAO productDAO = new ProductDAO();
            SanPham sp = productDAO.GetProduct(id);
            sp.DiemDanhGia = (from dg in db.DanhGias
                              where dg.MaSP == id
                              select dg).Average(x => x.Diem);
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}