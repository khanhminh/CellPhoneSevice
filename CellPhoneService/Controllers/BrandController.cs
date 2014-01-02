using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class BrandController : ApiController
    {
        private BrandDAO data = new BrandDAO();

        public IEnumerable<NhaSanXuat> Get()
        {
            return data.GetListBrand();
        }
    }
}
