using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SC.Attributes;
using SC.Movement;
using SC.Combat;

namespace SC.UI
{
    public class PlayerDetailUI : MonoBehaviour
    {
        [SerializeField] PlayerDetailItem playerDetailItemPrefab;
        [SerializeField] TextMeshProUGUI shipTypeText;
        [SerializeField] Image shipImage;


        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            SetShipInfo();
            SetShipSpeed();
            SetLaserWeapons();
            SetMissleLauchers();
            SetDamage();
        }

        private void SetShipInfo()
        {
            ShipInformation shipInformation = player.GetComponent<ShipInformation>();
            var shipDetails = shipInformation.GetShipDetails();
            shipTypeText.text = shipDetails.shipType;
            shipImage.sprite = shipDetails.shipSprite;
        }

        private void SetShipSpeed()
        {
            Move move = player.GetComponent<Move>();
            Rotate rotate = player.GetComponent<Rotate>();
            CreateDeatilsUi("Speed", string.Empty);
            CreateDeatilsUi("Max Speed", move.MaxSpeed.ToString());
            CreateDeatilsUi("Acceleration", move.GetRateOfAcceleration().ToString());
            CreateDeatilsUi("Turn Speed", rotate.TurnSpeed.ToString());
        }

        private void SetLaserWeapons()
        {
            WeaponController weaponController = player.GetComponent<WeaponController>();
            CreateDeatilsUi("Weapons", string.Empty);
            CreateDeatilsUi("Laser Weapons", weaponController.LaserWeapons.Length.ToString());

            StringBuilder laserRange = new StringBuilder();
            StringBuilder laserDamage = new StringBuilder();
            StringBuilder laserRateOfFire = new StringBuilder();
            for (int i = 0; i < weaponController.LaserWeapons.Length; i++)
            {
                laserRange.Append(weaponController.LaserWeapons[i].Range.ToString() + " / ");
                laserDamage.Append(weaponController.LaserWeapons[i].Damage.ToString() + " / ");
                laserRateOfFire.Append(weaponController.LaserWeapons[i].GetRateOfFire().ToString("#0.##") + " / ");
            }

            CreateDeatilsUi("Laser Range", laserRange.ToString());

            CreateDeatilsUi("Laser Damage", laserDamage.ToString());

            CreateDeatilsUi("Laser Rate of Fire", laserRateOfFire.ToString());
        }

        private void SetMissleLauchers()
        {
            WeaponController weaponController = player.GetComponent<WeaponController>();
            CreateDeatilsUi("Missle Lauchers", weaponController.MissileLaunchers.Length.ToString());
            CreateDeatilsUi("Missiles Per Laucher", weaponController.MissileLaunchers[0].MaxNumberOfMissiles.ToString());
        }

        private void SetDamage()
        {
            Health health = player.GetComponent<Health>();
            CreateDeatilsUi("Defence", string.Empty);
            CreateDeatilsUi("Max Damage", health.MaxHealth.ToString());
        }

        private void  CreateDeatilsUi(string label, string value)
        {
            var newDetails = Instantiate(playerDetailItemPrefab, transform);
            newDetails.Setup(label, value);
        }

    }
}


