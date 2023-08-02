using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.JumpGate
{
    public class JumpGate : MonoBehaviour
    {
        [SerializeField] GameObject[] objectsToEnable;
        [SerializeField] bool isEnabled = true;

        public bool IsEneabled {  get { return isEnabled; } }


        // Start is called before the first frame update
        void Start()
        {
            EnableDisableJumpGate(isEnabled);
        }

        public void EnableDisableJumpGate(bool enable)
        {
            isEnabled = enable;

            for (int i = 0; i < objectsToEnable.Length; i++)
            {
                objectsToEnable[i].SetActive(isEnabled);
            }

        }

    }

}


