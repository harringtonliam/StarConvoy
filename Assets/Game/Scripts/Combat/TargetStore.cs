using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class TargetStore : MonoBehaviour
    {
        [SerializeField] Health[] availableTargets;
        
        private static TargetStore _instance;

        public static TargetStore Instance { get { return _instance; } }

        public Health[] AvailableTargets { get { return availableTargets; } }

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
        }


    }

}


