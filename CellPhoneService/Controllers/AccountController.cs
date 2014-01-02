using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class AccountController : ApiController
    {
        AccountDAO data = new AccountDAO();
                
        public TaiKhoan Get([FromUri]string username)
        {
            return data.GetAccount(username);
        }

        public int Post([FromBody]TaiKhoan account)
        {
            return data.CreateAccount(account);
        }

        public bool Put([FromBody]TaiKhoan account)
        {
            return data.UpdateAccount(account);
        }
    }
}
