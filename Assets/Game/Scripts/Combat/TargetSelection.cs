using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace SC.Combat
{
    public class TargetSelection : MonoBehaviour
    {
        [SerializeField] Transform targetIdentifier;
        [SerializeField] float targetIdentifierRange = 3000f;
        [SerializeField] float targetIdentifierRadius = 0.5f;



        private CombatTarget currentTarget;
        private CombatTarget targetInSights;

        private int currentTargetIndex;

        public event Action CurrentTargetChanged;
        public event Action TargetInSightsChanged;

        public CombatTarget GetCurrentTarget ()
        {
            return currentTarget;
        }

        public CombatTarget GetTargetInSights() 
        { 
            return targetInSights; 
        }

        void Start ()
        {
            currentTargetIndex = 0;
            currentTarget = null;
            TargetChanged();
        }

        void Update()
        {

            RespondToScrollToNextTarget();
            IdentifyTargetInSights();
            RespondToSwitchToTargetInSights();
        }

        private void RespondToSwitchToTargetInSights()
        {
            if (Input.GetKeyDown(KeyCode.T) && targetInSights != null)
            {
                currentTarget = targetInSights;
                TargetChanged();
            }
        }

        private void IdentifyTargetInSights()
        {
            CombatTarget detectedTargetInSights = null;

            if (targetIdentifier == null) return;

            RaycastHit hit;
            bool raycastForward = Physics.SphereCast(transform.position, targetIdentifierRadius, transform.forward, out hit, targetIdentifierRange);

            if (raycastForward)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target != null)
                {
                    detectedTargetInSights = target;
                }
                else
                {
                    detectedTargetInSights = null;
                }
            }
            else
            {
                detectedTargetInSights = null;
            }

            if (detectedTargetInSights != targetInSights)
            {
                targetInSights = detectedTargetInSights;
                TargetInSightsChange();
            }

        }

        private void RespondToScrollToNextTarget()
        {

            bool indexChanged = false;
            if (currentTarget == null)
            {
                currentTargetIndex = 0;
                indexChanged = true;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentTargetIndex++;

                if (currentTargetIndex >= TargetStore.Instance.AvailableTargets.Count)
                {
                    currentTargetIndex = 0;
                    
                }
                indexChanged = true;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                currentTargetIndex--;
                if (currentTargetIndex < 0)
                {
                    currentTargetIndex = TargetStore.Instance.AvailableTargets.Count - 1;
                }
                indexChanged = true;
            }

            if (indexChanged)
            {
                currentTarget = TargetStore.Instance.AvailableTargets.ElementAt(currentTargetIndex).Value;
                TargetChanged();
            }

        }


        private void TargetChanged()
        {
            if (CurrentTargetChanged != null)
            {
                CurrentTargetChanged();
            }
        }

        private void TargetInSightsChange()
        {
            if (TargetInSightsChanged != null)
            {
                TargetInSightsChanged();
            }

        }
    }

}


