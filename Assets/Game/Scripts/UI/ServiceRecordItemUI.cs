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
        [SerializeField] Color defaultBackGroundColor;


        public void Setup(string label, string value )
        {
            Setup(label, value, defaultBackGroundColor, false);
        }

        public void Setup(string label, string value, Color backgroundImageColor, bool totalItem)
        {
            itemLabel.text = label;
            itemValue.text = value;
            backgroundImage.color = backgroundImageColor;
            if (totalItem)
            {
                itemLabel.fontWeight = FontWeight.Bold;
                itemValue.fontWeight = FontWeight.Bold;
            }
        }
    }
}


