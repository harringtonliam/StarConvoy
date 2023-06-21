using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    [RequireComponent (typeof (Health))]
    public class ReactorCore : MonoBehaviour
    {
        [SerializeField] Health parentObject;

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
            parentObject.TakeDamage(parentObject.CurrentHealth);
        }
    }

}


