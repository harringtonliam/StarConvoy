using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SC.Messaging
{


    public class MessageReceiver : MonoBehaviour
     {
        string currentMessage;

        public string GetCurrentMessage()
        {
            return currentMessage;
        }
        
        public event Action onMessageReceived;


        public void  ReceiveMessage(string message)
        {
            currentMessage = message;
            NotifyMessageReceived();
        }

        private void NotifyMessageReceived()
        {
            if (onMessageReceived != null)
            {
                onMessageReceived();
            }
        }
    }

}



