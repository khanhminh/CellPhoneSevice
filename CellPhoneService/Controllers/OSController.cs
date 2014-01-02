using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class OSController : ApiController
    {
        private OsDAO data = new OsDAO();

        public List<HeDieuHanh> Get()
        {
            return data.GetOS();
        }
    }
}
