using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        [SerializeField] bool addToTargetStore = true;

        Health health;

        public event Action onIsHidden;

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
            health.onDeath += DestroyInTargetStore;
            if (addToTargetStore)
            {
                TargetStore.Instance.AddTarget(GetFullIdentifier(), this);
            }
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
            if (onIsHidden != null)
            {
                onIsHidden();
            }
        }

        private void OnDisable()
        {
            try
            {
                health.onDeath -= DestroyInTargetStore;
            }
            catch (Exception ex)
            {
                Debug.Log("CombatTarget OnDisable " + ex.Message);
            }

        }

        public void RemoveFromTargetStore()
        {
            if (addToTargetStore)
            {
                TargetStore.Instance.RemoveTarget(GetFullIdentifier());
            }
        }

        public void DestroyInTargetStore()
        {
            if (addToTargetStore)
            {
                TargetStore.Instance.DestroyTarget(GetFullIdentifier());
            }
        }

        public string GetFullIdentifier()
        {
            return uniqueIdentifierPrefix + uniqueIdentifier;
        }

    }

}



