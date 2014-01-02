using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class PriceCompareController : ApiController
    {
        private PriceCompareDAO data = new PriceCompareDAO();

        public List<SoSanhGia> Get(int id)
        {
            return data.GetListPriceCompare(id);
        }
    }
}
