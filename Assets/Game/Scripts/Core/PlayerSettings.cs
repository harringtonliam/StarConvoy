using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SC.Core
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField] float JoystickSensitivityDefault = 0.5f;
        [SerializeField] string MouseOrControllerDefault = UseMouseSetting;
        [SerializeField] string TutorialToggleDefault = TutorialOn;

        public const string MouseOrControllerKey = "MouseOrController";
        public const string UseMouseSetting = "mouse";
        public const string UseControllerSetting = "controller";
        public const string InvertJoystickKey = "InvertJoystickUpDown";
        public const string InvertJoystickUpdownTrueSetting = "true";
        public const string JoystickSensitivityKey = "JoystickSensitivity";
        public const string TutorialToggle = "TutorialToggle";
        public const string TutorialOff = "Off";
        public const string TutorialOn = "On";

        public event Action onSettingsUpdated;

        // Start is called before the first frame update
        void Start()
        {
            DefaultSettings();
            SettingsUpdated();
        }

        public void SettingsUpdated()
        {
            if (onSettingsUpdated != null)
            {
                onSettingsUpdated();
            }
        }

        private void DefaultSettings()
        {
            bool needToSave = false;
            if (!PlayerPrefs.HasKey(TutorialToggle))
            {
                PlayerPrefs.SetString(TutorialToggle, TutorialOn);
                needToSave = true;
            }

            if(!PlayerPrefs.HasKey(MouseOrControllerKey))
            {
                PlayerPrefs.SetString(MouseOrControllerKey, MouseOrControllerDefault);
                PlayerPrefs.SetString(InvertJoystickKey, InvertJoystickUpdownTrueSetting);
                PlayerPrefs.SetFloat(JoystickSensitivityKey, JoystickSensitivityDefault);
                needToSave = true;
            }

            if(needToSave)
            {
                PlayerPrefs.Save();
            }

        }
    }

}
