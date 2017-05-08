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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ProjectOxford.EventKnowledge.Contract
{
    /// <summary>
    /// Container of the response of eventdetail.
    /// </summary>
    [DataContract]
    public class DocumentResponse
    {
        /// <summary>
        /// The result value of the response.
        /// </summary>
        [DataMember(EmitDefaultValue = false, Name = "value")]
        public IEnumerable<Document> Value { get; set; }

        /// <summary>
        /// The url of next link.
        /// </summary>
        [DataMember(EmitDefaultValue = false, Name = "@nextLink")]
        public string NextLink { get; set; }
    }

    /// <summary>
    /// Representation of the document, incuding document id, source, publish date, title, summary, url and related entites.
    /// </summary>
    [DataContract]
    public class Document
    {
        /// <summary>
        /// Document ID.
        /// </summary>
        [DataMember(Name = "documentId")]
        public string DocumentId { get; set; }

        /// <summary>
        /// The source of the document.
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        /// The publish time of the document.
        /// </summary>
        [DataMember(Name = "publishDate")]
        public DateTimeOffset PublishDate { get; set; }

        /// <summary>
        /// The title of the document.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// The summary of the document.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// The url of the document.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The related entites of the document.
        /// </summary>
        [DataMember(Name = "entities")]
        public string[] Entities { get; set; }
    }
}
