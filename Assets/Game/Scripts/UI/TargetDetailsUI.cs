using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Combat;
using SC.Attributes;

namespace SC.UI
{
    public class TargetDetailsUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI targetNameText;
        [SerializeField] TextMeshProUGUI targetFactionText;


        public void Setup(string title, CombatTarget target)
        {
            titleText.text = title;

            if (target == null)
            {

                targetNameText.text = string.Empty;
                targetFactionText.text = string.Empty;
                return;
            }
            ShipInformation shipInformation = target.GetComponent<ShipInformation>();
            targetNameText.text = shipInformation.GetShipDetails().shipName;
            targetFactionText.text = target.GetFaction().ToString();
        }


    }

}


