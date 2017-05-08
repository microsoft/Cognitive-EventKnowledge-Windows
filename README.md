# Microsoft EventKnowledge API: Windows Client Library & Sample
This repo contains the Windows client library & sample for the Microsoft
EventKnowledge API, an easy-to-use API designed to help developers find and surface events related to specific entities. Just start with an entity from Wikipedia, and the API will provide a list of related events by time. It can also start with an event and return related news, or start with a date and return trending news related to the date. Additional controls allow you to include a limit on the number of results to return, or exclude specific items from the results.

* [Learn about the EventKnowledge API](https://dev.labs.cognitive.microsoft.com/docs/services/590bf0b54f545a0de4422d3b)
* [Read the documentation](https://labs-wu-ppe.azurewebsites.net/en-us/Project-Cuzco/documentation/overview)
* [Find more SDKs & Samples](https://www.microsoft.com/cognitive-services/en-us/SDK-Sample?api=event%20knowledge)

## The Client Library

The EventKnowledge API client library is a thin C\# client wrapper for
Microsoft EventKnowledge APIs.

The easiest way to use this client library is to get 
microsoft.projectoxford.eventknowledge package from [nuget](https://nuget.org).

Please go to [EventKnowledge API Package in nuget](https://www.nuget.org/packages/Microsoft.ProjectOxford.EventKnowledge/) for more details.

## The Sample


This sample is a Windows WPF application to demonstrate the use of 
Microsoft EventKnowledge API.

It demonstrates EventKnowledge API by specifying a date, using a Wikipedia ID 
or providing an event ID.

### Build the Sample


 1. Starting in the folder where you clone the repository (this folder)
 2. In a git command line tool, type `git submodule init` (or do this through a UI)
 3. Pull in the shared Windows code by calling `git submodule update`
 4. Start Microsoft Visual Studio 2015 and select `File > Open > Project/Solution`.
 5. Go to `Sample-WPF Folder`.
 6. Double-click the Visual Studio 2015 Solution (.sln) file EventKnowledgeAPI-WPF-Samples.
 7. Press Ctrl+Shift+B, or select `Build > Build Solution`.


### Run the Sample


After the build is complete, press F5 to run the sample.

First, you must obtain a EventKnowledge API subscription key by 
[following instructions on our website](https://www.microsoft.com/cognitive-services/en-us/sign-up).

Locate the text edit box saying "Paste your subscription key here to start" on
the top right corner. Paste your subscription key. You can choose to persist
your subscription key in your machine by clicking "Save Key" button. When you
want to delete the subscription key from the machine, click "Delete Key" to
remove it from your machine.

Click on "Select Scenario" to use samples of different scenarios, and
follow the instructions on screen.


## Contributing
We welcome contributions. Feel free to file issues and pull requests on the repo 
and we'll address them as we can. Learn more about how you can help on our 
[Contribution Rules & Guidelines](/CONTRIBUTING.md). 

You can reach out to us anytime with questions and suggestions using our communities below:
 - **Support questions:** [StackOverflow](https://stackoverflow.com/questions/tagged/microsoft-cognitive)
 - **Feedback & feature requests:** [Cognitive Services UserVoice Forum](https://cognitive.uservoice.com)

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). 
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) 
or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.


## License
All Microsoft Cognitive Services SDKs and samples are licensed with the MIT License. 
For more details, see [LICENSE](/LICENSE.md).

Sample images are licensed separately, please refer to [LICENSE-IMAGE](/LICENSE-IMAGE.md)

## Developer Code of Conduct
Developers using Cognitive Services, including this client library & sample, are 
expected to follow the "Developer Code of Conduct for Microsoft Cognitive Services",
found at [http://go.microsoft.com/fwlink/?LinkId=698895](http://go.microsoft.com/fwlink/?LinkId=698895).
