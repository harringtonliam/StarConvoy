using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SC.Attributes;
using SC.Combat;

namespace SC.Messaging
{
    public class MessageReceiver : MonoBehaviour
     {

        CombatTarget combatTarget;

        public struct MessageDetails
        {
            public ShipInformation shipInformation;
            public Faction faction;
            public string message;
        }

        MessageDetails currentMessage;

        private void Start()
        {
            combatTarget = GetComponent<CombatTarget>();
            currentMessage = new MessageDetails();
        }

        public MessageDetails GetCurrentMessage()
        {
            return currentMessage;
        }
        
        public event Action onMessageReceived;
        public event Action onSameFactionMessageReceived;

        public void  ReceiveMessage(ShipInformation shipInformation, Faction faction,  string message)
        {
            Debug.Log("ReceiveMessage " + message + " " + faction.ToString());
            currentMessage.shipInformation = shipInformation;
            currentMessage.faction = faction;
            currentMessage.message = message;
            if (combatTarget == null)
            {
                combatTarget = GetComponent<CombatTarget>();
            }
            if (combatTarget.GetFaction() == faction)
            {
                NotifySameFactionMessageReceived();
            }
            else
            {
                NotifyMessageReceived();
            }

        }

        private void NotifyMessageReceived()
        {
            if (onMessageReceived != null)
            {
                onMessageReceived();
            }
        }

        private void NotifySameFactionMessageReceived()
        {
            if (onSameFactionMessageReceived != null)
            {
                Debug.Log("onSameFactionMessageReceived ");
                onSameFactionMessageReceived();
            }
        }
    }

}



