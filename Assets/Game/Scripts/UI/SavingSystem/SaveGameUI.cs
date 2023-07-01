using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.SceneControl;
using TMPro;


namespace SC.UI.SavingSystem
{
    public class SaveGameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI savedGameNameText = null;
        [SerializeField] TextMeshProUGUI savedGameTimeText = null;
        [SerializeField] Button loadButton;
        [SerializeField] Button deleteButton;


        private void Start()
        {
            loadButton.onClick.AddListener(LoadGame);
            deleteButton.onClick.AddListener(DeleteGame);
        }

        public void Setup(string savedGameName, string savedGameTime)
        {

            savedGameNameText.text = savedGameName;
            savedGameTimeText.text = savedGameTime;
        }


        public void SaveGame()
        {
            FindObjectOfType<SavingWrapper>().Save(savedGameNameText.text);

        }

        public void DeleteGame()
        {
            FindObjectOfType<SavingWrapper>().Delete(savedGameNameText.text);
        }

        public void LoadGame()
        {
            FindObjectOfType<SavingWrapper>().LoadSavedGame(savedGameNameText.text);

        }


    }

}


