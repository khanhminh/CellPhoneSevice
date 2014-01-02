using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class DetailProductModel
    {
        public SanPham sanpham { get; set; }
        public ChiTietSanPham chitietsanpham { get; set; }
        public List<HinhAnh> hinhanh { get; set; }
    }
}