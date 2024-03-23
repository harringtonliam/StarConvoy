using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SC.Core
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField] float JoystickSensitivityDefault = 0.5f;
        [SerializeField] string MouseOrControllerDefault = UseControllerSetting;

        public const string MouseOrControllerKey = "MouseOrController";
        public const string UseMouseSetting = "mouse";
        public const string UseControllerSetting = "controller";
        public const string InvertJoystickKey = "InvertJoystickUpDown";
        public const string InvertJoystickUpdownTrueSetting = "true";
        public const string JoystickSensitivityKey = "JoystickSensitivity";

        public event Action onSettingsUpdated;

        // Start is called before the first frame update
        void Start()
        {
            DefaultSettings();
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
            if(PlayerPrefs.HasKey(MouseOrControllerKey)) return;
            PlayerPrefs.SetString(MouseOrControllerKey, MouseOrControllerDefault);
            PlayerPrefs.SetString(InvertJoystickKey, InvertJoystickUpdownTrueSetting);
            PlayerPrefs.SetFloat(JoystickSensitivityKey, JoystickSensitivityDefault);
            PlayerPrefs.Save();
        }
    }

}
