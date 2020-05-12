using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApp.Service.Model
{
    public class CommentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PostId { get; set; }
        public Nullable<int> UpVote { get; set; }
        public Nullable<int> DownVote { get; set; }
        public string MakeBy { get; set; }
        public Nullable<System.DateTime> MakeDate { get; set; }
    }
}
