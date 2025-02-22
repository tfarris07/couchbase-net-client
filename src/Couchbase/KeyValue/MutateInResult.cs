using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase.Core;
using Couchbase.Core.Compatibility;
using Couchbase.Core.IO.Operations.SubDocument;
using Couchbase.Core.IO.Serializers;
using Couchbase.Utils;

#nullable enable

namespace Couchbase.KeyValue
{
    [Obsolete("This class will be made internal in a future release.")]
    public class MutateInResult : IMutateInResult, ITypeSerializerProvider
    {
        private readonly IList<MutateInSpec> _specs;

        /// <inheritdoc />
        public ITypeSerializer Serializer { get; }

        // Purely present for semver, delete when this class is made internal
        public MutateInResult(IList<OperationSpec> specs, ulong cas, MutationToken? token, ITypeSerializer serializer)
            : this(specs.OfType<MutateInSpec>().ToList(), cas, token, serializer)
        {
        }

        public MutateInResult(IList<MutateInSpec> specs, ulong cas, MutationToken? token, ITypeSerializer serializer)
        {
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (specs == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(specs));
            }
            if (serializer == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(serializer));
            }
            // ReSharper restore ConditionIsAlwaysTrueOrFalse

            _specs = specs.OrderBy(spec => spec.OriginalIndex).ToList();
            Cas = cas;
            MutationToken = token ?? MutationToken.Empty;
            Serializer = serializer;
        }

        public ulong Cas { get; }
        public MutationToken MutationToken { get; set; }
        public T? ContentAs<T>(int index)
        {
            if (index < 0 || index >= _specs.Count)
            {
                ThrowHelper.ThrowInvalidIndexException($"The index provided is out of range: {index}.");
            }

            var spec = _specs[index];
            return Serializer.Deserialize<T>(spec.Bytes);
        }

        /// <inheritdoc />
        [InterfaceStability(Level.Volatile)]
        public int IndexOf(string path)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (path == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(path));
            }

            for (var i = 0; i < _specs.Count; i++)
            {
                if (_specs[i].Path == path)
                {
                    return i;
                }
            }

            return -1;
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
