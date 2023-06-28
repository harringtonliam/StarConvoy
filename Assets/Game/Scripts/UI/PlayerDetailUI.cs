using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] TextMeshProUGUI captainNameText;
        [SerializeField] TextMeshProUGUI shipTypeText;
        [SerializeField] Image captianImage;
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
            PlayerInformationStore playerInformationStore = player.GetComponent<PlayerInformationStore>();

            var shipDetails = shipInformation.GetShipDetails();
            captainNameText.text = playerInformationStore.PlayerName;
            shipTypeText.text = shipDetails.shipType;
            captianImage.sprite = playerInformationStore.PlayerInformation.Portrait; 
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

            CreateDeatilsUi("Laser Range", weaponController.LaserWeapons[0].Range.ToString());

            CreateDeatilsUi("Laser Damage", weaponController.LaserWeapons[0].Damage.ToString());

            CreateDeatilsUi("Laser Rate of Fire", weaponController.LaserWeapons[0].GetRateOfFire().ToString());
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


