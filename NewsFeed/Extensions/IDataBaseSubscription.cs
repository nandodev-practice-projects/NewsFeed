using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Extensions
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
