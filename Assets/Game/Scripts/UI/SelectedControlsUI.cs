using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SC.Core;
using System;


namespace SC.UI
{
    public class SelectedControlsUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI displayText;
        [SerializeField] Image displayImage;
        [SerializeField] DisplayConfiguration[] displayConfigurations;

        [Serializable]
        public struct DisplayConfiguration
        {
            public string mouseOrControllerSettingValue;
            public string textToDisplay;
            public Sprite spriteToDisplay;
        }

        PlayerSettings playerSettings; 

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerSettings = player.GetComponent<PlayerSettings>();
            playerSettings.onSettingsUpdated += ShowDisplaySettings;
            ShowDisplaySettings();
        }

        private void OnDisable()
        {
            try
            {
                playerSettings.onSettingsUpdated -= ShowDisplaySettings;
            }
            catch (Exception)
            {

                Debug.Log("inable to unscubsribe from player settings ");
            }
        }


        private void ShowDisplaySettings()
        {
            for (int i = 0; i < displayConfigurations.Length; i++)
            {
                if (displayConfigurations[i].mouseOrControllerSettingValue == PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey))
                {
                    displayText.text = displayConfigurations[i].textToDisplay;
                    displayImage.sprite = displayConfigurations[i].spriteToDisplay;
                }
            }
        }
    }

}


