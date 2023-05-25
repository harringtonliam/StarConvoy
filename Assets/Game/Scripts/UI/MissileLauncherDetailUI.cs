using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using TMPro;

namespace SC.UI
{
    public class MissileLauncherDetailUI : MonoBehaviour
    {
        [SerializeField] MissileLauncher missileLauncher;
        [SerializeField] TextMeshProUGUI weaponNumber;
        [SerializeField] TextMeshProUGUI remaininMissilesText;
        [SerializeField] RectTransform foregroundMissileBar;

        private void OnDisable()
        {
            try
            {
                missileLauncher.missileNumberUpdated -= Redraw;
            }
            catch (System.Exception ex)
            {
                Debug.Log("MissileLauncherDetailUI unable to unregister missileNumberUpdated " + ex.Message);
            }
        }

        public void Setup(MissileLauncher weapon, int number)
        {
            missileLauncher = weapon;
            missileLauncher.missileNumberUpdated += Redraw;
            weaponNumber.text = number.ToString();
            Redraw();

        }

        private void Redraw()
        {
            if (foregroundMissileBar == null) return;
            float remainingMissilesFraction = (float)missileLauncher.CurrentNumberOfMissiles / (float)missileLauncher.MaxNumberOfMissiles;
            Vector3 newScale = new Vector3(remainingMissilesFraction, 1, 1);
            foregroundMissileBar.localScale = newScale;
            remaininMissilesText.text = missileLauncher.CurrentNumberOfMissiles.ToString();
        }
    }
}

