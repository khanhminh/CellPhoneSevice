using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class OrderDAO : DataDAO
    {
        public List<PhuongThucGiaoHang> GetListPTGH()
        {
            return db.PhuongThucGiaoHangs.ToList();
        }

        public List<PhuongThucThanhToan> GetListPTTT()
        {
            return db.PhuongThucThanhToans.ToList();
        }

        public int CreateOrder(DonDatHang order)
        {
            try
            {
                db.DonDatHangs.Add(order);
                db.SaveChanges();

                return order.MaDDH;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public ListOrderModel GetListOrder(string username, int offset, int count)
        {
            var query = from dh in db.DonDatHangs.Include("TrangThaiDonDatHang")
                        where dh.MaTK == username
                        select dh;
            ListOrderModel result = new ListOrderModel();
            result.count = query.Count();
            result.orders = query.OrderByDescending(x => x.NgayLap).Skip(offset).Take(count).ToList();
            foreach (DonDatHang dh in result.orders)
            {
                dh.TrangThaiDonDatHang.DonDatHangs = null;
            }

            return result;
        }

        public DonDatHang GetOrder(int id)
        {
            var query = from dh in db.DonDatHangs
                            .Include("PhuongThucGiaoHang")
                            .Include("PhuongThucThanhToan")
                            .Include("TrangThaiDonDatHang")
                        where dh.MaDDH == id
                        select dh;
            DonDatHang ddh = query.FirstOrDefault();
            if (ddh != null)
            {
                ddh.NguoiNhan = GetNguoiNhan(ddh.MaNguoiNhan);
                ddh.PhuongThucGiaoHang.DonDatHangs = null;
                ddh.PhuongThucThanhToan.DonDatHangs = null;
                ddh.TrangThaiDonDatHang.DonDatHangs = null;
                ddh.ChiTietDonHangs = GetListDetail(ddh.MaDDH);
            }

            return ddh;
        }

        private NguoiNhan GetNguoiNhan(int id)
        {
            NguoiNhan nn = db.NguoiNhans.Find(id);
            nn.DonDatHangs = null;

            return nn;
        }

        private List<ChiTietDonHang> GetListDetail(int id)
        {
            var query = from ctdh in db.ChiTietDonHangs
                        where ctdh.MaDDH == id
                        select ctdh;
            List<ChiTietDonHang> result = query.ToList();
            foreach (ChiTietDonHang ctdh in result)
            {
                ctdh.DonDatHang = null;
            }

            return result;
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                DonDatHang dh = db.DonDatHangs.Find(id);
                if (dh.MaTrangThai != "CGTT")
                {
                    return false;
                }
                var query = from ctdh in db.ChiTietDonHangs
                            where ctdh.MaDDH == id
                            select ctdh;
                List<ChiTietDonHang> ctdhs = query.ToList();
                for (int i = 0; i < ctdhs.Count; i++)
                {
                    db.ChiTietDonHangs.Remove(ctdhs[i]);
                }

                db.Entry(dh).State = EntityState.Deleted;
                NguoiNhan nn = db.NguoiNhans.Find(dh.MaNguoiNhan);
                db.NguoiNhans.Remove(nn);

                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}