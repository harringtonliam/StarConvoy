using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SC.Messaging;

namespace SC.UI
{
    public class ShipDetailUI : MonoBehaviour
    {
        [SerializeField] Image captainImage;
        [SerializeField] Image shipImage;
        [SerializeField] TextMeshProUGUI captainNameText;
        [SerializeField] TextMeshProUGUI shipNameText;
        [SerializeField] TextMeshProUGUI shipTypeText;

        public void SetUp(Sprite captainSprit, string captainName, Sprite shipSprit, string shipName, string shipType )
        {
            this.captainImage.sprite = captainSprit;
            captainNameText.text = captainName;
            shipNameText.text = shipName;
            this.shipImage.sprite = shipSprit;
            shipTypeText.text = shipType;
        }
    }
}