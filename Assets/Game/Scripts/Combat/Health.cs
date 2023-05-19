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

        public event Action healthUpdated;

        public event Action onDeath;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

 
        public void TakeDamage(float damage)
        {
            Debug.Log("Take Damage" + gameObject.name + " " + damage.ToString());
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
            if (onDeath != null)
            {
                onDeath();
            }
            PlayDeathVFX();
            PlayDeathSFX();
            Destroy(gameObject, destroyDelaySeconds);
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
    }



}


