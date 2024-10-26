using System;
using UnityEngine;
using UnityEngine.UI;
using SC.Core;


namespace sc.ui
{
    public class SCTutorialToggle : MonoBehaviour
    {
        Toggle tutorialToggle;

        private PlayerSettings playerSettings;

        void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerSettings = player.GetComponent<PlayerSettings>();

            tutorialToggle = GetComponent<Toggle>();
            tutorialToggle.onValueChanged.AddListener(delegate { ToggleTutorial(tutorialToggle); });

            if(PlayerPrefs.GetString(PlayerSettings.TutorialToggle) != PlayerSettings.TutorialOn)
            {
                tutorialToggle.isOn = true;
            }
        }

        private void ToggleTutorial(Toggle change)
        {
            if (change.isOn)
            {
                PlayerPrefs.SetString(PlayerSettings.TutorialToggle, PlayerSettings.TutorialOn);
            }
            else
            {
                PlayerPrefs.SetString(PlayerSettings.TutorialToggle, PlayerSettings.TutorialOff);
            }
            playerSettings.SettingsUpdated();

        }

    }

}


