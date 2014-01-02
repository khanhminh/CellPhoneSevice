using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class PaymentModeController : ApiController
    {
        private OrderDAO data = new OrderDAO();

        public List<PhuongThucThanhToan> Get()
        {
            return data.GetListPTTT();
        }
    }
}
