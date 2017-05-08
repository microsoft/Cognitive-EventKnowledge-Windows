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
using System.Threading.Tasks;

using Microsoft.ProjectOxford.EventKnowledge.Contract;

namespace Microsoft.ProjectOxford.EventKnowledge
{

    /// <summary>
    /// The EventKnowledge service client proxy interface.
    /// </summary>
    public interface IEventKnowledgeServiceClient
    {
        /// <summary>
        /// Get hot events by date and return the results as a result segment.
        /// </summary>
        /// <param name="date">The specific date to be queried on.</param>
        /// <param name="top">The number of events to be returned. 
        /// <para>
        ///     E.g., if top is 5, the first 5 events will be returned. If not specified,
        ///     the service will return the first page of events and a continuation token
        ///     if possible.
        /// </para></param>
        /// <param name="skip">The number of events to be skipped. 
        /// <para>
        ///     E.g., if skip is 5, the first 5 events will be skipped. If not specified,
        ///     the service will not skip any events.
        /// </para></param>
        /// <param name="continuationToken">A continuation token returned by a previous operation, can be null.</param>
        /// <returns>Async task, which, upon completion, will return a result segment including the Hot Events on the specific date.</returns>
        Task<Segment<Event>> GetHotEventsSegmentedAsync(DateTimeOffset date, int? top = null, int? skip = null, string continuationToken = null);

        /// <summary>
        /// Get related events by wikipedia id and return the results as a result segment.
        /// </summary>
        /// <param name="wikipediaId">The specific wikipedia id to be queried on. </param>
        /// <param name="top">The number of events to be returned. 
        /// <para>
        ///     E.g., if top is 5, the first 5 events will be returned. If not specified, 
        ///     the service will return the first page of events and a continuation token
        ///     if possible.
        /// </para></param>
        /// <param name="skip">The number of events to be skipped. 
        /// <para>
        ///     E.g., if skip is 5, the first 5 events will be skipped. If not specified,
        ///     the service will not skip any events.
        /// </para></param>
        /// <param name="continuationToken">A continuation token returned by a previous operation, can be null.</param>
        /// <returns>Async task, which, upon completion, will return a result segment including the Related Events of the specific wikipedia id.</returns>
        Task<Segment<Event>> GetRelatedEventsSegmentedAsync(string wikipediaId, int? top = null, int? skip = null, string continuationToken = null);

        /// <summary>
        /// Get the detail of the event and return the results as a result segment.
        /// </summary>
        /// <param name="eventId">The specific event id to be queried on. </param>
        /// <param name="top">The number of documents to be returned. 
        /// <para>
        ///     E.g., if top is 5, the first 5 documents will be returned. If not specified, 
        ///     the service will return the first page of documents and a continuation token
        ///     if possible.
        /// </para></param>
        /// <param name="skip">The number of documents to be skipped. 
        /// <para>
        ///     E.g., if skip is 5, the first 5 documents will be skipped. If not specified,
        ///     the service will not skip any documents.
        /// </para></param>
        /// <param name="continuationToken">A continuation token returned by a previous operation, can be null.</param>
        /// <returns>Async task, which, upon completion, will return a result segment including the documents of the specific event.</returns>
        Task<Segment<Document>> GetEventDetailSegmentedAsync(string eventId, int? top = null, int? skip = null, string continuationToken = null);

        /// <summary>
        /// Get hot events by date in one time.
        /// </summary>
        /// <param name="date">The specific date to be queried on.</param>
        /// <param name="top">The number of events to be returned. 
        /// <para>E.g., if top is 5, the first 5 events will be returned. If not specified, the service will return all the events on that date.</para></param>
        /// <param name="skip">The number of events to be skipped. 
        /// <para>E.g., if skip is 5, the first 5 events will be skipped. If not specified, the service will not skip any events.</para></param>
        /// <returns>Hot Events on the specific date.</returns>
        IEnumerable<Event> GetHotEvents(DateTimeOffset date, int? top = null, int? skip = null);

        /// <summary>
        /// Get related events by wikipedia id in one time.
        /// </summary>
        /// <param name="wikipediaId">The specific wikipedia id to be queried on. </param>
        /// <param name="top">The number of events to be returned. 
        /// <para>E.g., if top is 5, the first 5 events will be returned. If not specified, the service will return all the events related to that wikipedia id.</para></param>
        /// <param name="skip">The number of events to be skipped. 
        /// <para>E.g., if skip is 5, the first 5 events will be skipped. If not specified, the service will not skip any events.</para></param>
        /// <returns>Related Events of the specific wikipedia id.</returns>
        IEnumerable<Event> GetRelatedEvents(string wikipediaId, int? top = null, int? skip = null);

        /// <summary>
        /// Get related documents of the specific event in one time.
        /// </summary>
        /// <param name="eventId">The specific event id to be queried on. </param>
        /// <param name="top">The number of documents to be returned. 
        /// <para>E.g., if top is 5, the first 5 documents will be returned. If not specified, the service will return all the documents in that event.</para></param>
        /// <param name="skip">The number of documents to be skipped. 
        /// <para>E.g., if skip is 5, the first 5 documents will be skipped. If not specified, the service will not skip any documents.</para></param>
        /// <returns>Related documents of the specific event.</returns>
        IEnumerable<Document> GetEventDetail(string eventId, int? top = null, int? skip = null);
    }
}
