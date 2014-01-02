using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class FilterController : ApiController
    {
        ProductDAO data = new ProductDAO();
        public ListProductModel Post([FromBody] FilterModel filter)
        {
            ListProductModel result = null;
            try
            {
                result = data.Query(filter);
            }
            catch (Exception) { }

            return result;
        }
    }
}
