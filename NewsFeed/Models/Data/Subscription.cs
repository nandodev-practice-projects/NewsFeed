using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeed.Models
{
    public class Subscription
    {
        public int SubscriptionID { get; set; }
        public string Subscriber { get; set; }
        public int CategoryID { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey(nameof(CategoryID))]
        [InverseProperty(nameof(Category.Subscriptions))]
        public Category category { get; set; }

    }
}
