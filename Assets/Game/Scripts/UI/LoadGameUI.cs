using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SC.UI
{
    public class LoadGameUI : MonoBehaviour
    {
        [SerializeField] GameObject newGameUI;
        [SerializeField] Button cancelButton;


        private void Start()
        {
            cancelButton.onClick.AddListener(cancelButtonClicked);
        }

        public void ShowHideUI(bool isVisible)
        {
            newGameUI.SetActive(isVisible);

        }

        private void cancelButtonClicked()
        {
            ShowHideUI(false);
        }


    }

}
