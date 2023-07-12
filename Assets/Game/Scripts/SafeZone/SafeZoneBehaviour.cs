using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using SC.Combat;
using SC.Messaging;

namespace SC.SafeZone
{
    public class SafeZoneBehaviour : MonoBehaviour
    {
        public void StartArrivalProcess(bool isBringToAStopOnEnter)
        {
            CombatTarget combatTarget = GetComponent<CombatTarget>();
            combatTarget.SetIsSafe(true);
            SendArrivalMessage();
            if (isBringToAStopOnEnter)
            {
                BringToAStop();
            }
        }

        private void BringToAStop()
        {

            Debug.Log("Safe zone behaviour bring into a stop");
            AIMovementControl aIMovementControl = GetComponent<AIMovementControl>();
            if (aIMovementControl != null)
            {
                Debug.Log("Safe zone behaviour disabling speed control ");
                aIMovementControl.SetDesiredSpeed(0f);
                aIMovementControl.SetCanManeuver(false);
            }
            Move move = GetComponent<Move>();
            move.ChangeSpeed(0f);
        }

        private void SendArrivalMessage()
        {
            MessageSender messageSender = GetComponent<MessageSender>();
            if (messageSender != null)
            {
                messageSender.TriggerSendMessageProcess(MessageType.ReachedSafety);
            }
        }
    }
}


