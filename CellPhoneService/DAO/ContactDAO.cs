using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class ContactDAO : DataDAO
    {
        public bool CreateContact(LienHe lh)
        {
            try
            {
                lh.Ngay = DateTime.Now;
                db.LienHes.Add(lh);
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