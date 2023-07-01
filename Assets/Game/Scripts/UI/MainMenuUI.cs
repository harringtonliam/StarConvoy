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
        [SerializeField] Button exitGameButton;
        [SerializeField] NewGameUI newGameUI;
        [SerializeField] LoadGameUI loadGameUI;

        // Start is called before the first frame update
        void Start()
        {
            newGameButton.onClick.AddListener(NewGameButtonClicked);
            loadSavedGameButton.onClick.AddListener(LoadSavedGameButtonClicked);
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

        public void ExitGameButtonClicked()
        {
            MainMenuController.Instance.ExitGame();

        }
    }

}



