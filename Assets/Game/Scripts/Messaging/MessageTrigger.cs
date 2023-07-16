using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Messaging
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField] MessageType messageType;
        [SerializeField] float sendAfterSeconds;


        MessageSender messageSender;

        // Start is called before the first frame update
        void Start()
        {
            messageSender = GetComponent<MessageSender>();
            StartCoroutine(TriggerMessage());
        }


        IEnumerator TriggerMessage()
        {
            yield return new WaitForSeconds(sendAfterSeconds);
            messageSender.TriggerSendMessageProcess(messageType);
        }
    }

}


