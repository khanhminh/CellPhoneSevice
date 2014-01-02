using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class FilterModel
    {
        public string[] brand { get; set; }
        public string[] os { get; set; }
        public string[] price { get; set; }
        public string[] star { get; set; }

        public string query { get; set; }
        public int page { get; set; }
        public string sortby { get; set; }
        public int view { get; set; }

       
    }
}