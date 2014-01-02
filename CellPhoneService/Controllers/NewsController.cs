using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class NewsController : ApiController
    {
        private NewsDAO data = new NewsDAO();
        public List<TinTuc> Get([FromUri] int offset, [FromUri] int count)
        {
            return data.GetListNews(offset, count);
        }
    }
}
