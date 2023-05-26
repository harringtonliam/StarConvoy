using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SC.Messaging;

namespace SC.UI
{
    public class MessageDetailsUI : MonoBehaviour
    {
        [SerializeField] Image speakerImage;
        [SerializeField] TextMeshProUGUI speakerNameText;
        [SerializeField] TextMeshProUGUI shipNameText;
        [SerializeField] TextMeshProUGUI messageText;
        [SerializeField] float messageDisplayTime;

        float timeMessageDisplayed = 0f;

        public void SetUp(Sprite messageImage, string speakerName, string shipName, string message, float displayTime)
        {
            this.speakerImage.sprite = messageImage;
            speakerNameText.text = speakerName;
            shipNameText.text = shipName;
            messageText.text = message;
            messageDisplayTime = displayTime;
        }

        // Start is called before the first frame update
        void Start()
        {
            timeMessageDisplayed = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            timeMessageDisplayed += Time.deltaTime;
            CheckMessageTimeHasElapsed();
        }

        private void CheckMessageTimeHasElapsed()
        {
            if (timeMessageDisplayed > messageDisplayTime)
            {
                Destroy(gameObject);
            }

        }
    }

}


