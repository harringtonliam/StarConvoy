using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;

namespace SC.UI
{
    public class StartGameCanvasUI : MonoBehaviour
    {

        [SerializeField] GameObject uiCanvas = null;

        void OnEnable()
        {
            Debug.Log("***StartGameCanvasUI onEnable***");
            SceneController.Instance.onSceneStarted += ShowStartGameUI;
        }

        private void OnDisable()
        {
            Debug.Log("***StartGameCanvasUI OnDisable***");
            SceneController.Instance.onSceneStarted -= ShowStartGameUI;
        }

        private void Start()
        {
            ShowStartGameUI();
        }

        private void ShowStartGameUI()
        {
            Debug.Log("***StartGameCanvasUI ShowStartGameUI***");
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


