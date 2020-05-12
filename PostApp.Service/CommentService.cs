using PostApp.Data;
using PostApp.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentApp.Service
{
    public class CommentService
    {
        PostDBEntities db;
        public CommentService()
        {
            db = new PostDBEntities();
        }
        public List<CommentModel> GetAll()
        {
            List<CommentModel> commentModels = new List<CommentModel>();
            var result = db.Comments.ToList();
            foreach (var item in result)
            {
                CommentModel commentModel = new CommentModel() { Id = item.Id, Name = item.Name, PostId = item.PostId, UpVote = item.UpVote, DownVote = item.DownVote, MakeBy = item.MakeBy, MakeDate = item.MakeDate };
                commentModels.Add(commentModel);
            }
            return commentModels;
        }
        public List<CommentModel> GetRange(int pageSize, int page)
        {
            List<CommentModel> commentModels = new List<CommentModel>();
            var result = db.Comments.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            foreach (var item in result)
            {
                CommentModel commentModel = new CommentModel() { Id = item.Id, Name = item.Name, PostId = item.PostId, UpVote = item.UpVote, DownVote = item.DownVote, MakeBy = item.MakeBy, MakeDate = item.MakeDate };
                commentModels.Add(commentModel);
            }
            return commentModels;
        }
        public List<CommentModel> GetByPost(string postId)
        {
            List<CommentModel> commentModels = new List<CommentModel>();
            var result = db.Comments.Where(s=>s.PostId == postId).ToList();
            foreach (var item in result)
            {
                CommentModel commentModel = new CommentModel() { Id = item.Id, Name = item.Name, PostId = item.PostId, UpVote = item.UpVote, DownVote = item.DownVote, MakeBy = item.MakeBy, MakeDate = item.MakeDate };
                commentModels.Add(commentModel);
            }
            return commentModels;
        }
        public CommentModel Get(string id)
        {
            var result = db.Comments.Where(s => s.Id == id).FirstOrDefault();
            CommentModel commentModel = new CommentModel() { Id = result.Id, Name = result.Name, PostId = result.PostId, UpVote = result.UpVote, DownVote = result.DownVote, MakeBy = result.MakeBy, MakeDate = result.MakeDate };

            return commentModel;
        }
        public string Create(CommentModel commentModel)
        {
            Comment comment = new Comment() { Id = commentModel.Id, Name = commentModel.Name, PostId = commentModel.PostId, UpVote = commentModel.UpVote, DownVote = commentModel.DownVote, MakeBy = commentModel.MakeBy, MakeDate = commentModel.MakeDate };

            db.Comments.Add(comment);
            db.SaveChanges();

            return comment.Id;
        }
        public void Update(CommentModel commentModel)
        {
            Comment comment = db.Comments.Find(commentModel.Id);
            comment.Id = commentModel.Id;
            comment.Name = commentModel.Name;
            comment.MakeBy = commentModel.MakeBy;
            comment.MakeDate = commentModel.MakeDate;
            db.SaveChanges();
        }
        public void Delete(string id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
        }
    }
}
