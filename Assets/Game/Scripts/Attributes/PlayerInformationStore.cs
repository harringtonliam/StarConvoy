using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Saving;

namespace SC.Attributes
{
    public class PlayerInformationStore : MonoBehaviour, ISaveable
    {
        [SerializeField] string playerName;
        [SerializeField] PlayerInformation playerInformation;


        public string PlayerName {  get { return playerName; } }
        public PlayerInformation PlayerInformation {  get { return playerInformation; } }


        [System.Serializable]
        public struct PlayerInfoSerializable
        {
            public string playerName;
            public string playerinfoID;

        }

        public void SetPlayerInformation(string newPlayerName, PlayerInformation newPlayerInformation)
        {
            playerName = newPlayerName;
            playerInformation = newPlayerInformation;
        }

        public object CaptureState()
        {
            PlayerInfoSerializable playerInfoSerializable = new PlayerInfoSerializable();
            playerInfoSerializable.playerName = playerName;
            playerInfoSerializable.playerinfoID = playerInformation.PlayerinfoId;
            return playerInfoSerializable;

        }

        public void RestoreState(object state)
        {
            PlayerInfoSerializable playerInfoSerializable = (PlayerInfoSerializable)state;
            this.playerName = playerInfoSerializable.playerName;
            this.playerInformation = PlayerInformation.GetFromID(playerInfoSerializable.playerinfoID);
        }
    }


}



