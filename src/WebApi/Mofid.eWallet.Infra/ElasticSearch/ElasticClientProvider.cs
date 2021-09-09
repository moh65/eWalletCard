﻿using Microsoft.Extensions.Options;
using Nest;

namespace Mofid.eWallet.Infra.ElasticSearch
{
    public class ElasticClientProvider
    {
        public ElasticClientProvider(IOptions<ElasticConnectionSettings> settings)
        {
            // Create the connection settings
            ConnectionSettings connectionSettings =
                // Get the cluster URL from appsettings.json and pass it in
                new ConnectionSettings(new System.Uri(settings.Value.ClusterUrl));
            // This is going to enable us to see the raw queries sent to elastic when debugging (really useful)
            connectionSettings.EnableDebugMode();

            if (settings.Value.DefaultIndex != null)
            {
                // Get the index name from appsettings.json and pass it in
                connectionSettings.DefaultIndex(settings.Value.DefaultIndex);
            }
            // Create the actual client
            this.Client = new ElasticClient(connectionSettings);

        }

        public ElasticClient Client { get; }
    }
}

