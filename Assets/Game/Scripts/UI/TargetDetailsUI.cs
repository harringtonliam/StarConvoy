using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] Image targetImage;
        [SerializeField] Image factionBackground;
        [SerializeField] Color sameFactionColor;
        [SerializeField] Color enemyFactionColor;
        [SerializeField] Color neutralFactionColor;

        Faction playerFaction;

        private void Start()
        {
            GetPlayerFaction();
        }

        private void GetPlayerFaction()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerFaction = player.GetComponent<CombatTarget>().GetFaction();
        }

        public void Setup(string title, CombatTarget target)
        {
            titleText.text = title;

            if (target == null)
            {
                targetNameText.text = string.Empty;
                targetFactionText.text = string.Empty;
                targetImage.sprite = null;
                return;
            }
            ShipInformation shipInformation = target.GetComponent<ShipInformation>();
            targetNameText.text = shipInformation.GetShipDetails().shipName;
            targetFactionText.text = target.GetFaction().ToString();
            Debug.Log("TargetDetailsUI Setup");
            targetImage.sprite = shipInformation.GetShipDetails().shipSprite;
            Debug.Log("TargetDetailsUI Setup set sprite");
            if (target.GetFaction() == playerFaction)
            {
                factionBackground.color = Color.green;
                Debug.Log("TargetDetailsUI same faction");
            }
            else if(target.GetFaction() == Faction.None)
            {
                factionBackground.color = Color.grey;
                Debug.Log("TargetDetailsUI neutral faction");
            }
            else
            {
                factionBackground.color = Color.red;
                Debug.Log("TargetDetailsUI enemy faction");
            }
        }


    }

}


