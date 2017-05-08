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
using System.Windows;
using System.Windows.Controls;
using Microsoft.ProjectOxford.EventKnowledge.Contract;

namespace EventKnowledgeAPI_WPF_Samples.UserControls
{
    /// <summary>
    /// Interaction logic for DocumentnUserControl.xaml
    /// </summary>
    public partial class DocumentUserControl : UserControl
    {
        
        public static readonly DependencyProperty DocumentsProperty =
            DependencyProperty.Register(
                "Documents",
                typeof(Document[]),
                typeof(DocumentUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(DocumentsChangedCallback)));


        public DocumentUserControl()
        {
            InitializeComponent();
            DataContext = this;
        }


        public Document[] Documents
        {
            get
            {
                return (Document[])GetValue(DocumentsProperty);
            }
            

            set
            {
                try
                {
                    SetValue(DocumentsProperty, value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
            }
        }

       private static void DocumentsChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs eventArg)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;

            if (window != null)
            {
                DocumentUserControl userControl = obj as DocumentUserControl;

                if (userControl != null)
                {
                    window.ListDocumentResult(userControl.ResultListBox, userControl.Documents);
                }
            }
        }

       
        private void listbox_SlectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ResultListBox.SelectedIndex;
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            if (window != null)
            {
                TextBox.Text = window.DisplaySelectedDocument(index, Documents);
            }
        }
    }
}
