using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class ContactController : ApiController
    {
        private ContactDAO data = new ContactDAO();

        public bool Post([FromBody] LienHe lh)
        {
            return data.CreateContact(lh);
        }
    }
}
