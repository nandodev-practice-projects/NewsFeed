using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeed.Models
{
    public class New
    {
        [Key]
        public int NewID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorUserName { get; set; }
        public DateTime Created { get; set; }
        public int CategoryID { get; set; }

        [ForeignKey(nameof(CategoryID))]
        [InverseProperty(nameof(Category.News))]
        public virtual Category category { get; set; }
    }
}
