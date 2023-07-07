using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using System;

namespace SC.UI
{
    public class TargetsUI : MonoBehaviour
    {
        [SerializeField] TargetDetailsUI currentTarget;
        [SerializeField] TargetDetailsUI targetInSights;


        GameObject player;
        TargetSelection targetSelection;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            targetSelection = player.GetComponent<TargetSelection>();
            targetSelection.CurrentTargetChanged += Redraw;
            targetSelection.TargetInSightsChanged += Redraw;
            Redraw();
        }

        private void OnDisable()
        {
            try
            {
                targetSelection.CurrentTargetChanged -= Redraw;
                targetSelection.TargetInSightsChanged -= Redraw;
            }
            catch (Exception ex)
            {
                Debug.Log("TargetsUI OnDisable" + ex.Message);
            }

        }

        private void Redraw()
        {
            currentTarget.Setup("Current Target", targetSelection.GetCurrentTarget());
            targetInSights.Setup("[T]arget In Sights", targetSelection.GetTargetInSights());
        }


    }

}



