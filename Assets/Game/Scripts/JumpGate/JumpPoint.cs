using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.JumpGate
{
    public class JumpPoint : MonoBehaviour
    {
        [SerializeField] float jumpSpeed = 3000f;
        [SerializeField] Transform[] jumpDestinations;


        int jumpDestiationIndex;

        // Start is called before the first frame update
        void Start()
        {
            jumpDestiationIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            JumpGateBehaviour jumpGateBehaviour = other.GetComponent<JumpGateBehaviour>();
            if (jumpGateBehaviour == null) return;

            jumpGateBehaviour.StartJumpProcess(jumpSpeed, jumpDestinations[jumpDestiationIndex]);
            jumpDestiationIndex++;
            if (jumpDestiationIndex >= jumpDestinations.Length)
            {
                jumpDestiationIndex = 0;
            }

        }
    }

}


