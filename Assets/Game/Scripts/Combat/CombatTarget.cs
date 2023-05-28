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
        [SerializeField] bool isSafe = false;
        [SerializeField] bool isHidden = false;

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

        public bool GetIsSafe()
        {
            return isSafe;
        }

        public bool GetIsHidden()
        {
            return isHidden;
        }

        public void SetIsSafe(bool safe)
        {
            isSafe = safe;
        }

        public void SetIsHidden(bool hidden)
        {
            isHidden = hidden;
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



