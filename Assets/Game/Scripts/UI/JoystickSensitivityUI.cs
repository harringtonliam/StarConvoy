using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Core;
using TMPro;
using UnityEngine.UI;

namespace SC.UI
{
    public class JoystickSensitivityUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI sensitivityText;
        [SerializeField] Slider slider;
        [SerializeField] SensitivityValue[] sensitivityValues;

        [System.Serializable]
        public struct SensitivityValue
        {
            public float sliderValue;
            public float sensitivityValue;
            public string descriptiveText;
        }

        // Start is called before the first frame update
        void Start()
        {
            slider.onValueChanged.AddListener(delegate { SliderChanged(); });
            ShowCurrentValues();
        }

        
        public void ShowCurrentValues()
        {
            Debug.Log("JoystickSensitivityUI ShowCurrentValues " + PlayerPrefs.GetFloat(PlayerSettings.JoystickSensitivityKey).ToString());
            for (int i = 0; i < sensitivityValues.Length; i++)
            {
                if (Mathf.Approximately(PlayerPrefs.GetFloat(PlayerSettings.JoystickSensitivityKey), sensitivityValues[i].sensitivityValue))
                {
                    slider.value = sensitivityValues[i].sliderValue;
                    sensitivityText.text = sensitivityValues[i].descriptiveText;
                    break;
                }
            }
        }


        private void SliderChanged()
        {
            for (int i = 0; i < sensitivityValues.Length; i++)
            {
                if (Mathf.Approximately(slider.value, sensitivityValues[i].sliderValue))
                {
                    PlayerPrefs.SetFloat(PlayerSettings.JoystickSensitivityKey, sensitivityValues[i].sensitivityValue);
                    sensitivityText.text = sensitivityValues[i].descriptiveText;
                    break;
                }
            }
        }
    }
}


