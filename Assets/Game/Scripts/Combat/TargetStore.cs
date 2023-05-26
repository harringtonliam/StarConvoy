using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SC.Combat
{
    public class TargetStore : MonoBehaviour
    {
        [SerializeField] Faction neutralFaction = Faction.None;
        Dictionary<string, CombatTarget> availableTargets;
        
        private static TargetStore _instance;

        public static TargetStore Instance { get { return _instance; } }

        public Dictionary<string, CombatTarget> AvailableTargets { get { return availableTargets; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            availableTargets = new Dictionary<string, CombatTarget>();
        }

        public bool AddTarget(string targetGuid, CombatTarget combatTarget)
        {
            bool added = availableTargets.TryAdd(targetGuid, combatTarget);
            return added;
        }

        public void RemoveTarget(string targetGuid)
        {
            availableTargets.Remove(targetGuid);
        }

        public Dictionary<string, CombatTarget> CombatTargetsInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                   where kpv.Value.GetFaction() == faction
                                   select kpv;
            return filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            
        }


        public Dictionary<string, CombatTarget> CombatTargetsNotInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                   where kpv.Value.GetFaction() != faction && kpv.Value.GetFaction() != neutralFaction
                                   select kpv;
            return filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
        }


    }

}


