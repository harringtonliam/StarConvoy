using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SC.UI
{
    public class PlayerDetailItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemLabel;
        [SerializeField] TextMeshProUGUI itemValue;

        public void Setup(string label, string value)
        {
            itemLabel.text = label;
            itemValue.text = value;
        }

    }
}


