using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeed.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<New> News { get; set; }
    }
}
