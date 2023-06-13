using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Combat;
using SC.Attributes;
using System;

namespace SC.UI
{
    public class CurrentTargetUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI currentTargetName;

        GameObject player;
        TargetSelection playerTargetSelection;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerTargetSelection = player.GetComponent<TargetSelection>();
            playerTargetSelection.CurrentTargetChanged += Redraw;
        }


        private void OnDisable()
        {
            try
            {
                playerTargetSelection.CurrentTargetChanged += Redraw;
            }
            catch (Exception ex)
            {
                Debug.Log("CurrentTargetUI OnDisable " + ex.Message);
            }

        }



        private void Redraw()
        {
            if (playerTargetSelection.GetCurrentTarget() != null)
            {
                CombatTarget cuurentCombatTarget = playerTargetSelection.GetCurrentTarget();
                ShipInformation currentShipInformation = cuurentCombatTarget.GetComponent<ShipInformation>();
                currentTargetName.text = currentShipInformation.GetShipDetails().shipName;
            }
            else
            {
                currentTargetName.text = "No Target";
            }
            
        }
    }

}


