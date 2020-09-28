using Microsoft.AspNetCore.SignalR;
using NewsFeed.Extensions;
using NewsFeed.Models;
using NewsFeed.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using ErrorEventArgs = TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs;

namespace NewsFeed.Hubs
{
    public class PublicationsDatabaseSubscription: IDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly INewsRepository _repository;
        private readonly IHubContext<Publications> _hubContext;
        private SqlTableDependency<New> _tableDependency;

        public PublicationsDatabaseSubscription(INewsRepository repository, IHubContext<Publications> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<New>(connectionString, null, null, null, null, null, DmlTriggerType.All);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            Console.WriteLine("Waiting for receiving notifications...");
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<New> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                var Message = "";

                switch (e.ChangeType)
                {
                    case ChangeType.Delete:
                        Message = "A news has been deleted";
                        break;
                    case ChangeType.Insert:
                        Message = "A news has been added";
                        break;
                    case ChangeType.Update:
                        Message = "A news has been updated";
                        break;
                    default:
                        break;
                }

                // TODO: manage the changed entity
                var changedEntity = e.Entity;
                //_hubContext.Clients.All.SendAsync("UpdateFeed", _repository.News, default);
                _hubContext.Clients.All.SendAsync("UpdateFeed", Message, default);
            }
        }

        #region IDisposable

        ~PublicationsDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
