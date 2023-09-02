using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using SC.Messaging;


namespace SC.JumpGate
{
    public class JumpPoint : MonoBehaviour
    {
        [SerializeField] float jumpSpeed = 3000f;
        [SerializeField] Transform[] jumpDestinations;
        [SerializeField] Transform playerJumpDestination;
        [SerializeField] MessageSender messageSender;


        int jumpDestiationIndex;

        // Start is called before the first frame update
        void Start()
        {
            jumpDestiationIndex = 0;
        }

        



        private void OnTriggerEnter(Collider other)
        {
            JumpGateBehaviour jumpGateBehaviour = other.GetComponent<JumpGateBehaviour>();
            if (jumpGateBehaviour == null) return;

            Transform jumpDestinationToUse = jumpDestinations[jumpDestiationIndex];

            if (other.gameObject.tag == "Player" && playerJumpDestination != null)
            {
                jumpDestinationToUse = playerJumpDestination;
            }

            jumpGateBehaviour.StartJumpProcess(jumpSpeed, jumpDestinationToUse);
            jumpDestiationIndex++;
            if (jumpDestiationIndex >= jumpDestinations.Length)
            {
                jumpDestiationIndex = 0;
            }
            if(other.gameObject.tag != "Player")
            {
                CheckHowManyShipsLeftToJump();
            }


        }

        private void CheckHowManyShipsLeftToJump()
        {
            if (TargetStore.Instance.ConvoyShipsThatAreNotSafeExceptPlayer(Faction.Alliance).Count == 0)
            {
                messageSender.TriggerSendMessageProcess(MessageType.JumpDone);
            }
        }

    }

}


