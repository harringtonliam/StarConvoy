using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Combat;
using System;

namespace SC.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageValueText;
        [SerializeField] RectTransform foregroudnHealthBar;

        GameObject player;
        Health playerHealth;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerHealth.healthUpdated += Redraw;
            Redraw();
        }

        private void OnDisable()
        {
            try
            {
                playerHealth.healthUpdated -= Redraw;
            }
            catch(Exception ex)
            {
                Debug.Log("HealthUI OnDisable " + ex.Message);
            }
        }

        private void Redraw()
        {
            
            if (foregroudnHealthBar == null) return;
            float damageTaken = playerHealth.MaxHealth - playerHealth.CurrentHealth;
            damageValueText.text = damageTaken.ToString("###0");
            Vector3 newScale = new Vector3(damageTaken / playerHealth.MaxHealth, 1, 1);
            foregroudnHealthBar.localScale = newScale;

        }

    }

}


