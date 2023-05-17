using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 500f;
        [SerializeField] ParticleSystem destroyVFX;
        [SerializeField] AudioClip destroySFX;
        [SerializeField] float destroyDelaySeconds = 1f;

        float currentHealth;

        public float MaxHealth {  get { return maxHealth; } }
        public float CurrentHealth { get { return currentHealth; } }

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        


        public void TakeDamage(float damage)
        {
            Debug.Log("Take Damage" + gameObject.name + " " + damage.ToString());
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                DestroyProcedures(); ;
            }

        }

        private void DestroyProcedures()
        {
            PlayDestroyVFX();
            PlayDestroySFX();
            Destroy(gameObject, destroyDelaySeconds);
        }

        private void PlayDestroyVFX()
        {
            if (destroyVFX != null)
            {
                destroyVFX.Play();
            }
        }

        private void PlayDestroySFX()
        {
            if (destroySFX != null)
            {
                AudioSource.PlayClipAtPoint(destroySFX, transform.position);
            }
        }
    }



}


