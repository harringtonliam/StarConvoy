using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = "";

        Health health;


        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        private void Awake()
        {
            uniqueIdentifier = System.Guid.NewGuid().ToString();
        }

        void Start()
        {
            health = GetComponent<Health>();
            health.onDeath += RemoveFromTargetStore;
            TargetStore.Instance.AddTarget(uniqueIdentifier, this);
            
        }

        private void OnDisable()
        {
            health.onDeath -= RemoveFromTargetStore;
        }

        public void RemoveFromTargetStore()
        {
            TargetStore.Instance.RemoveTarget(uniqueIdentifier);
        }

    }

}



