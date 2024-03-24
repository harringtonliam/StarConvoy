using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.Movement;
using SC.Core;
using UnityEngine.EventSystems;


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

        private void Update()
        {
            if(Input.GetButtonDown("Fire1")  && !EventSystem.current.IsPointerOverGameObject())
            {
                LauchButtonClicked();
            }
        }

        public void LauchButtonClicked()
        {
            uiCanvas.SetActive(false);
            if (PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey) == PlayerSettings.UseMouseSetting)
            {
                CursorControl.Instance.SetNewCursor(CursorType.GameCursor);
            }
            Move playerMove = player.GetComponent<Move>();
            playerMove.SetCurrentSpeed(playerMove.MaxSpeed * launchSpeedFraction);
            
        }

    }



}


