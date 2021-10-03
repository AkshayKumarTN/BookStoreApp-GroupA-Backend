using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FeedBackModel
    {
        public int FeedBackId { get; set; }
        public int BookId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
