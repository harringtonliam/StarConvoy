using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC.UI
{

    public class InstructionsUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] Button closeButton;



        // Start is called before the first frame update
        void Start()
        {
            ToggleUI(false);
            closeButton.onClick.AddListener(CloseButtonClicked);
        }

        public void ToggleUI(bool showUI)
        {
            uiCanvas.SetActive(showUI);
        }

        private void CloseButtonClicked()
        {
            try
            {
                PlayerPrefs.Save();
            }
            catch(System.Exception ex)
            {
                Debug.Log("Unable to save play prefs " + ex.Message);
            }
            ToggleUI(false);
        }

    }

}
