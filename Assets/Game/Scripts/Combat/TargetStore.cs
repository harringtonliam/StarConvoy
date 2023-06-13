using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SC.Attributes;

namespace SC.Combat
{
    public class TargetStore : MonoBehaviour
    {
        [SerializeField] Faction neutralFaction = Faction.None;
        SortedDictionary<string, CombatTarget> availableTargets;
        SortedDictionary<string, DestroyedTarget> destroyedTargets;

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
            destroyedTargets = new SortedDictionary<string, DestroyedTarget>();
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

        public void DestroyTarget(string targetGuid)
        {
            Debug.Log("Destorying target");
            ShipInformation shipInformation;
            CombatTarget combatTarget;
            bool targetFound = availableTargets.TryGetValue(targetGuid, out combatTarget);
            if (targetFound)
            {
                shipInformation = combatTarget.GetComponent<ShipInformation>();
                if (shipInformation != null)
                {
                    ShipInformation.ShipDetails shipDetails = shipInformation.GetShipDetails();
                    DestroyedTarget destroyedTarget = new DestroyedTarget(shipDetails.shipName, shipDetails.captainName, shipDetails.shipType, shipDetails.shipSprite, shipDetails.captainSprite, combatTarget.GetFaction());
                    Debug.Log("Adding destroyed target");
                    destroyedTargets.Add(targetGuid, destroyedTarget);
                    RemoveTarget(targetGuid);
                }
            }

        }

        public Faction GetNeutralFaction()
        {
            return neutralFaction;
        }

        public SortedDictionary<string, CombatTarget> CombatTargetsInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in availableTargets
                                        where kpv.Value.GetFaction() == faction
                                        select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
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

        public SortedDictionary<string, DestroyedTarget> DestroyedTargetsInFaction(Faction faction)
        {
            var filteredCombatTargets = from kpv in destroyedTargets
                                        where kpv.Value.faction == faction 
                                        select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, DestroyedTarget>(filteredDictionary);
        }

        public SortedDictionary<string, DestroyedTarget> DestroyedTargetsNotInFaction(Faction faction)
        {

            Debug.Log("Total destoryed Targets" + destroyedTargets.Count());
            var filteredCombatTargets = from kpv in destroyedTargets
                                        where kpv.Value.faction != faction && kpv.Value.faction != neutralFaction
                                        select kpv;
            var filteredDictionary = filteredCombatTargets.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
            return new SortedDictionary<string, DestroyedTarget>(filteredDictionary);
        }


    }

    public class DestroyedTarget
    {
        public string shipName;
        public string captainName;
        public string shipType;
        public Sprite shipSprite;
        public Sprite captainSprite;
        public Faction faction;

        public DestroyedTarget(string shipName, string captainName, string shipType, Sprite shipSprite, Sprite captainSprite, Faction faction )
        {
            this.shipName =shipName;
            this.captainName = captainName;
            this.shipType = shipType;
            this.shipSprite = shipSprite;
            this.captainSprite = captainSprite;
            this.faction = faction;
        }
    }


 


}


