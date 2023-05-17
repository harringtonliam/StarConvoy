using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.Combat
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] float damageDoneToOtherOnCollision = 10f;
        [SerializeField] bool destroySelfOnCollision = false;
        [SerializeField] bool multiplyDamageByVelocity = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("OnCollisionEnter");

            Health otherHealth = collision.gameObject.GetComponent<Health>();
            if (otherHealth != null)
            {
                float damge = damageDoneToOtherOnCollision;
                if (multiplyDamageByVelocity)
                {
                    damge = damageDoneToOtherOnCollision * collision.relativeVelocity.magnitude;
                }
                otherHealth.TakeDamage(damageDoneToOtherOnCollision);  
            }
            if (destroySelfOnCollision)
            {
                Health health = GetComponent<Health>();
                health.TakeDamage(health.MaxHealth);
            }

        }
    }

}


