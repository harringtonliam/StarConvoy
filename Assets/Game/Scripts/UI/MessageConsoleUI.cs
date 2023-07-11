using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Messaging;
using TMPro;
using System;

namespace SC.UI
{
    public class MessageConsoleUI : MonoBehaviour
    {
        [SerializeField] float messageDisplayTime = 3f;
        [SerializeField] TextMeshProUGUI messageText;

        GameObject player;
        MessageReceiver messageReceiver;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            messageReceiver = player.GetComponent<MessageReceiver>();
            messageReceiver.onMessageReceived += StartDisplayMessage;
            messageText.text = string.Empty;
        }

        private void OnDisable()
        {
            try
            {
                messageReceiver.onMessageReceived -= StartDisplayMessage;
            }
            catch (Exception ex)
            {
                Debug.Log("MessageConsoleUI OnDisable " + ex.Message);
            }

        }

        private void StartDisplayMessage()
        {
            StartCoroutine(DisplayMessage());
        }

        private IEnumerator DisplayMessage()
        {
            messageText.text = messageReceiver.GetCurrentMessage().shipInformation.GetShipDetails().shipName + ": " + messageReceiver.GetCurrentMessage().message;
            yield return new WaitForSeconds(messageDisplayTime);
            messageText.text = string.Empty;
        }
    }

}


