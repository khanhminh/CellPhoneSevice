using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class NewProductController : ApiController
    {
        private ProductDAO data = new ProductDAO();

        // GET api/newproduct
        public NewProduct Get([FromUri]int offset = 0, [FromUri]int count = 10)
        {
            NewProduct result = new NewProduct();
            result.data = data.GetNewProducts(offset, count);
            result.count = data.GetTotalProduct();

            return result;
        }

        public int Get(string option)
        {
            //if (option == "total")
            //{
            //    return data.GetTotalProduct();
            //}

            return 0;
        }

        // POST api/newproduct
        public void Post([FromBody]string value)
        {
        }

        // PUT api/newproduct/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/newproduct/5
        public void Delete(int id)
        {
        }
    }
}
