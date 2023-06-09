using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{


    public class TargetDetector : MonoBehaviour
    {
        [SerializeField] AICombatControl aICombatControl;
        [SerializeField] CombatTarget thisCombatTarget;



        private void OnTriggerEnter(Collider other)
        {
            CombatTarget otherCombatTarget = other.GetComponent<CombatTarget>();
            if (IsValidTarget(otherCombatTarget))
            {
                aICombatControl.SetCombatTarget(otherCombatTarget);
            }
        }

        private bool IsValidTarget(CombatTarget otherCombatTarget)
        {
            if (otherCombatTarget == null) return false;
            if ( otherCombatTarget.GetFaction() == thisCombatTarget.GetFaction()) return false;
            if (otherCombatTarget.GetFaction() == TargetStore.Instance.GetNeutralFaction()) return false;
            return true;
        }
    }
}


