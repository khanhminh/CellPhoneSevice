using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class RelativeProductController : ApiController
    {
        private ProductDAO data = new ProductDAO();

        public List<SanPham> Get(int id, [FromUri]int count = 20)
        {
            return data.GetRelateProduct(id, count);
        }
    }
}
