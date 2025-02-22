using System;
using Couchbase.Core.Configuration.Server;
using Couchbase.Core.Configuration.Server.Streaming;
using Couchbase.Core.IO.HTTP;
using Microsoft.Extensions.Logging;

#nullable enable

namespace Couchbase.Core.DI
{
    /// <summary>
    /// Default implementation of <see cref="IHttpStreamingConfigListenerFactory"/>.
    /// </summary>
    internal class HttpStreamingConfigListenerFactory : IHttpStreamingConfigListenerFactory
    {
        private readonly ClusterOptions _clusterOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HttpStreamingConfigListener> _logger;

        public HttpStreamingConfigListenerFactory(ClusterOptions clusterOptions, IServiceProvider serviceProvider, ILogger<HttpStreamingConfigListener> logger)
        {
            _clusterOptions = clusterOptions ?? throw new ArgumentNullException(nameof(clusterOptions));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public HttpStreamingConfigListener Create(string bucketName, IConfigHandler configHandler) =>
            new HttpStreamingConfigListener(bucketName, _clusterOptions,
                _serviceProvider.GetRequiredService<ICouchbaseHttpClientFactory>(), // Get each time so it's not a singleton
                configHandler, _logger);
    }
}


/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2021 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/
