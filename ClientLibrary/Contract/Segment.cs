// 
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
// 
// Microsoft Cognitive Services (formerly Project Oxford): https://www.microsoft.com/cognitive-services
// 
// Microsoft Cognitive Services (formerly Project Oxford) GitHub:
// https://github.com/Microsoft/ProjectOxford-ClientSDK
// 
// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProjectOxford.EventKnowledge.Contract
{
    /// <summary>
    /// Representation of the segmented query result.
    /// </summary>
    /// <typeparam name="T">The entity type of the query.</typeparam>
    public class Segment<T> : IEnumerable<T>
    {
        /// <summary>
        /// A continuation token from the server when the operation returns a partial result.
        /// </summary>
        public string ContinuationToken { get; private set; }
        /// <summary>
        /// The IEumerable result of the segmented query.
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        /// Constructor takes two parameters.
        /// </summary>
        /// <param name="result">The IEumerable result of the segmented query.</param>
        /// <param name="continuationToken">A continuation token from the server when the operation returns a partial result.</param>
        public Segment(IEnumerable<T> result, string continuationToken)
        {
            Result = result;
            ContinuationToken = continuationToken;
        }

        /// <summary>
        /// Get a enumerator of the IEnumerable result.
        /// </summary>
        /// <returns>A enumerator of the IEnumerable result.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        /// <summary>
        /// Get the enumerator of the Segment.
        /// </summary>
        /// <returns>A enumerator of the Segment.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
