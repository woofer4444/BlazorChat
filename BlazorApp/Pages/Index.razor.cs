using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class Index : ComponentBase, IDisposable
    {
        private delegate void MessageHandler(Chat message);
        private static event MessageHandler MessageEvent;

        public string Name { get; set; }
        public string Message { get; set; }
        private List<Chat> Messages = new List<Chat>();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MessageEvent += OnMessageRecieved;
        }

        public void Dispose()
        {
            MessageEvent -= OnMessageRecieved;
        }

        private void OnMessageRecieved(Chat message)
        {
            Messages.Add(message);
            InvokeAsync(StateHasChanged);
        }

        private void SendMessage()
        {
            if (Message != null)
            {
                MessageEvent?.Invoke(new Chat(Name, Message));
                Message = null;
            }
        }
    }

    public class Chat
    {
        public Chat(string sender, string message)
        {
            Sender = sender;
            Message = message;
        }

        public string Sender { get; set; }
        public string Message { get; set; }
    }
}