using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Combat;

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
            playerTargetSelection.CurrentTargetChanged += Redraw;
        }



        private void Redraw()
        {
            if (playerTargetSelection.GetCurrentTarget() != null)
            {
                currentTargetName.text = playerTargetSelection.GetCurrentTarget().gameObject.name;
            }
            else
            {
                currentTargetName.text = "No Target";
            }
            
        }
    }

}


