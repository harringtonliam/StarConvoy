using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.SceneControl;

namespace SC.UI
{

    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] Button newGameButton;
        [SerializeField] Button loadSavedGameButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button exitGameButton;
        [SerializeField] NewGameUI newGameUI;
        [SerializeField] LoadGameUI loadGameUI;
        [SerializeField] SettingsUI settingsUI;

        // Start is called before the first frame update
        void Start()
        {
            CursorControl.Instance.SetNewCursor(CursorType.UICursor);
            newGameButton.onClick.AddListener(NewGameButtonClicked);
            loadSavedGameButton.onClick.AddListener(LoadSavedGameButtonClicked);
            settingsButton.onClick.AddListener(DisplaySettingsButtonClicked);
            exitGameButton.onClick.AddListener(ExitGameButtonClicked);
        }


        public void NewGameButtonClicked()
        {
            newGameUI.ShowHideUI(true);
        }

        public void LoadSavedGameButtonClicked()
        {
            loadGameUI.ShowHideUI(true);

        }

        public void DisplaySettingsButtonClicked()
        {
            settingsUI.ToggleUI(true);
        }

        public void ExitGameButtonClicked()
        {
            Application.Quit();
        }
    }

}



