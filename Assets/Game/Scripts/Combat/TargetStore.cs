using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SC.Combat
{
    public class TargetStore : MonoBehaviour
    {
        [SerializeField] Faction neutralFaction = Faction.None;
        SortedDictionary<string, CombatTarget> availableTargets;
        
        private static TargetStore _instance;

        public static TargetStore Instance { get { return _instance; } }

        public SortedDictionary<string, CombatTarget> AvailableTargets { get { return availableTargets; } }

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
            availableTargets = new SortedDictionary<string, CombatTarget>();
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

        public SortedDictionary<string, CombatTarget> CombatTargetsInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                   where kpv.Value.GetFaction() == faction
                                   select kpv;
            var filteredDictionary  = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, CombatTarget>(filteredDictionary);
            
        }

        public SortedDictionary<string, CombatTarget> SafeCombatTargetsInFaction(Faction faction, bool isSafe)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                        where kpv.Value.GetFaction() == faction && kpv.Value.GetIsSafe() == isSafe
                                        select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, CombatTarget>(filteredDictionary);
        }

        public SortedDictionary<string, CombatTarget> VisibleCombatTargets(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                        where kpv.Value.GetFaction() == faction && kpv.Value.GetIsHidden() == false
                                        select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, CombatTarget>(filteredDictionary);
        }


        public SortedDictionary<string, CombatTarget> CombatTargetsNotInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                   where kpv.Value.GetFaction() != faction && kpv.Value.GetFaction() != neutralFaction
                                   select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, CombatTarget>(filteredDictionary);
        }

        public SortedDictionary<string, CombatTarget> ValidCombatTargetsNotInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                        where kpv.Value.GetFaction() != faction && kpv.Value.GetFaction() != neutralFaction && kpv.Value.GetIsSafe() == false
                                        select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, CombatTarget>(filteredDictionary);
        }


    }

}


