using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using TMPro;


namespace SC.UI
{
    public class LaserDetailUI : MonoBehaviour
    {
        [SerializeField] LaserWeapon laserWeapon;
        [SerializeField] TextMeshProUGUI weaponNumber;
        [SerializeField] RectTransform foregroundTemperatureBar;

        private void OnDisable()
        {
            try
            {
                laserWeapon.temperatureUpdated -= Redraw;
            }
            catch (System.Exception ex)
            {
                Debug.Log("LaserDetailUI unable to unredisger tempreatureUpdated " + ex.Message);
            }
            
        }

        public void Setup(LaserWeapon weapon, int number)
        {
            laserWeapon = weapon;
            laserWeapon.temperatureUpdated += Redraw;
            weaponNumber.text = number.ToString();
            Redraw();
        }

        private void Redraw()
        {
            if (foregroundTemperatureBar == null) return;
            Vector3 newScale = new Vector3(laserWeapon.GetCurrentTemperature() / laserWeapon.GetMaxTemperature(), 1, 1);
            foregroundTemperatureBar.localScale = newScale;
        }
    }


}

