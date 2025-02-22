using System;

#nullable enable

namespace Couchbase.Core.Utils
{
    public static class StringExtensions
    {
        public static string ToHexString(this uint opaque)
        {
            const string hexPrefix = "0x", hexFormat = "x";
            return string.Join(hexPrefix, opaque.ToString(hexFormat));
        }

        /// <summary>
        /// Adds back ticks to the beginning and end of a string if they do not already exist.
        /// </summary>
        /// <param name="value">A value such as a bucket or scope name.</param>
        /// <returns>The original value escaped with back ticks.</returns>
        public static string EscapeIfRequired(this string value)
        {
            const char backtick = '`';

            if (value.Length == 0)
            {
                return "``";
            }

            if (value[0] != backtick)
            {
                if (value[value.Length - 1] != backtick)
                {
                    // hot path is no backticks at all
                    return $"`{value}`";
                }

                return backtick + value;
            }

            if (value[value.Length - 1] != backtick)
            {
                return value + backtick;
            }

            // Already has backticks
            return value;
        }

        /// <summary>
        /// Allows for case and cultural insensitive comparisons
        /// </summary>
        /// <param name="source">The incoming string to compare.</param>
        /// <param name="value">The value to check for.</param>
        /// <param name="comparison">The <see cref="StringComparison"/> to use.</param>
        /// <returns></returns>
        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            return source?.IndexOf(value, comparison) >= 0;
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
