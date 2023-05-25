using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;

namespace SC.UI
{
    public class MissileLauncherDetailsUI : MonoBehaviour
    {
        [SerializeField] MissileLauncherDetailUI missileLanucherDetailPrefab;

        GameObject player;
        WeaponController playerWeaponConfig;
        MissileLauncher[] missileLaunchers;


        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerWeaponConfig = player.GetComponent<WeaponController>();
            missileLaunchers = playerWeaponConfig.MissileLaunchers;
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < missileLaunchers.Length; i++)
            {
                var laserDetailUI = Instantiate(missileLanucherDetailPrefab, transform);
                laserDetailUI.Setup(missileLaunchers[i], i);
            }
        }
    }
}


