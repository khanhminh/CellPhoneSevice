using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellPhoneService
{
    public abstract class DataDAO
    {
        protected CellPhoneDbEntities db = new CellPhoneDbEntities();

        public DataDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
