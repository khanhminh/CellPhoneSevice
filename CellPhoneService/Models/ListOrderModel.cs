using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class ListOrderModel
    {
        public List<DonDatHang> orders { get; set; }
        public int count { get; set; }
    }
}