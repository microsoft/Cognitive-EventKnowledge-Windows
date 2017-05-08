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
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.EventKnowledge.Contract;

namespace Microsoft.ProjectOxford.EventKnowledge
{
    /// <summary>
    /// The EventKnowledge service client proxy implementation.
    /// </summary>
    public class EventKnowledgeServiceClient : ServiceClient, IEventKnowledgeServiceClient
    {
        #region private members

        /// <summary>
        /// The subscription key name.
        /// </summary>
        private const string SubscriptionKeyName = "Ocp-Apim-Subscription-Key";

        /// <summary>
        /// Path string for REST HotEvents method.
        /// </summary>
        private const string HotEventsQuery = "hotevents";

        /// <summary>
        /// Path string for REST RelatedEvents method.
        /// </summary>
        private const string RelatedEventsQuery = "relatedevents";

        /// <summary>
        /// Path string for REST EventDetail method.
        /// </summary>
        private const string EventDetailQuery1 = "events";

        /// <summary>
        /// Path string for REST EventDetail method.
        /// </summary>
        private const string EventDetailQuery2 = "documents";

        /// <summary>
        /// Url of the service root.
        /// </summary>
        private const string ServiceRoot = "https://api.labs.cognitive.microsoft.com";

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventKnowledgeServiceClient"/> class.
        /// </summary>
        /// <param name="subscriptionKey">The subscription key.</param>
        public EventKnowledgeServiceClient(string subscriptionKey) : this(subscriptionKey, ServiceRoot) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventKnowledgeServiceClient"/> class, with a client-supplied
        /// HttpClient object. Intended primarily for testing.
        /// </summary>
        /// <param name="subscriptionKey">The subscription key.</param>
        /// <param name="serviceRoot">The service root.</param>
        public EventKnowledgeServiceClient(string subscriptionKey, string serviceRoot)
        {
            AuthKey = SubscriptionKeyName;
            AuthValue = subscriptionKey;
            ApiRoot = serviceRoot + "/eventknowledge/v1.0";
        }
        #endregion

        #region GetAllOnce implementations

        /// <summary>
        /// Get hot events by date in one time.
        /// </summary>
        /// <param name="date">The specific date to be queried on.</param>
        /// <param name="top">The number of events to be returned. 
        /// <para>E.g., if top is 5, the first 5 events will be returned. If not specified, the service will return all the events on that date.</para></param>
        /// <param name="skip">The number of events to be skipped. 
        /// <para>E.g., if skip is 5, the first 5 events will be skipped. If not specified, the service will not skip any events.</para></param>
        /// <returns>Hot Events on the specific date.</returns>
        public IEnumerable<Event> GetHotEvents(DateTimeOffset date, int? top = null, int? skip = null)
        {
            string nextLink = null;
            do
            {
                var requestUri = string.IsNullOrWhiteSpace(nextLink) ? FormatQuery(HotEventsQuery, "date", GetUtcDate(date), top, skip) : nextLink;

                var result = SyncUtility.RunWithoutSynchronizationContext(() =>
                {
                    var task = GetAsync<string, EventResponse>(requestUri, null).ConfigureAwait(false);
                    return task.GetAwaiter().GetResult();
                });

                nextLink = result.NextLink;
                foreach (var eventItem in result.Value)
                {
                    yield return eventItem;
                }

            } while (!string.IsNullOrWhiteSpace(nextLink));
        }

        /// <summary>
        /// Get related events by wikipedia id in one time.
        /// </summary>
        /// <param name="wikipediaId">The specific wikipedia id to be queried on. </param>
        /// <param name="top">The number of events to be returned. 
        /// <para>E.g., if top is 5, the first 5 events will be returned. If not specified, the service will return all the events related to that wikipedia id.</para></param>
        /// <param name="skip">The number of events to be skipped. 
        /// <para>E.g., if skip is 5, the first 5 events will be skipped. If not specified, the service will not skip any events.</para></param>
        /// <returns>Related Events of the specific wikipedia id.</returns>
        public IEnumerable<Event> GetRelatedEvents(string wikipediaId, int? top = null, int? skip = null)
        {
            if (string.IsNullOrWhiteSpace(wikipediaId))
            {
                throw new ArgumentNullException(nameof(wikipediaId));
            }

            string nextLink = null;
            do
            {
                var requestUri = string.IsNullOrWhiteSpace(nextLink) ? FormatQuery(RelatedEventsQuery, "wikipediaid", wikipediaId, top, skip) : nextLink;

                var result = SyncUtility.RunWithoutSynchronizationContext(() =>
                {
                    var task = GetAsync<string, EventResponse>(requestUri, null).ConfigureAwait(false);
                    return task.GetAwaiter().GetResult();
                });


                nextLink = result.NextLink;
                foreach (var eventItem in result.Value)
                {
                    yield return eventItem;
                }

            } while (!string.IsNullOrWhiteSpace(nextLink));
        }

        /// <summary>
        /// Get related documents of the specific event in one time.
        /// </summary>
        /// <param name="eventId">The specific event id to be queried on. </param>
        /// <param name="top">The number of documents to be returned. 
        /// <para>E.g., if top is 5, the first 5 documents will be returned. If not specified, the service will return all the documents in that event.</para></param>
        /// <param name="skip">The number of documents to be skipped. 
        /// <para>E.g., if skip is 5, the first 5 documents will be skipped. If not specified, the service will not skip any documents.</para></param>
        /// <returns>Related documents of the specific event.</returns>
        public IEnumerable<Document> GetEventDetail(string eventId, int? top = null, int? skip = null)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new ArgumentNullException(nameof(eventId));
            }

            string nextLink = null;
            do
            {
                var queryPath = new StringBuilder();
                queryPath.AppendFormat("{0}/{1}/{2}", EventDetailQuery1, Uri.EscapeDataString(eventId), EventDetailQuery2);
                var requestUri = string.IsNullOrWhiteSpace(nextLink) ? FormatQuery(queryPath.ToString(), "eventid", eventId, top, skip) : nextLink;

                var result = SyncUtility.RunWithoutSynchronizationContext(() =>
                {
                    var task = GetAsync<string, DocumentResponse>(requestUri, null).ConfigureAwait(false);
                    return task.GetAwaiter().GetResult();

                });

                nextLink = result.NextLink;
                foreach (var docItem in result.Value)
                {
                    yield return docItem;
                }

            } while (!string.IsNullOrWhiteSpace(nextLink));
        }

        #endregion

        #region Segmented implementations

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
        public async Task<Segment<Event>> GetHotEventsSegmentedAsync(DateTimeOffset date, int? top = null, int? skip = null, string continuationToken = null)
        {
            EventResponse result;
            if (continuationToken != null)
            {
                result = await GetAsync<string, EventResponse>(continuationToken, null);

            }
            else
            {
                var requestUri = FormatQuery(HotEventsQuery, "date", GetUtcDate(date), top, skip);
                result = await GetAsync<string, EventResponse>(requestUri, null);
            }
            var segment = new Segment<Event>(result.Value, result.NextLink);
            return segment;
        }

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
        public async Task<Segment<Event>> GetRelatedEventsSegmentedAsync(string wikipediaId, int? top = null, int? skip = null, string continuationToken = null)
        {
            if (string.IsNullOrWhiteSpace(wikipediaId) && string.IsNullOrWhiteSpace(continuationToken))
            {
                throw new ArgumentNullException(nameof(wikipediaId));
            }

            EventResponse result;
            if (continuationToken != null)
            {
                result = await GetAsync<string, EventResponse>(continuationToken, null);

            }
            else
            {
                var requestUri = FormatQuery(RelatedEventsQuery, "wikipediaid", wikipediaId, top, skip);
                result = await GetAsync<string, EventResponse>(requestUri, null);
            }
            var segment = new Segment<Event>(result.Value, result.NextLink);
            return segment;
        }

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
        public async Task<Segment<Document>> GetEventDetailSegmentedAsync(string eventId, int? top = null, int? skip = null, string continuationToken = null)
        {
            if (string.IsNullOrWhiteSpace(eventId) && string.IsNullOrWhiteSpace(continuationToken))
            {
                throw new ArgumentNullException(nameof(eventId));
            }

            DocumentResponse result;
            if (continuationToken != null)
            {
                result = await GetAsync<string, DocumentResponse>(continuationToken, null);

            }
            else
            {
                var queryPath = new StringBuilder();
                queryPath.AppendFormat("{0}/{1}/{2}", EventDetailQuery1, Uri.EscapeDataString(eventId), EventDetailQuery2);
                var requestUri = FormatQuery(queryPath.ToString(), "eventid", eventId, top, skip);
                result = await GetAsync<string, DocumentResponse>(requestUri, null);
            }
            var segment = new Segment<Document>(result.Value, result.NextLink);
            return segment;
        }
        #endregion

        #region helper methods

        /// <summary>
        /// Helper function to format query
        /// </summary>
        /// <param name="queryPath">The query path of a specific API.</param>
        /// <param name="requiredParamName">The name of the specific API's required paramter.</param>
        /// <param name="requiredParamValue">The value of the specific API's required paramter.</param>
        /// <param name="top">The top parameter to indicate how many items should be returned.</param>
        /// <param name="skip">The skip parameter to indicate how many items should be skipped.</param>
        /// <returns></returns>
        private string FormatQuery(string queryPath, string requiredParamName, string requiredParamValue, int? top, int? skip)
        {
            var query = new StringBuilder();
            query.AppendFormat(CultureInfo.InvariantCulture, "/{0}", queryPath);

            if (!requiredParamName.Equals("eventid", StringComparison.OrdinalIgnoreCase))
            {
                query.AppendFormat(CultureInfo.InvariantCulture, "?{0}={1}", requiredParamName, Uri.EscapeDataString(requiredParamValue));
                if (top != null || skip != null)
                {
                    query.Append("&");
                }
            }
            else if (requiredParamName.Equals("eventid", StringComparison.OrdinalIgnoreCase) && (top != null || skip != null))
            {
                query.Append("?");
            }

            if (top != null && skip != null)
            {
                query.AppendFormat(CultureInfo.InvariantCulture, "$top={0}&$skip={1}", top, skip);
            }
            else if (top != null)
            {
                query.AppendFormat(CultureInfo.InvariantCulture, "$top={0}", top);
            }
            else if (skip != null)
            {
                query.AppendFormat(CultureInfo.InvariantCulture, "$skip={0}", skip);
            }

            return query.ToString();
        }

        /// <summary>
        /// Convert a DateTimeOffset input to UTC date.
        /// </summary>
        /// <param name="date">The datetime to be converted.</param>
        /// <returns>An UTC date string.</returns>
        private string GetUtcDate(DateTimeOffset date)
        {
            return date.UtcDateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
