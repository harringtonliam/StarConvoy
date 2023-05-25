using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;


namespace SC.UI
{
    public class LaserDetailsUI : MonoBehaviour
    {
        [SerializeField] LaserDetailUI laserDetailPrefab;

        GameObject player;
        WeaponController playerWeaponConfig;
        LaserWeapon[] laserWeapons;


        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerWeaponConfig = player.GetComponent<WeaponController>();
            laserWeapons = playerWeaponConfig.LaserWeapons;
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < laserWeapons.Length; i++)
            {
                var laserDetailUI = Instantiate(laserDetailPrefab, transform);
                laserDetailUI.Setup(laserWeapons[i],i);
            }
        }


    }

}

