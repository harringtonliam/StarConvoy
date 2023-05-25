using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;

namespace SC.Messaging
{
    public class MessageSender : MonoBehaviour
    {
        [SerializeField] MessageReceiver messageReceiver;
        [SerializeField] float timeToWaitBetweenMessages = 30f;
        [SerializeField] AvailableMessage[] availableMessages;

        [System.Serializable]
        public struct AvailableMessage
        {
            public MessageType messageType;
            public string messageText;
            public float timeSinceLastMessage;
        }

        Health health;

        private void Start()
        {
            if (messageReceiver == null)
            {
                messageReceiver = GameObject.FindGameObjectWithTag("Player").GetComponent<MessageReceiver>();
            }
            health = GetComponent<Health>();
            health.onDeath += TriggerDeathMessage;
            health.healthUpdated += TriggerDamageMessage;
        }

        private void Update()
        {
            UpdateTimeBetweenMessages();
        }

        private void OnDisable()
        {
            health.onDeath -= TriggerDeathMessage;
            health.healthUpdated -= TriggerDamageMessage;
        }

        private void TriggerDeathMessage()
        {
            TriggerSendMessageProcess(MessageType.Destroyed);
        }

        private void TriggerDamageMessage()
        {
            Debug.Log("Damage message triggered " + gameObject.name);
            TriggerSendMessageProcess(MessageType.DamageTaken);
        }

        public void TriggerSendMessageProcess(MessageType messageType)
        {
            AvailableMessage messageToSend = GetAvailableMessage(messageType);
            if (messageToSend.messageType != messageType) return;
            if (messageToSend.timeSinceLastMessage < timeToWaitBetweenMessages) return;
            messageReceiver.ReceiveMessage(gameObject.name + ": " + messageToSend.messageText);
            messageToSend.timeSinceLastMessage = 0f;
        }

        private AvailableMessage GetAvailableMessage(MessageType messageType)
        {
            foreach (var availableMessage in availableMessages)
            {
                if (availableMessage.messageType == messageType)
                {
                    return availableMessage;
                }
            }

            return availableMessages[0];
        }

        private void UpdateTimeBetweenMessages()
        {
            for (int i = 0; i < availableMessages.Length; i++)
            {
                availableMessages[i].timeSinceLastMessage += Time.deltaTime;
            }
        }

    }
}


