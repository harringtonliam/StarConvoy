using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SC.Combat;
using SC.Attributes;

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
        ShipInformation shipInformation;
        CombatTarget combatTarget;

        private void Start()
        {
            if (messageReceiver == null)
            {
                messageReceiver = GameObject.FindGameObjectWithTag("Player").GetComponent<MessageReceiver>();
            }
            health = GetComponent<Health>();
            shipInformation = GetComponent<ShipInformation>();
            combatTarget = GetComponent<CombatTarget>();
            health.onDeath += TriggerDeathMessage;
            health.healthUpdated += TriggerDamageMessage;
        }

        private void Update()
        {
            UpdateTimeBetweenMessages();
        }

        private void OnDisable()
        {
            try
            {
                health.onDeath -= TriggerDeathMessage;
                health.healthUpdated -= TriggerDamageMessage;
            }
            catch (Exception ex)
            {
                Debug.Log("MessageSender OnDisable" + ex.Message);
            }

        }

        private void TriggerDeathMessage()
        {
            TriggerSendMessageProcess(MessageType.Destroyed);
        }

        private void TriggerDamageMessage()
        {
            TriggerSendMessageProcess(MessageType.DamageTaken);
        }

        public void TriggerSendMessageProcess(MessageType messageType)
        {
            AvailableMessage messageToSend = GetAvailableMessage(messageType);
            if (messageToSend.messageType != messageType) return;
            if (messageToSend.timeSinceLastMessage < timeToWaitBetweenMessages) return;
            messageReceiver.ReceiveMessage(shipInformation, combatTarget.GetFaction(), messageToSend.messageText);
            ResetTimeSinceLastMessage(messageType);
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

            return new AvailableMessage();
        }

        private void ResetTimeSinceLastMessage(MessageType messageType)
        {
            for (int i = 0; i < availableMessages.Length; i++)
            {
                if (availableMessages[i].messageType == messageType)
                {
                    availableMessages[i].timeSinceLastMessage = 0f;
                }
            }
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


