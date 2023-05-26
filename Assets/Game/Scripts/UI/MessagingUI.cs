using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Messaging;

namespace SC.UI
{
    public class MessagingUI : MonoBehaviour
    {
        [SerializeField] MessageDetailsUI messageDetailsPrefab;
        [SerializeField] float messageDisplayTime = 3f;

        GameObject player;
        MessageReceiver messageReceiver;

        // Start is called before the first frame update
        void Start()
        {

            CleanUpMessages();
            player = GameObject.FindGameObjectWithTag("Player");
            messageReceiver = player.GetComponent<MessageReceiver>();
            messageReceiver.onSameFactionMessageReceived += DisplayMessage;
        }

        private void OnDisable()
        {
            try
            {
                messageReceiver.onSameFactionMessageReceived += DisplayMessage;
            }
            catch (System.Exception ex)
            {
                Debug.Log("MessagingUI unable to unredisger onSameFactionMessageReceived " + ex.Message);
            }
        }

        private void CleanUpMessages()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void DisplayMessage()
        {
            if (messageDetailsPrefab == null) return;

            var newMessage = Instantiate(messageDetailsPrefab, transform);
            var shipDetails = messageReceiver.GetCurrentMessage().shipInformation.GetShipDetails();
            newMessage.SetUp(shipDetails.captainSprite, shipDetails.captainName, shipDetails.shipName, messageReceiver.GetCurrentMessage().message, messageDisplayTime);
       }
    }

}


