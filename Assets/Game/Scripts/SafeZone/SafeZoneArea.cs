using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;



namespace SC.SafeZone
{

    public class SafeZoneArea : MonoBehaviour
    {
        [SerializeField] Faction faction;
        [SerializeField] bool ignorePlayer = true;
        [SerializeField] bool bringToStopOnEnter = false;

        private void OnTriggerEnter(Collider other)
        {
            CombatTarget combatTarget = other.GetComponent<CombatTarget>();
            if (combatTarget == null) return;
            if (combatTarget.GetFaction() != faction) return;
            if (combatTarget.gameObject.tag == "Player" && ignorePlayer) return;

            SafeZoneBehaviour safeZoneBehaviour = other.GetComponent<SafeZoneBehaviour>();
            if (safeZoneBehaviour != null)
            {
                safeZoneBehaviour.StartArrivalProcess(bringToStopOnEnter);
            }

        }
    }
}

