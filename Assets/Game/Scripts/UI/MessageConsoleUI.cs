using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.Messaging;
using SC.Combat;
using TMPro;
using System;

namespace SC.UI
{
    public class MessageConsoleUI : MonoBehaviour
    {
        [SerializeField] float messageDisplayTime = 3f;
        [SerializeField] TextMeshProUGUI messageText;
        [SerializeField] TextMeshProUGUI redTextMessage;
        [SerializeField] string damageMessage = "Damage Taken";
        [SerializeField] Image messageBackgroundImage;

        GameObject player;
        MessageReceiver messageReceiver;
        Health playerHealth;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            messageReceiver = player.GetComponent<MessageReceiver>();
            messageReceiver.onMessageReceived += StartDisplayMessage;
            playerHealth = player.GetComponent<Health>();
            playerHealth.healthUpdated += StartDisplayDamageMessage;
            messageText.text = string.Empty;
            redTextMessage.text = string.Empty;
        }

        public void StartErrorProcedure()
        {
            messageBackgroundImage.color = Color.red;
            StartCoroutine(DisplayErrorMessage());
        }

        private IEnumerator DisplayErrorMessage()
        {
            bool isErrorProcedureRunning = true;

            while (isErrorProcedureRunning)
            {
                messageText.text = "System Error";
                yield return new WaitForSeconds(1f);
                messageText.text = string.Empty;
                yield return new WaitForSeconds(1f);
            }
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

        private void StartDisplayDamageMessage()
        {
            StartCoroutine(DisplayDamageMessage());

        }

        private IEnumerator DisplayDamageMessage()
        {
            messageText.text = string.Empty;
            redTextMessage.text = damageMessage;
            yield return new WaitForSeconds(messageDisplayTime);
            redTextMessage.text = string.Empty;
        }

        private void StartDisplayMessage()
        {
            StartCoroutine(DisplayMessage());
        }

        private IEnumerator DisplayMessage()
        {
            redTextMessage.text = string.Empty;
            messageText.text = messageReceiver.GetCurrentMessage().shipInformation.GetShipDetails().shipName + ": " + messageReceiver.GetCurrentMessage().message;
            yield return new WaitForSeconds(messageDisplayTime);
            messageText.text = string.Empty;
        }
    }

}


