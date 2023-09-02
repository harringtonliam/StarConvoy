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
        [SerializeField] bool isAddToTargetStore = true;
        [SerializeField] bool isExcludedFromConvoy = false;
        [SerializeField] bool denyMissileLock = false;

        Health health;

        public event Action<CombatTargetUpdateHandlerArgs> TargetUpdated;

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public void SetUniqueIdentifer(string newId)
        {
            uniqueIdentifier = newId;
        }

        public bool DenyMissileLock {  get { return denyMissileLock; } }

        public Faction GetFaction()
        {

            return faction;
        }

        public bool IsAddToTargetStore{get { return isAddToTargetStore; } }
        

        private void Awake()
        {
            uniqueIdentifier = System.Guid.NewGuid().ToString();
        }

        void Start()
        {
            health = GetComponent<Health>();
            health.onDeath += DestroyInTargetStore;
        }

        public bool GetIsSafe()
        {
            return isSafe;
        }

        public bool GetIsHidden()
        {
            return isHidden;
        }

        public bool GetIsExcludedFromConvoy()
        {
            return isExcludedFromConvoy;
        }

        public void SetIsSafe(bool safe)
        {
            isSafe = safe;
            OnTargetUpdated(new CombatTargetUpdateHandlerArgs(this, false, false));
        }

        public void SetIsHidden(bool hidden)
        {
            isHidden = hidden;
            OnTargetUpdated(new CombatTargetUpdateHandlerArgs(this, false, false));
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

        public void DestroyInTargetStore()
        {
            if (isAddToTargetStore)
            {
                OnTargetUpdated(new CombatTargetUpdateHandlerArgs(this, true, false));
            }
        }

        public string GetFullIdentifier()
        {
            return uniqueIdentifierPrefix + uniqueIdentifier;
        }

        private void OnTargetUpdated(CombatTargetUpdateHandlerArgs e)
        {
            if (TargetUpdated != null)
            {
                TargetUpdated(e);
            }
        }
    }

    public class CombatTargetUpdateHandlerArgs : EventArgs
    {
        public CombatTarget combatTarget;
        public bool isDestroyed;
        public bool isRemoveFromTargetStore;

        public  CombatTargetUpdateHandlerArgs (CombatTarget combatTarget, bool isDestroyed, bool isRemoveFromTargetStore)
        {
            this.combatTarget = combatTarget;
            this.isDestroyed = isDestroyed;
            this.isRemoveFromTargetStore = isRemoveFromTargetStore;
        }
    }
}



