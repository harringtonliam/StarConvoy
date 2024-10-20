using UnityEngine;

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

        public void HideUi()
        {
            uiCanvas.SetActive(false);
        }

    }

}


