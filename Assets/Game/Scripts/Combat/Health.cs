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
        [SerializeField] float currentHealth;


        public float MaxHealth {  get { return maxHealth; } }
        public float CurrentHealth { get { return currentHealth; } }

        public event Action healthUpdated;

        public event Action onDeath;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

 
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (healthUpdated != null)
            {
                healthUpdated();
            }
            if (currentHealth <= 0f)
            {
                DeathProcedures(); ;
            }
        }

        private void DeathProcedures()
        {
            PlayDeathVFX();
            PlayDeathSFX();
            if (gameObject.tag != "Player")
            {
                NotifyOfDeath();
                Destroy(gameObject, destroyDelaySeconds);
            }
            else
            {
                StartCoroutine(DelayedNotifyDeath());
            }
        }

        private void PlayDeathVFX()
        {
            if (destroyVFX != null)
            {
                destroyVFX.Play();
            }
        }

        private void PlayDeathSFX()
        {
            if (destroySFX != null)
            {
                AudioSource.PlayClipAtPoint(destroySFX, transform.position);
            }
        }

        private IEnumerator DelayedNotifyDeath()
        {
            yield return new WaitForSeconds(destroyDelaySeconds);
            NotifyOfDeath();
        }

        private void NotifyOfDeath()
        {
            if (onDeath != null)
            {
                onDeath();
            }
        }
    }



}


