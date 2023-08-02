using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using SC.Movement;

namespace SC.SafeZone
{

    public class StopPoint : MonoBehaviour
    {
        [SerializeField] Faction faction;
        [SerializeField] bool ignorePlayer = true;
        [SerializeField] bool bringToStopOnEnter = true;




        private void OnTriggerEnter(Collider other)
        {
            CombatTarget combatTarget = other.GetComponent<CombatTarget>();
            if (combatTarget == null) return;
            if (combatTarget.GetFaction() != faction) return;
            if (combatTarget.gameObject.tag == "Player" && ignorePlayer) return;



            AIMovementControl aIMovementControl = other.GetComponent<AIMovementControl>();
            if (aIMovementControl != null)
            { 
                aIMovementControl.SetDesiredSpeed(0f);
                aIMovementControl.SetCanControLSpeed(true);
                aIMovementControl.SetCanManeuver(false);
            }

        }
    }
}