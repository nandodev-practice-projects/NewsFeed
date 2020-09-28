using Microsoft.AspNetCore.SignalR;
using NewsFeed.Models.ViewModels;
using NewsFeed.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Hubs
{
    public class Publications : Hub
    {
        private readonly INewsRepository _repository;

        public Publications(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task RegisterNew(NewsViewModel _new)
        {
            _repository.AddNew(_new);

            //Sends the notification of a new added to client side
            return Clients.All.SendAsync("UpdateFeed", "A news has been added", default);
        }
    }
}
