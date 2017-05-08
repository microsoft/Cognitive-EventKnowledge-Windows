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
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.EventKnowledge;
using Microsoft.ProjectOxford.EventKnowledge.Contract;

namespace EventKnowledgeAPI_WPF_Samples
{
    /// <summary>
    /// Interaction logic for GetEventDetailPage.xaml
    /// </summary>
    public partial class GetEventDetailPage : Page
    {
        private string _continuationToken;

        public GetEventDetailPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Send the event id to Project Oxford and get the related documents of that specific event.
        /// </summary>
        /// <param name="eventId">The event id to be sent to service.</param>
        /// <param name="continuToken">The continuation token to continue to get more results.</param>
        /// <returns>The documents related to the specific event.</returns>
        private async Task<Segment<Document>> SendAndGetEventDetail(string eventId, string continuToken = null)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            string subscriptionKey = window.ScenarioControl.SubscriptionKey;

            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE STARTS HERE
            // -----------------------------------------------------------------------

            window.Log("EventKnowledgeServiceClient is created");

            //
            // Create Project Oxford EventKnowledge API Service client
            //
            EventKnowledgeServiceClient eventKnowledgeServiceClient = new EventKnowledgeServiceClient(subscriptionKey);

            window.Log("Calling EventKnowledgeServiceClient.GetEventDetail()...");

            //
            // Get the event detail by event id
            //
            Segment<Document> getEventDetailResult = await eventKnowledgeServiceClient.GetEventDetailSegmentedAsync(eventId, continuationToken: continuToken);
            return getEventDetailResult;

            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE ENDS HERE
            // -----------------------------------------------------------------------

        }

        //The handler for ok button click event
        private async void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            try
            {
                ClearContent();
                string eventIdString = EventIdTextBox.Text;

                _retrieveStatus.Text = "Retrieving results...";

                Segment<Document> eventDetailResult = await SendAndGetEventDetail(eventIdString);

                LogAndUpdateResult(eventDetailResult);
            }
            catch (ClientException exception)
            {
                var error = exception.Error;

                if (error != null)
                {
                    window.Log($"Client Error\n{error.Code} - {error.Message}");
                }
                else
                {
                    window.Log(exception.ToString());
                }
            }
            catch (Exception exception)
            {
                window.Log("Retrieving result failed. Please make sure that you have the right subscription key.");
                window.Log(exception.Message);
            }
            finally
            {
                _retrieveStatus.Text = "Results Retrieval Done";
            }

        }

        //The handler for hyperlink click event
        private async void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            try
            {
                ClearContent();
                _retrieveStatus.Text = "Retrieving results...";

                Segment<Document> eventDetailResult = await SendAndGetEventDetail(string.Empty, _continuationToken);

                LogAndUpdateResult(eventDetailResult);
            }
            catch (ClientException exception)
            {
                var error = exception.Error;

                if (error != null)
                {
                    window.Log($"Client Error\n{error.Code} - {error.Message}");
                }
                else
                {
                    window.Log(exception.ToString());
                }
            }
            catch (Exception exception)
            {
                window.Log("Retrieving result failed. Please make sure that you have the right subscription key.");
                window.Log(exception.Message);
            }
            finally
            {
                _retrieveStatus.Text = "Results Retrieval Done";
            }
        }

        //log the retrieving result and update the document listview
        private void LogAndUpdateResult(Segment<Document> eventDetailResult)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;

            //
            // Log retrieving result in the log window
            //
            window.Log("");
            window.Log("Get EventDetail Result:");
            var docuemntsResult = eventDetailResult.Result.ToArray();
            window.LogDocumentResult(docuemntsResult);

            _documentUserControl.Documents = docuemntsResult;
            HyperLinkText.Text = eventDetailResult.ContinuationToken != null ? "Viem More Results." : "";
            _continuationToken = eventDetailResult.ContinuationToken;
        }

        //clear the content in log listview and text box
        private void ClearContent()
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            window.ScenarioControl.ClearLog();
            _documentUserControl.TextBox.Clear();
            HyperLinkText.Text = string.Empty;
            _documentUserControl.Documents = new Document[]{};
        }
    }
}
