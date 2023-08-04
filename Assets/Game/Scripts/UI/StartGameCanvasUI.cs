using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;


namespace SC.UI
{
    public class StartGameCanvasUI : MonoBehaviour
    {

        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] public Texture2D onEnableCursorTexture2D;
        [SerializeField] public Vector2 cursorHotspot;


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
            CursorControl.Instance.SetNewCursor(CursorType.GameCursor);
            SceneController.Instance.StartScene();
        }

        public void QuitButtonClicked()
        {

        }
    }

}


