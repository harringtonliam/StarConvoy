using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SC.UI
{
    public class ServiceRecordItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemLabel;
        [SerializeField] TextMeshProUGUI itemValue;
        [SerializeField] Image backgroundImage;


        public void Setup(string label, string value, Color backgroundImageColor)
        {
            itemLabel.text = label;
            itemValue.text = value;
            backgroundImage.color = backgroundImageColor;
        }
    }
}


