using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellPhoneService
{
    public class ProductDAO : DataDAO
    {
        private int Scale = 1000000;

        public List<SanPham> GetNewProducts(int start, int count)
        {
            return db.SanPhams.OrderByDescending(sp => sp.NgayNhap).Skip(start).Take(count).ToList();
        }

        public SanPham GetProduct(int id)
        {
            SanPham sp = db.SanPhams.Find(id);

            return sp;
        }

        public SanPham GetProductWithDetail(int id)
        {
            SanPham sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return null;
            }
            sp.ChiTietSanPham = GetDetailProduct(sp.MaCTSP);

            return sp;
        }

        public List<SanPham> GetProducts(int offset, int count)
        {
            List<SanPham> result = db.SanPhams.OrderBy(x => x.MaSP).Skip(offset).Take(count).ToList();
            foreach (SanPham sp in result)
            {
                sp.ChiTietSanPham = GetDetailProduct(sp.MaCTSP);
            }

            return result;
        }

        public List<SanPham> GetRelateProduct(int id, int count)
        {
            List<SanPham> result = new List<SanPham>();
            SanPham sp = db.SanPhams.Find(id);
            if (sp != null)
            {
                var qNSX = from p in db.SanPhams
                            where p.MaNSX == sp.MaNSX && p.MaSP != sp.MaSP
                            select p;
                result = qNSX.OrderBy(x => x.MaSP).Skip(0).Take(count).ToList();
                if (result.Count < count)
                {
                    var qPrice = from pPrice in db.SanPhams
                                 where pPrice.GiaBanHienHanh < (sp.GiaBanHienHanh + Scale) && pPrice.GiaBanHienHanh > (sp.GiaBanHienHanh - Scale)
                                 select pPrice;
                    List<SanPham> spPrice = qPrice.OrderBy(y => y.MaSP).Skip(0).Take(count - result.Count).ToList();
                    foreach (SanPham sanpham in spPrice)
                    {
                        if (!result.Contains(sanpham) && sanpham.MaSP != sp.MaSP)
                        {
                            result.Add(sanpham);
                        }
                    }
                }

            }

            return result;
        }

        private ChiTietSanPham GetDetailProduct(int MaCT)
        {
            var query = from ctsp in db.ChiTietSanPhams.Include("HinhAnhs").Include("HeDieuHanh")
                        where ctsp.MaChiTiet == MaCT
                        select ctsp;
            ChiTietSanPham ct = query.FirstOrDefault();
            if (ct != null)
            {
                ct.HeDieuHanh.ChiTietSanPhams = null;
                ct.SanPhams = null;
                foreach (HinhAnh ha in ct.HinhAnhs)
                {
                    ha.ChiTietSanPham = null;
                }
            }

            return ct;
        }

        private ChiTietSanPham GetDetailProductWithoutImage(int MaCT)
        {
            var query = from ctsp in db.ChiTietSanPhams.Include("HeDieuHanh")
                        where ctsp.MaChiTiet == MaCT
                        select ctsp;
            ChiTietSanPham ct = query.FirstOrDefault();
            if (ct != null)
            {
                ct.HeDieuHanh.ChiTietSanPhams = null;
                ct.SanPhams = null;
            }

            return ct;
        }

        public ListProductModel Query(FilterModel filter)
        {
            int offset = (filter.page - 1) * filter.view;
            int count = filter.view;

            IQueryable<SanPham> query = null;
            if (filter.query != null && filter.query.Trim() != "")
            {
                string name = filter.query;
                query = SearchByName(name, offset, count);
            }
            else
            {                
                string sql = CreatSqlQuery(filter);
                query = (((IObjectContextAdapter)db).ObjectContext.CreateQuery<SanPham>(sql));
            }
            ListProductModel result = new ListProductModel();
            string[] param = filter.sortby.Split('_');
            bool order_price = param[0] == "price" ? true : false;
            bool asc = param[1] == "asc" ? true : false;
            if (order_price)
            {
                if (asc)
                {
                    result.data = query.OrderBy(x => x.GiaBanHienHanh).Skip(offset).Take(count).ToList();
                }
                else
                {
                    result.data = query.OrderByDescending(x => x.GiaBanHienHanh).Skip(offset).Take(count).ToList();
                }
            }
            else
            {
                if (asc)
                {
                    result.data = query.OrderBy(x => x.TenSP).Skip(offset).Take(count).ToList();
                }
                else
                {
                    result.data = query.OrderByDescending(x => x.TenSP).Skip(offset).Take(count).ToList();
                }
            }
            foreach (SanPham sp in result.data)
            {
                sp.ChiTietSanPham = GetDetailProductWithoutImage(sp.MaCTSP);
            }
            result.count = query.Count();

            return result;
        }

        private string CreatSqlQuery(FilterModel filter)
        {
            string where = "";
            if (filter.price != null)
            {
                string wherePrice = "";
                foreach (string price in filter.price)
                {
                    if (wherePrice != "")
                    {
                        wherePrice += " or";
                    }
                    string[] param = price.Split('_');
                    wherePrice += string.Format(" (sp.GiaBanHienHanh >= {0} and sp.GiaBanHienHanh <= {1})", param[0], param[1]);
                }
                if (wherePrice != "")
                {
                    where += string.Format("({0})", wherePrice);
                }
            }
            if (filter.brand != null)
            {
                string whereBrand = "";
                foreach (string brand in filter.brand)
                {
                    if (whereBrand != "")
                    {
                        whereBrand += " or";
                    }
                    whereBrand += string.Format(" (sp.MaNSX = {0})", brand);
                }
                if (whereBrand != "")
                {
                    if (where != "")
                    {
                        where += " and";
                    }
                    where += string.Format("({0})", whereBrand);
                }
            }
            if (filter.star != null)
            {
                string whereStar = "";
                foreach (string star in filter.star)
                {
                    if (whereStar != "")
                    {
                        whereStar += " or";
                    }
                    whereStar += string.Format(" (sp.DiemDanhGia >= {0})", star);
                }
                if (whereStar != "")
                {
                    if (where != "")
                    {
                        where += " and";
                    }
                    where += string.Format("({0})", whereStar);
                }
            }
            if (filter.os != null)
            {
                string whereos = "";
                foreach (string os in filter.os)
                {
                    if (whereos != "")
                    {
                        whereos += " or";
                    }
                    whereos += string.Format(" (sp.ChiTietSanPham.HeDieuHanh.MaHDH = '{0}')", os);
                }
                if (whereos != "")
                {
                    if (where != "")
                    {
                        where += " and";
                    }
                    where += string.Format("({0})", whereos);
                }
            }
            string sql = "SELECT VALUE sp FROM CellPhoneDbEntities.SanPhams as sp";
            if (where != "")
            {
                sql += " WHERE" + where;
            }

            return sql;
        }

        private IQueryable<SanPham> SearchByName(string name, int offset, int count)
        {
            ListProductModel result = new ListProductModel();

            var query = from sp in db.SanPhams
                        where sp.TenSP.ToLower().Contains(name.ToLower())
                        select sp;

            return query;
        }

        public int GetTotalProduct()
        {
            return db.SanPhams.Count();
        }
    }
}
