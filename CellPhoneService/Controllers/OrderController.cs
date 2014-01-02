using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class OrderController : ApiController
    {
        private OrderDAO data = new OrderDAO();

        public int Post([FromBody] DonDatHang order)
        {
            return data.CreateOrder(order);
        }

        public ListOrderModel Get([FromUri]string username, [FromUri]int offset, [FromUri]int count)
        {
            return data.GetListOrder(username, offset, count);
        }

        public DonDatHang Get(int id)
        {
            return data.GetOrder(id);
        }

        public bool Delete(int id)
        {
            return data.DeleteOrder(id);
        }
    }
}
