using SC.SceneControl;
using SC.Core;
using UnityEngine;
using UnityEngine.UI;

namespace SC.UI
{
    public class TutotialUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas;
        [SerializeField] bool isShownAtSceneStart = false;

        void Start()
        {
            ShowStartGameUI();
        }

        private void ShowStartGameUI()
        {
            CursorControl.Instance.SetNewCursor(CursorType.UICursor);
            uiCanvas.SetActive(isShownAtSceneStart);
        }

        public void ShowHideUI(bool isVisible)
        {
            uiCanvas.SetActive(isVisible);
        }

        public void CloseButtonClicked()
        {
            ShowHideUI(false);

            SceneController.Instance.StartScene();
         }




    }

}


