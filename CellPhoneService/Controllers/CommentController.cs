using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class CommentController : ApiController
    {
        private CommentDAO data = new CommentDAO();

        public ListCommentModel Get(int id, [FromUri] int offset = 0, [FromUri] int count = 10)
        {
            return data.GetComments(id, offset, count);
        }

        public bool Post([FromBody] BinhLuan bl)
        {            
            return data.CreateComment(bl);
        }
    }
}
