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
        [SerializeField] bool isShownAtSceneStart = false;

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
            CursorControl.Instance.SetNewCursor(CursorType.UICursor);
            uiCanvas.SetActive(isShownAtSceneStart);
        }

        public void LauchButtonClicked()
        {
            uiCanvas.SetActive(false);
            CursorControl.Instance.SetNewCursor(CursorType.GameCursor);
            Move playerMove = player.GetComponent<Move>();
            playerMove.SetCurrentSpeed(playerMove.MaxSpeed * launchSpeedFraction);
            
        }

    }



}


