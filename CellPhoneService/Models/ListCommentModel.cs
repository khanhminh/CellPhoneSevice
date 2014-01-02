using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class ListCommentModel
    {
        public List<BinhLuan> comments { get; set; }
        public int count { get; set; }
    }
}