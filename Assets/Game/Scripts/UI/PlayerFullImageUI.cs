using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.Attributes;

namespace SC.UI
{

    public class PlayerFullImageUI : MonoBehaviour
    {

        [SerializeField] Image playerFullImage;


        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerInformationStore playerInformationStore = player.GetComponent<PlayerInformationStore>();

            playerFullImage.sprite = playerInformationStore.PlayerInformation.FullImage;
        }


    }

}


