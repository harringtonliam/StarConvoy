using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifierPrefix = "";
        [SerializeField] string uniqueIdentifier = "";
        [SerializeField] Faction faction = Faction.None;

        Health health;

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public Faction GetFaction()
        { 
       
            return faction;
        }

        private void Awake()
        {
            uniqueIdentifier = System.Guid.NewGuid().ToString();
        }

        void Start()
        {
            health = GetComponent<Health>();
            health.onDeath += RemoveFromTargetStore;
            TargetStore.Instance.AddTarget(GetFullIdentifier(), this);
        }

        private void OnDisable()
        {
            health.onDeath -= RemoveFromTargetStore;
        }

        public void RemoveFromTargetStore()
        {
            TargetStore.Instance.RemoveTarget(GetFullIdentifier());
        }

        public string GetFullIdentifier()
        {
            return uniqueIdentifierPrefix + uniqueIdentifier;
        }

    }

}



