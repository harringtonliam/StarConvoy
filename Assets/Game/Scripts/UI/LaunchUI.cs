using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.Movement;


namespace SC.UI
{
    public class LaunchUI : MonoBehaviour
    {
        [SerializeField] Button lauchButton;
        [SerializeField] GameObject uiCanvas;
        [SerializeField] float launchSpeedFraction = 0.75f;

        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            lauchButton.onClick.AddListener(LauchButtonClicked);
            player = GameObject.FindGameObjectWithTag("Player");
            ShowStartGameUI();
        }


        private void ShowStartGameUI()
        {
            uiCanvas.SetActive(true);
        }

        public void LauchButtonClicked()
        {
            uiCanvas.SetActive(false);
            Move playerMove = player.GetComponent<Move>();
            playerMove.SetCurrentSpeed(playerMove.MaxSpeed * launchSpeedFraction);
            
        }

    }



}


