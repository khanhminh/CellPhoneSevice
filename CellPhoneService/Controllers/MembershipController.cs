using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class MembershipController : ApiController
    {
        AccountDAO data = new AccountDAO();

        public string Get(string email)
        {
            return data.GetUsername(email);
        }

        // login
        public bool Post([FromBody] LoginModel account)
        {
            return data.CheckAccount(account);
        }

        // change password
        public bool Put([FromBody] UserMembershipModel user)
        {
            return data.ChangePassword(user);
        }
    }
}
