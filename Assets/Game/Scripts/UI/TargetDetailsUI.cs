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
                targetImage.enabled = false;
                factionBackground.enabled = false;
           
                return;
            }
            targetImage.enabled = true;
            factionBackground.enabled = true;
            ShipInformation shipInformation = target.GetComponent<ShipInformation>();
            if (shipInformation== null)
            {
                Debug.Log("Missing SHip information for " + target.gameObject.name);
                return;
            }
            targetNameText.text = shipInformation.GetShipDetails().shipName;
            targetFactionText.text = target.GetFaction().ToString();
            targetImage.sprite = shipInformation.GetShipDetails().shipSprite;
            if (target.GetFaction() == playerFaction)
            {
                factionBackground.color = Color.green;
            }
            else if(target.GetFaction() == Faction.None)
            {
                factionBackground.color = Color.grey;
            }
            else
            {
                factionBackground.color = Color.red;
            }
        }


    }

}


