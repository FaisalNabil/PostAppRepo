using Newtonsoft.Json;
using PostApp.Service;
using PostApp.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace PostsApp.Controllers
{
    //[RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        private readonly PostService _post;
        const int MaxPageSize = 10;

        PostController()
        {
            _post = new PostService();
        }

        [HttpGet]
        [EnableQuery(PageSize = MaxPageSize)]
        //[Route("")]
        public IHttpActionResult Get(int page = 1, int pageSize = MaxPageSize, string postName = null)
        {
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var paginationHeader = new
            {
                totalCount = _post.GetAll().Count
            };

            List<PostModel> result = _post.GetRange(pageSize, page);

            HttpContext.Current.Response.AppendHeader("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            return Ok(result);
        }

        [HttpGet]
        [EnableQuery(PageSize = MaxPageSize)]
        //[Route("")]
        public IHttpActionResult Get(int page = 1, int pageSize = MaxPageSize)
        {
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var paginationHeader = new
            {
                totalCount = _post.GetAll().Count
            };

            List<PostModel> result = _post.GetRange(pageSize, page);

            HttpContext.Current.Response.AppendHeader("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            return Ok(result);
        }

        [HttpGet]
        //[Route("{id:string}", Name = "GetSinglePost")]
        [EnableQuery(PageSize = 1)]
        public IHttpActionResult GetSingle(string id)
        {
            PostModel postModel = _post.Get(id);

            if (postModel == null)
            {
                return NotFound();
            }

            return Ok(postModel);
        }

        [HttpPost]
        //[Route("")]
        public IHttpActionResult Create([FromBody] PostModel postModel)
        {
            if (postModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _post.Create(postModel);

            return CreatedAtRoute("GetSinglePost", new { id = id }, postModel);
        }

        [HttpPut]
        //[Route("{id:string}")]
        public IHttpActionResult Update(string id, [FromBody] PostModel postModel)
        {
            if (postModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _post.Update(postModel);

            return Ok(postModel);
        }

        [HttpDelete]
        //[Route("{id:string}")]
        public IHttpActionResult Delete(string id)
        {
            _post.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

}
