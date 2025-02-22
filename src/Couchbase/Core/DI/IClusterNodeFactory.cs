using System.Threading;
using System.Threading.Tasks;
using Couchbase.Core.Configuration.Server;
using Couchbase.Management.Buckets;

#nullable enable

namespace Couchbase.Core.DI
{
    /// <summary>
    /// Creates a <see cref="IClusterNode"/>.
    /// </summary>
    internal interface IClusterNodeFactory
    {
        /// <summary>
        /// Create and connect to a <see cref="IClusterNode"/>.
        /// </summary>
        /// <param name="endPoint"><see cref="HostEndpointWithPort"/> of the node.</param>
        /// <param name="bucketType"></param>
        /// <param name="nodeAdapter"></param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The <seealso cref="IClusterNode"/> type.</returns>
        Task<IClusterNode> CreateAndConnectAsync(HostEndpointWithPort endPoint, BucketType bucketType, NodeAdapter nodeAdapter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create and connect to a <see cref="IClusterNode"/>.
        /// </summary>
        /// <param name="endPoint"><see cref="HostEndpointWithPort"/> of the node.</param>
        /// <param name="bucketType"></param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The <seealso cref="IClusterNode"/> type.</returns>
        Task<IClusterNode> CreateAndConnectAsync(HostEndpointWithPort endPoint, BucketType bucketType, CancellationToken cancellationToken = default);
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
