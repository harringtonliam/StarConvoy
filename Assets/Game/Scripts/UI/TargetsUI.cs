using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;

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
            targetSelection.CurrentTargetChanged -= Redraw;
            targetSelection.TargetInSightsChanged -= Redraw;
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



