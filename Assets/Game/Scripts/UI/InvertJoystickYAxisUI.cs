using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Core;
using UnityEngine.UI;
using System;


namespace SC.UI
{
    public class InvertJoystickYAxisUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI descriptiveText;
        [SerializeField] Toggle toggle;
        [SerializeField] string isOntext = "Push stick up to go down";
        [SerializeField] string isOfftext = "Push stick up to go up";

        private PlayerSettings playerSettings;

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerSettings = player.GetComponent<PlayerSettings>();
            toggle.onValueChanged.AddListener(delegate { ToggleInvertJoystick(toggle); });
            DisplayCurrentValue();
        }

        private void DisplayCurrentValue()
        {
            if(PlayerPrefs.GetString(PlayerSettings.InvertJoystickKey)== PlayerSettings.InvertJoystickUpdownTrueSetting)
            { 
                toggle.isOn = true;
                descriptiveText.text = isOntext;
            }
            else
            { 
                toggle.isOn = false;
                descriptiveText.text = isOfftext;
            }
        }

        private void ToggleInvertJoystick(Toggle toggle)
        {
            if (toggle.isOn) 
            {
                PlayerPrefs.SetString(PlayerSettings.InvertJoystickKey, PlayerSettings.InvertJoystickUpdownTrueSetting);
            }
            else
            {
                PlayerPrefs.SetString(PlayerSettings.InvertJoystickKey, "");
            }
            playerSettings.SettingsUpdated();
            DisplayCurrentValue();
        }
    }
}


