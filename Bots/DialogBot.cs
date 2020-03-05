// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;



namespace Microsoft.BotBuilderSamples.Bots
{
    // This IBot implementation can run any type of Dialog. The use of type parameterization is to allows multiple different bots
    // to be run at different endpoints within the same project. This can be achieved by defining distinct Controller types
    // each with dependency on distinct IBot types, this way ASP Dependency Injection can glue everything together without ambiguity.
    // The ConversationState is used by the Dialog system. The UserState isn't, however, it might have been used in a Dialog implementation,
    // and the requirement is that all BotState objects are saved at the end of a turn.
    public class DialogBot<T> : ActivityHandler
        where T : Dialog
    {
        protected readonly Dialog Dialog;
        protected readonly BotState ConversationState;
        protected readonly BotState UserState;
        protected readonly ILogger Logger;

        public DialogBot(ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
            Logger = logger;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Running dialog with Message Activity.");
            var activity = turnContext.Activity;
            IMessageActivity reply = null;
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Attachments != null && activity.Attachments.Any())
            {
                // We know the user is sending an attachment as there is at least one item

                // in the Attachments list.
                string replyText = string.Empty;
                foreach (var file in activity.Attachments)
                {
                    var imageAttachment = activity.Attachments?.FirstOrDefault(a => a.ContentType.Contains("image"));
                    HttpResponseMessage response;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "789ecf62be6d45388ba246f4229a4cbb");
                    using (var stream = await GetImageStream(connector, imageAttachment))
                    {
                        //return await this.captionService.GetCaptionAsync(stream);
                        byte[] byteresponse = null;

                        using (var binaryReader = new BinaryReader(stream))
                        {
                            byteresponse = binaryReader.ReadBytes(328093);
                        }
                        using (ByteArrayContent content = new ByteArrayContent(byteresponse))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            response = await client.PostAsync("https://atndtocr.cognitiveservices.azure.com/vision/v2.1/ocr" + "?" + "mode=Handwritten", content);
                            //string operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
                            //response = await client.PostAsync(uri, content);
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            
                            //string operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
                            string contentString;
                            int res = 0;
                            do
                            {
                                System.Threading.Thread.Sleep(1000);
                                //response = await client.GetAsync(operationLocation);
                                contentString = await response.Content.ReadAsStringAsync();
                                ++res;
                            }
                            while (res < 2 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1);

                            //if (res == 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1)
                            //{
                            //    //return null;
                            //}
                            // var rootobjet = JsonConvert.DeserializeObject<RootObjec>(contentString);

                            StringBuilder newbuilder3 = new StringBuilder();
                            var rootobject = JsonConvert.DeserializeObject<RootObject>(contentString);

                            var rootobjec = JsonConvert.DeserializeObject<RootObjec>(contentString);
                            dynamic rootob=JsonConvert.DeserializeObject(contentString);
                            
                            if (rootob.regions.Count!=0)
                            {
                                var regio = rootob.regions[0];
                                foreach (var alb in regio.lines)
                                {
                                    var lin = alb.words;
                                    foreach (var linn in lin)
                                    {
                                        var tex = linn.text;
                                        newbuilder3.Append(" ");
                                        newbuilder3.Append(tex.ToString());
                                    }

                                }


                                //StringBuilder stringBuilder = null;
                                string respone = string.Empty;
                                var newbil = response;

                                await turnContext.SendActivityAsync("I understand that ..");
                                await Task.Delay(4000);
                                await turnContext.SendActivityAsync(newbuilder3.ToString());

                                HeroCard plCardissue = new HeroCard()
                                {
                                    Text = "Please make a choice in which area are you looking to assist the customer?",
                                    Buttons = new List<CardAction>
                         {
                                    new CardAction(ActionTypes.PostBack, "No, I would rather type", value: "Please type"),
                                    new CardAction(ActionTypes.PostBack, "Yes, but let me add/correct some words", value: "correct some words"),
                                    new CardAction(ActionTypes.PostBack, "Yes, Perfect! Go ahead", value: "No Updates are required ")
         
                        }
                                };

                                var replyFeedback = MessageFactory.Attachment(plCardissue.ToAttachment());
                                await turnContext.SendActivityAsync(replyFeedback, cancellationToken);
                            }
                            else
                            {
                                await turnContext.SendActivityAsync("Oh! This image sees off the context….Please can you check and upload the issue-screenshot?");
                            }

                        }
                     

                    }



                }
            }
            else
            {
               






                // var reply = ProcessInput(turnContext);
                //await turnContext.SendActivityAsync(reply, cancellationToken);
                // Run the Dialog with the new message Activity.
                await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>("DialogState"), cancellationToken);
            }
           
        }
        private static async Task<Stream> GetImageStream(ConnectorClient connector, Attachment imageAttachment)
        {
            using (var httpClient = new HttpClient())
            {

                var uri = new Uri(imageAttachment.ContentUrl);

                return await httpClient.GetStreamAsync(uri);
            }
        }

        public class RootObject
        {
            public string status { get; set; }
            public List<RecognitionResult> recognitionResult { get; set; }
        }

        public class RootObjec
        {
            
            public List<RecognitionResult> recognitionResult { get; set; }
        }


        public class Word
        {
            public List<int> boundingBox { get; set; }
            public string text { get; set; }
        }

        public class Line
        {
            public List<int> boundingBox { get; set; }
            public string text { get; set; }
            public List<Word> words { get; set; }
        }

        public class RecognitionResult
        {
            public List<Line> lines { get; set; }
        }

        
    }
}
