using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using System;

namespace SC.UI
{
    public class TargetsUI : MonoBehaviour
    {
        [SerializeField] TargetDetailsUI targetDetailsUIPrefab;

        GameObject player;
        TargetSelection targetSelection;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            targetSelection = player.GetComponent<TargetSelection>();
            targetSelection.CurrentTargetChanged += Redraw;
            targetSelection.TargetInSightsChanged += Redraw;
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
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var currentTargetUI = Instantiate(targetDetailsUIPrefab, transform);
            currentTargetUI.Setup("Current Target", targetSelection.GetCurrentTarget());
            var targetInSightsUI = Instantiate(targetDetailsUIPrefab, transform);
            targetInSightsUI.Setup("[T]arget In Sights", targetSelection.GetTargetInSights());

        }


    }

}



