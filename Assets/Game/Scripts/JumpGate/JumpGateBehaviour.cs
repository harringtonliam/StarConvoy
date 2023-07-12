using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Messaging;
using SC.Combat;
using SC.Movement;
using System;

namespace SC.JumpGate
{
    public class JumpGateBehaviour : MonoBehaviour
    {
        [SerializeField] ParticleSystem jumpVFX;

        Move move;
        MessageSender messageSender;
        CombatTarget combatTarget;
        AIMovementControl aIMovementControl;



        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Move>();
            messageSender = GetComponent<MessageSender>();
            combatTarget = GetComponent<CombatTarget>();
            aIMovementControl = GetComponent<AIMovementControl>();
        }


        public void StartJumpProcess(float jumpSpeed, Transform jumpDestination)
        {
            EnableDisablePlayerControls(false);
            SetAIMovementTarget(jumpDestination);
            SendJumpingMessage();

            transform.LookAt(jumpDestination);
            PlayJumpVFX();
            move.SetCurrentSpeed(jumpSpeed, true);
            combatTarget.SetIsSafe(true);
        }

        private void SetAIMovementTarget(Transform jumpDestination)
        {
            if (aIMovementControl == null) return;
            aIMovementControl.SetMovementTarget(jumpDestination);
            aIMovementControl.SetCanControLSpeed(false);
        }

        public void EnableDisablePlayerControls(bool enable)
        {
            if (gameObject.tag != "Player") return;
            SpeedControl speedControl = GetComponent<SpeedControl>();
            speedControl.SetEnabled(enable);
            Rotate rotate = GetComponent<Rotate>();
            rotate.SetEnabled(enable);
            WeaponController weaponController = GetComponent<WeaponController>();
            weaponController.SetEnabled(enable);
        }

        private void SendJumpingMessage()
        {
            if (messageSender != null)
            {
                messageSender.TriggerSendMessageProcess(MessageType.JumpDone);
            }
        }

        public void StartArrivalProcess()
        {
            MakeHidden();
            BringToAStop();
            EnableDisablePlayerControls(true);
        }

        private void MakeHidden()
        {
            if (combatTarget == null) return;
            combatTarget.SetIsHidden(true);
        }

        private void BringToAStop()
        {
            StopJumpVFX();
            if (move == null) return;
            if (move.CurrentSpeed > move.MaxSpeed)
            {
                move.SetCurrentSpeed(move.MaxSpeed);
           }
            if (aIMovementControl != null)
            {
                aIMovementControl.SetDesiredSpeed(0f);
                aIMovementControl.SetCanManeuver(false);
            }
        }

        private void PlayJumpVFX()
        {
            if (jumpVFX != null)
            {
                jumpVFX.Play();
            }
        }

        private void StopJumpVFX()
        {
            if (jumpVFX != null)
            {
                jumpVFX.Stop();
            }
        }
    }

}