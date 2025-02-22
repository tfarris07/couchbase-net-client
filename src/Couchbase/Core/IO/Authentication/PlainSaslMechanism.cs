using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Core.Diagnostics.Tracing;
using Couchbase.Core.IO.Connections;
using Couchbase.Core.IO.Operations;
using Couchbase.Core.IO.Transcoders;
using Couchbase.Utils;
using Microsoft.Extensions.Logging;
using SaslStart = Couchbase.Core.IO.Operations.Authentication.SaslStart;
using SequenceGenerator = Couchbase.Core.IO.Operations.SequenceGenerator;

#nullable enable

namespace Couchbase.Core.IO.Authentication
{
    internal class PlainSaslMechanism : SaslMechanismBase
    {
        private readonly string _username;
        private readonly string _password;

        public PlainSaslMechanism(string username, string password, ILogger<PlainSaslMechanism> logger, IRequestTracer tracer,
            IOperationConfigurator operationConfigurator)
            : base(tracer, operationConfigurator)
        {
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            Logger = logger;
            MechanismType = MechanismType.Plain;
        }

        /// <inheritdoc />
        public override async Task AuthenticateAsync(IConnection connection, CancellationToken cancellationToken = default)
        {
            using var rootSpan = Tracer.RequestSpan(OuterRequestSpans.Attributes.Service, OuterRequestSpans.ServiceSpan.Internal.AuthenticatePlain);
            using var op = new SaslStart
            {
                Key = MechanismType.GetDescription()!,
                Content = GetAuthData(_username, _password),
                Opaque = SequenceGenerator.GetNext(),
                Span = rootSpan,
            };
            OperationConfigurator.Configure(op, SaslOptions.Instance);
            await SendAsync(op, connection, cancellationToken).ConfigureAwait(false);
        }

        static string GetAuthData(string userName, string passWord)
        {
            // see https://tools.ietf.org/html/rfc4616#section-2
            const string utf8Null = "\0";
            var sb = new StringBuilder();

            // authzid is optional, and not included at this time.
            sb.Append(utf8Null);
            sb.Append(userName);
            sb.Append(utf8Null);
            sb.Append(passWord);
            return sb.ToString();
        }
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
