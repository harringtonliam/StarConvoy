using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SC.Attributes;

namespace SC.UI
{
    public class PlayerDetailsContainerUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI captainNameText;
        [SerializeField] Image playerPortraitImage;

        GameObject player;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerInformationStore playerInformationStore = player.GetComponent<PlayerInformationStore>();
            captainNameText.text = playerInformationStore.PlayerName;
            playerPortraitImage.sprite = playerInformationStore.PlayerInformation.Portrait;
        }


    }

}


