using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SC.Combat
{
    public class TargetSelection : MonoBehaviour
    {
        private Health currentTarget;

        private int currentTargetIndex;

        public event Action CurrentTargetChanged;

        public Health GetCurrentTarget ()
        {
            return currentTarget;
        }


        // Start is called before the first frame update
        void Start ()
        {
            currentTargetIndex = 0;
            currentTarget = TargetStore.Instance.AvailableTargets[currentTargetIndex];
            CurrentTargetChanged();
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

                if (currentTargetIndex >= TargetStore.Instance.AvailableTargets.Length)
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
                    currentTargetIndex = TargetStore.Instance.AvailableTargets.Length - 1;
                }
                indexChanged = true;
            }

            if (indexChanged)
            {
                currentTarget = TargetStore.Instance.AvailableTargets[currentTargetIndex];
                if (CurrentTargetChanged != null)
                {
                    CurrentTargetChanged();
                }
            }

        }
    }

}


