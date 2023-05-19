using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class TargetStore : MonoBehaviour
    {
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

        private void Start()
        {
            
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


    }

}


