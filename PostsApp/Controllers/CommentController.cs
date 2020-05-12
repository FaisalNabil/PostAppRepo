using CommentApp.Service;
using Newtonsoft.Json;
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
    public class CommentController : ApiController
    {
        private readonly CommentService _comment;
        const int MaxPageSize = 10;

        CommentController()
        {
            _comment = new CommentService();
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
                totalCount = _comment.GetAll().Count
            };

            List<CommentModel> result = _comment.GetRange(pageSize, page);

            HttpContext.Current.Response.AppendHeader("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            return Ok(result);
        }

        [HttpGet]
        //[Route("{id:string}", Name = "GetSingleComment")]
        [EnableQuery(PageSize = 1)]
        public IHttpActionResult GetSingle(string id)
        {
            CommentModel commentModel = _comment.Get(id);

            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel);
        }

        [HttpPost]
        //[Route("")]
        public IHttpActionResult Create([FromBody] CommentModel commentModel)
        {
            if (commentModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _comment.Create(commentModel);

            return CreatedAtRoute("GetSingleComment", new { id = id }, commentModel);
        }

        [HttpPut]
        //[Route("{id:string}")]
        public IHttpActionResult Update(string id, [FromBody] CommentModel commentModel)
        {
            if (commentModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _comment.Update(commentModel);

            return Ok(commentModel);
        }

        [HttpDelete]
        //[Route("{id:string}")]
        public IHttpActionResult Delete(string id)
        {
            _comment.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
