using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApp.Service.Model
{
    public class PostModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MakeBy { get; set; }
        public Nullable<System.DateTime> MakeDate { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
