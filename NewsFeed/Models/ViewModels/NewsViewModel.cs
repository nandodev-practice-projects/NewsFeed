using System;

namespace NewsFeed.Models.ViewModels
{
    public class NewsViewModel
    {
        public int NewID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
        public string ReturnUrl { get; set; }

    }
}
