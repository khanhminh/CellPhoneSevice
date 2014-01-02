using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class DeliveryModeController : ApiController
    {
        private OrderDAO data = new OrderDAO();

        public List<PhuongThucGiaoHang> Get()
        {
            return data.GetListPTGH();
        }
    }
}
