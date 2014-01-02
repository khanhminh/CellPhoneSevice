using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class ListProductModel
    {
        public List<SanPham> data { get; set; }
        public int count { get; set; }
    }
}