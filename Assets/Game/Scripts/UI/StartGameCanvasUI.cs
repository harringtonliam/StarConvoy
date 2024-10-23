using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;
using SC.Core;


namespace SC.UI
{
    public class StartGameCanvasUI : MonoBehaviour
    {

        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] public Texture2D onEnableCursorTexture2D;
        [SerializeField] public Vector2 cursorHotspot;
        [SerializeField] TutotialUI tutorialUI;


        GameObject player;

        void OnEnable()
        {
            SceneController.Instance.onSceneStarted += ShowStartGameUI;

        }

        private void OnDisable()
        {
            SceneController.Instance.onSceneStarted -= ShowStartGameUI;

        }

        private void Start()
        {
            ShowStartGameUI();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void ShowStartGameUI()
        {
            CursorControl.Instance.SetNewCursor(CursorType.UICursor);
            uiCanvas.SetActive(true);
        }

        public void ToggleUI()
        {
            uiCanvas.SetActive(!uiCanvas.activeSelf);
        }

        public void StartButtonClicked()
        {
            ToggleUI();
            if(PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey)== PlayerSettings.UseMouseSetting)
            {
                CursorControl.Instance.SetNewCursor(CursorType.GameCursor);
            }

            if (tutorialUI != null)
            {
                tutorialUI.ShowHideUI(true);
            }
            else
            {
                SceneController.Instance.StartScene();
                StartCoroutine(CentreTheCursor());
            }    


        }

        public void QuitButtonClicked()
        {
            SceneController.Instance.LoadMainMenu();
        }

        private IEnumerator CentreTheCursor()
        {
            Debug.Log("CentreTheCursor");
            Cursor.lockState = CursorLockMode.Locked;
            yield return null;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}


