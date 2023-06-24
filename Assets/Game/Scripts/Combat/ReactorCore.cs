using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    [RequireComponent (typeof (Health))]
    public class ReactorCore : MonoBehaviour
    {
        [SerializeField] Health[] parentObjects;

        Health health;


        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.onDeath += DestroyBehaviour;
        }

        private void OnDisable()
        {
            health.onDeath -= DestroyBehaviour;
        }


        private void DestroyBehaviour()
        {
            for (int i = 0; i < parentObjects.Length; i++)
            {
                parentObjects[i].TakeDamage(parentObjects[i].CurrentHealth);
            }

           
        }
    }

}


