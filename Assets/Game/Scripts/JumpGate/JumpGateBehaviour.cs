using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Messaging;
using SC.Combat;
using SC.Movement;

namespace SC.JumpGate
{
    public class JumpGateBehaviour : MonoBehaviour
    {
        Move move;
        MessageSender messageSender;
        CombatTarget combatTarget;


        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Move>();
            messageSender = GetComponent<MessageSender>();
            combatTarget = GetComponent<CombatTarget>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartJumpProcess(float jumpSpeed, Transform jumpDestination)
        {
            messageSender.TriggerSendMessageProcess(MessageType.JumpDone);
            move.SetCurrentSpeed(jumpSpeed, true);
            combatTarget.SetIsSafe(true);
        }
    }

}