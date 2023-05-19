using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace SC.Combat
{
    public class TargetSelection : MonoBehaviour
    {
        private CombatTarget currentTarget;

        private int currentTargetIndex;

        public event Action CurrentTargetChanged;

        public CombatTarget GetCurrentTarget ()
        {
            return currentTarget;
        }


        // Start is called before the first frame update
        void Start ()
        {
            currentTargetIndex = 0;
            currentTarget = null;
            TargetChanged();
        }

        // Update is called once per frame
        void Update()
        {
            RespondToTargetChange();
        }

        private void RespondToTargetChange()
        {
            bool indexChanged = false;
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
    }

}


