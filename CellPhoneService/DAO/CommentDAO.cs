using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class CommentDAO : DataDAO
    {
        public bool CreateComment(BinhLuan cmm)
        {
            bool result = true;
            try
            {
                cmm.Ngay = DateTime.Now;
                db.BinhLuans.Add(cmm);
                db.SaveChanges();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public ListCommentModel GetComments(int id, int offset, int count)
        {
            ListCommentModel model = new ListCommentModel();
            var query = from cmm in db.BinhLuans
                        where cmm.SanPham.MaSP == id
                        select cmm;
            model.comments = query.OrderByDescending(x => x.Ngay).Skip(offset).Take(count).ToList();
            model.count = db.BinhLuans.Count(y => y.MaSP == id);

            return model;
        }

    }
}