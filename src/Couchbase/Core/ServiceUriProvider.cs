using System;

#nullable enable

namespace Couchbase.Core
{
    /// <summary>
    /// Default implementation of <see cref="IServiceUriProvider"/>.
    /// </summary>
    internal class ServiceUriProvider : IServiceUriProvider
    {
        private readonly ClusterContext _clusterContext;

        public ServiceUriProvider(ClusterContext clusterContext)
        {
            _clusterContext = clusterContext ?? throw new ArgumentNullException(nameof(clusterContext));
        }

        /// <inheritdoc />
        public Uri GetRandomAnalyticsUri() =>
            _clusterContext.GetRandomNodeForService(ServiceType.Analytics).AnalyticsUri;

        /// <inheritdoc />
        public Uri GetRandomQueryUri() =>
            _clusterContext.GetRandomNodeForService(ServiceType.Query).QueryUri;

        /// <inheritdoc />
        public Uri GetRandomSearchUri() =>
            _clusterContext.GetRandomNodeForService(ServiceType.Search).SearchUri;

        /// <inheritdoc />
        public Uri GetRandomManagementUri() =>
            _clusterContext.GetRandomNode().ManagementUri;

        /// <inheritdoc />
        public Uri GetRandomViewsUri(string bucketName) =>
            _clusterContext.GetRandomNodeForService(ServiceType.Views, bucketName).ViewsUri;

        /// <inheritdoc />
        public Uri GetRandomEventingUri() =>
            _clusterContext.GetRandomNodeForService(ServiceType.Eventing).EventingUri;
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
