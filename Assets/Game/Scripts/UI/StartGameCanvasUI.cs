using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;

namespace SC.UI
{
    public class StartGameCanvasUI : MonoBehaviour
    {

        [SerializeField] GameObject uiCanvas = null;


        // Start is called before the first frame update
        void OnEnable()
        {
            SceneController.Instance.onSceneStarted += ShowStartGameUI;
        }

        private void OnDisable()
        {
            SceneController.Instance.onSceneStarted -= ShowStartGameUI;
        }

        private void ShowStartGameUI()
        {
            uiCanvas.SetActive(true);
        }

        public void ToggleUI()
        {
            uiCanvas.SetActive(!uiCanvas.activeSelf);
        }

        public void StartButtonClicked()
        {
            ToggleUI();
            SceneController.Instance.StartScene();
        }

        public void QuitButtonClicked()
        {

        }
    }

}


