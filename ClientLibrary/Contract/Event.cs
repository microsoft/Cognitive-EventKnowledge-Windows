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
    /// Container of the response of hotevents and relatedevents.
    /// </summary>
    [DataContract]
    public class EventResponse
    {
        /// <summary>
        /// The result value of the response.
        /// </summary>
        [DataMember(EmitDefaultValue = false, Name = "value")]
        public IEnumerable<Event> Value { get; set; }

        /// <summary>
        /// The url of next link.
        /// </summary>
        [DataMember(EmitDefaultValue = false, Name = "@nextLink")]
        public string NextLink { get; set; }
    }

    /// <summary>
    /// Representation of the event, incuding event id, related entities, document publish time from and to, and document count.
    /// </summary>
    [DataContract]
    public class Event
    {
        /// <summary>
        /// Event ID.
        /// </summary>
        [DataMember(Name = "eventId")]
        public string EventId { get; set; }

        /// <summary>
        /// Related Entities of this event.
        /// </summary>
        [DataMember(Name = "relatedEntities")]
        public string[] RelatedEntities { get; set; }

        /// <summary>
        /// The earliest publish time of related documents.
        /// </summary>
        [DataMember(Name = "from")]
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// The latest publish time of related documents.
        /// </summary>
        [DataMember(Name = "to")]
        public DateTimeOffset To { get; set; }

        /// <summary>
        /// The latest document in this event.
        /// </summary>
        [DataMember(Name = "latestDocument")]
        public Document LatestDocument { get; set; }
    }
}
