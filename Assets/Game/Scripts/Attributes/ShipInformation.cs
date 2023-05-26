using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Attributes
{
    public class ShipInformation : MonoBehaviour
    {
        [SerializeField] ShipDetails shipDetails;


        [System.Serializable]
        public struct ShipDetails
        {
            public string shipName;
            public string captainName;
            public string shipType;
            public Sprite shipSprite;
            public Sprite captainSprite;
        }

        public ShipDetails GetShipDetails()
        {
            return shipDetails;
        }


    }

}


