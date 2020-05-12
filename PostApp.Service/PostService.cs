using PostApp.Data;
using PostApp.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApp.Service
{
    public class PostService
    {
        PostDBEntities db;
        public PostService()
        {
            db = new PostDBEntities();
        }
        public List<PostModel> GetAll()
        {
            List<PostModel> postModels = new List<PostModel>();
            var result = db.Posts.ToList();
            foreach (var item in result)
            {
                PostModel postModel = new PostModel() { Id = item.Id, Name = item.Name, MakeBy = item.MakeBy, MakeDate = item.MakeDate };
                postModels.Add(postModel);
            }
            return postModels;
        }
        public List<PostModel> GetRange(int pageSize, int page)
        {
            List<PostModel> postModels = new List<PostModel>();
            var result = db.Posts.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            foreach (var item in result)
            {
                PostModel postModel = new PostModel() { Id = item.Id, Name = item.Name, MakeBy = item.MakeBy, MakeDate = item.MakeDate };
                postModels.Add(postModel);
            }
            return postModels;
        }
        public PostModel Get(string id)
        {
            var result = db.Posts.Where(s => s.Id == id).FirstOrDefault(); 
            PostModel postModel = new PostModel() { Id = result.Id, Name = result.Name, MakeBy = result.MakeBy, MakeDate = result.MakeDate };

            return postModel;
        }
        public PostModel GetByName(string name)
        {
            var result = db.Posts.Where(s => s.Name == name).FirstOrDefault();
            PostModel postModel = new PostModel() { Id = result.Id, Name = result.Name, MakeBy = result.MakeBy, MakeDate = result.MakeDate };

            return postModel;
        }
        public string Create(PostModel postModel)
        {
            Post post = new Post() { Id = postModel.Id, Name = postModel.Name, MakeBy = postModel.MakeBy, MakeDate = postModel.MakeDate };
            
            db.Posts.Add(post);
            db.SaveChanges();

            return post.Id;
        }
        public void Update(PostModel postModel)
        {
            Post post = db.Posts.Find(postModel.Id);
            post.Id = postModel.Id;
            post.Name = postModel.Name;
            post.MakeBy = postModel.MakeBy;
            post.MakeDate = postModel.MakeDate;
            db.SaveChanges();
        }
        public void Delete(string id)
        {
            Post post = db.Posts.Find(id);
            List<Comment> comments = db.Comments.Where(s => s.PostId == id).ToList();
            foreach (var item in comments)
            {
                db.Comments.Remove(item);
            }
            db.Posts.Remove(post);
        }
    }
}
