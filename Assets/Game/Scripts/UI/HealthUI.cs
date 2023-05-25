using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Combat;

namespace SC.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthValueText;
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
            playerHealth.healthUpdated -= Redraw;
        }

        private void Redraw()
        {
            healthValueText.text = playerHealth.CurrentHealth.ToString("###0");
            if (foregroudnHealthBar == null) return;
            Vector3 newScale = new Vector3(playerHealth.CurrentHealth / playerHealth.MaxHealth, 1, 1);
            foregroudnHealthBar.localScale = newScale;

        }

    }

}


