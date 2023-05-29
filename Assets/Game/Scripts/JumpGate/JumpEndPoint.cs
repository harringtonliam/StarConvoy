using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using SC.Combat;

namespace SC.JumpGate
{
    public class JumpEndPoint : MonoBehaviour
    {
        Move move;
        CombatTarget combatTarget;

        private void OnTriggerEnter(Collider other)
        {
            JumpGateBehaviour jumpGateBehaviour = other.GetComponent<JumpGateBehaviour>();
            if (jumpGateBehaviour == null) return;

            jumpGateBehaviour.StartArrivalProcess();
        }


    }
}


