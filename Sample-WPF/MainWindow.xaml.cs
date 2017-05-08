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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.ProjectOxford.EventKnowledge.Contract;
using SampleUserControlLibrary;

namespace EventKnowledgeAPI_WPF_Samples
{
    //the definition for event result listview item
    public class EventResultDisplayItem
    {
        public string EventId
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }

        public string RelatedEntities
        {
            get;
            set;
        }
    }

    //the definition for document result listview item
    public class DocumentResultDisplayItem
    {
        public string DocumentId
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }

        public string Entities
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SampleScenarios ScenarioControl
        {
            get
            {
                return _scenariosControl;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            //
            // Initialize SampleScenarios User Control with titles and scenario pages
            //
            _scenariosControl.SampleTitle = "EventKnowledge API";
            _scenariosControl.SampleScenarioList = new Scenario[]
            {
                new Scenario { Title = "Get hot events by date", PageClass = typeof(EventKnowledgeAPI_WPF_Samples.GetHotEventsPage)},
                new Scenario { Title = "Get related events by Wikipedida ID", PageClass = typeof(EventKnowledgeAPI_WPF_Samples.GetRelatedEventsPage) },
                new Scenario { Title = "Get documents by event ID", PageClass = typeof(EventKnowledgeAPI_WPF_Samples.GetEventDetailPage) }
            };
        }

        public void Log(string message)
        {
            _scenariosControl.Log(message);
        }

        //log the event retrieving process in log box
        public void LogEventResult(IEnumerable<Event> eventsResult)
        {
            int eventsResultCount = 0;
            if (eventsResult != null && eventsResult.Count() > 0)
            {
                foreach (Event eventItem in eventsResult)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    sb.AppendLine("Event[" + eventsResultCount + "] {");

                    sb.AppendLine("     EventID: " + eventItem.EventId);
                    sb.AppendLine("     Related Entities[" + eventItem.RelatedEntities.Count() + "]");
                    for (int i = 0; i < eventItem.RelatedEntities.Count(); i++)
                    {
                        sb.AppendLine("          " + i + ": " + eventItem.RelatedEntities[i]);
                    }

                    sb.AppendLine("     From:  " + eventItem.From);
                    sb.AppendLine("     To:    " + eventItem.To);
                    sb.AppendLine("     LatestDocument");
                    sb.AppendLine("          Title: " + eventItem.LatestDocument.Title);
                    sb.AppendLine("          DocumentID: " + eventItem.LatestDocument.DocumentId);
                    sb.AppendLine("          Source: " + eventItem.LatestDocument.Source);
                    sb.AppendLine("          Publish Date: " + eventItem.LatestDocument.PublishDate);

                    sb.AppendLine("          URL: " + eventItem.LatestDocument.Url);
                    sb.AppendLine("          Entities[" + eventItem.LatestDocument.Entities.Count() + "]");
                    for (int i = 0; i < eventItem.LatestDocument.Entities.Count(); i++)
                    {
                        sb.AppendLine("               " + i + ": " + eventItem.LatestDocument.Entities[i]);
                    }
                    sb.AppendLine("          Summary:" + eventItem.LatestDocument.Summary);
                    sb.AppendLine(" }");
                    sb.AppendLine();
                    Log(sb.ToString());
                    eventsResultCount++;
                }
            }
            else
            {
                Log("No Result is retrieved. ");
            }
        }

        //list the event result in listview
        public void ListEventResult(ListBox resultListBox, Event[] eventsResult)
        {
            if (eventsResult != null)
            {
                
                List<EventResultDisplayItem> itemSource = new List<EventResultDisplayItem>();
                for (int i = 0; i < eventsResult.Length; i++)
                {
                    itemSource.Add(new EventResultDisplayItem
                    {
                        EventId = "Event ID: " + eventsResult[i].EventId,
                        Title =   "Title: " + eventsResult[i].LatestDocument.Title,
                        RelatedEntities = "Related Entities: " + string.Join(",", eventsResult[i].RelatedEntities)
                    });
                }
                resultListBox.ItemsSource = itemSource;
            }
        }

        //display the selected list item in text box
        public string DisplaySelectedEvent(int index, Event[] eventsResult)
        {
            StringBuilder sb = new StringBuilder();
            if (eventsResult != null && eventsResult.Any())
            {
                var eventItem = eventsResult[index];
                sb.AppendLine("Event[" + index + "] {");

                sb.AppendLine("     EventID: " + eventItem.EventId);
                sb.AppendLine("     Related Entities[" + eventItem.RelatedEntities.Count() + "]");
                for (int i = 0; i < eventItem.RelatedEntities.Count(); i++)
                {
                    sb.AppendLine("          " + i + ": " + eventItem.RelatedEntities[i]);
                }

                sb.AppendLine("     From:  " + eventItem.From);
                sb.AppendLine("     To:    " + eventItem.To);
                sb.AppendLine("     LatestDocument");
                sb.AppendLine("          Title: " + eventItem.LatestDocument.Title);
                sb.AppendLine("          DocumentID: " + eventItem.LatestDocument.DocumentId);
                sb.AppendLine("          Source: " + eventItem.LatestDocument.Source);
                sb.AppendLine("          Publish Date: " + eventItem.LatestDocument.PublishDate);
                
                sb.AppendLine("          URL: " + eventItem.LatestDocument.Url);
                sb.AppendLine("          Entities[" + eventItem.LatestDocument.Entities.Count() + "]");
                for (int i = 0; i < eventItem.LatestDocument.Entities.Count(); i++)
                {
                    sb.AppendLine("               " + i + ": " + eventItem.LatestDocument.Entities[i]);
                }
                sb.AppendLine("          Summary: " + eventItem.LatestDocument.Summary);
                sb.AppendLine(" }");
                
            }
            return sb.ToString();
        }

        //log the document retrieving process in log box
        public void LogDocumentResult(IEnumerable<Document> docsResult)
        {
            int docsResultCount = 0;
            if (docsResult != null && docsResult.Any())
            {
                foreach (Document docItem in docsResult)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    sb.AppendLine("Document[" + docsResultCount + "] {");
                    sb.AppendLine("     DocumentID: " + docItem.DocumentId);
                    sb.AppendLine("     Title: " + docItem.Title);
                    sb.AppendLine("     Source: " + docItem.Source);
                    sb.AppendLine("     URL: " + docItem.Url);
                    sb.AppendLine("     Publish Date: " + docItem.PublishDate);
                    sb.AppendLine("     Summary: " + docItem.Summary);
                    sb.AppendLine("     Entities[" + docItem.Entities.Count() + "]");
                    for (int i = 0; i < docItem.Entities.Count(); i++)
                    {
                        sb.AppendLine("          " + i + ": " + docItem.Entities[i]);
                    }
                    sb.AppendLine(" }");
                    sb.AppendLine();
                    Log(sb.ToString());
                    docsResultCount++;
                }
            }
            else
            {
                Log("No Result is retrieved. ");
            }
        }

        //list the document result in listview
        public void ListDocumentResult(ListBox resultListBox, Document[] documentsResult)
        {
            if (documentsResult != null)
            {

                List<DocumentResultDisplayItem> itemSource = new List<DocumentResultDisplayItem>();
                for (int i = 0; i < documentsResult.Length; i++)
                {
                    itemSource.Add(new DocumentResultDisplayItem
                    {
                        DocumentId = "Document ID: " + documentsResult[i].DocumentId,
                        Title =      "Title: " + documentsResult[i].Title,
                        Entities =   "Entities: " + string.Join(",", documentsResult[i].Entities)
                    });
                }
                resultListBox.ItemsSource = itemSource;
            }
        }

        //display the selected list item in text box
        public string DisplaySelectedDocument(int index, Document[] documentsResult)
        {
            StringBuilder sb = new StringBuilder();
            if (documentsResult != null && documentsResult.Any())
            {
                var docItem = documentsResult[index];
                sb.AppendLine("Document[" + index + "] {");
                sb.AppendLine("     DocumentID: " + docItem.DocumentId);
                sb.AppendLine("     Title: " + docItem.Title);
                sb.AppendLine("     Source: " + docItem.Source);
                sb.AppendLine("     URL: " + docItem.Url);
                sb.AppendLine("     Publish Date: " + docItem.PublishDate);
                sb.AppendLine("     Summary: " + docItem.Summary);
                sb.AppendLine("     Entities[" + docItem.Entities.Count() + "]");
                for (int i = 0; i < docItem.Entities.Count(); i++)
                {
                    sb.AppendLine("          " + i + ": " + docItem.Entities[i]);
                }
                sb.AppendLine(" }");

            }
            return sb.ToString();
        }
    }
}
