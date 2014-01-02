using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class ApplicationDAO : DataDAO
    {
        public Application GetAppSecret(string AppId)
        {
            return db.Applications.Find(AppId);
        }
    }
}