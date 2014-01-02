using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class OsDAO : DataDAO
    {
        public List<HeDieuHanh> GetOS()
        {
            return db.HeDieuHanhs.ToList();
        }
    }
}