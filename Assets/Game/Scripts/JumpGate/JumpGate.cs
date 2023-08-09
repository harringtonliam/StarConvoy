using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.JumpGate
{
    public class JumpGate : MonoBehaviour
    {
        [SerializeField] GameObject[] objectsToEnable;
        [SerializeField] JumpPoint jumpPoint;
        [SerializeField] bool isEnabled = true;
        [SerializeField] bool enableAfterTime = false;
        [SerializeField] float enableTimeDelay = 840;
        [SerializeField] JumpGateBehaviour[] shipsToDirectToJumpPoint;

        public bool IsEnabled {  get { return isEnabled; } }


        // Start is called before the first frame update
        void Start()
        {
            EnableDisableJumpGate(isEnabled);
            if (enableAfterTime)
            {
                StartCoroutine(EnableAfterTimeDelay());
            }
        }

        public void EnableDisableJumpGate(bool enable)
        {
            isEnabled = enable;

            for (int i = 0; i < objectsToEnable.Length; i++)
            {
                objectsToEnable[i].SetActive(isEnabled);
            }

            if (isEnabled && enableAfterTime)
            {
                DirectShipsToJumpPoint();
            }
        }

        private IEnumerator EnableAfterTimeDelay()
        {
            yield return new WaitForSeconds(enableTimeDelay);
            EnableDisableJumpGate(!isEnabled);
        }

        private void DirectShipsToJumpPoint()
        {
            for (int i = 0; i < shipsToDirectToJumpPoint.Length; i++)
            {
                if (shipsToDirectToJumpPoint[i] != null)
                {
                    shipsToDirectToJumpPoint[i].DirectToJumpPoint(jumpPoint);
                }
            }
        }

    }

}


