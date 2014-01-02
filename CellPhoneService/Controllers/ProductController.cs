using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class ProductController : ApiController
    {
        private ProductDAO data = new ProductDAO();

        // GET api/values
        //public List<SanPham> Get([FromUri] string brand,
        //    [FromUri] string os,
        //    [FromUri] string price,
        //    [FromUri] string star,
        //    [FromUri] string query)
        //{
        //    int i = 0;
        //    return null;
        //}

        // GET api/values/5
        public SanPham Get(int id, bool detail = false)
        {
            SanPham sp = null;
            if (detail)
            {
                sp = data.GetProductWithDetail(id);
            }
            else
            {
                sp = data.GetProduct(id);
            }

            return sp;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}