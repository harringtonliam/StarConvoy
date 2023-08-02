using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.JumpGate
{
    public class JumpGate : MonoBehaviour
    {
        [SerializeField] GameObject[] objectsToEnable;
        [SerializeField] bool isEnabled = true;
        [SerializeField] bool enableAfterTime = false;
        [SerializeField] float enableTimeDelay = 840;

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

        }

        private IEnumerator EnableAfterTimeDelay()
        {
            yield return new WaitForSeconds(enableTimeDelay);
            EnableDisableJumpGate(!isEnabled);
        }

    }

}


