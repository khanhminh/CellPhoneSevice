using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellPhoneService
{
    public class BrandDAO : DataDAO
    {
        public List<NhaSanXuat> GetListBrand()
        {
            return db.NhaSanXuats.ToList();
        }
    }
}
