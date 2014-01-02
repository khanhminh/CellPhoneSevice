using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class RatingController : ApiController
    {
        private RatingDAO data = new RatingDAO();

        public RatingModel Get(int id)
        {
            return data.GetRatingProduct(id);
        }

        public bool Put([FromBody] DanhGia model)
        {
            return data.UpdateRating(model);
        }
    }
}
