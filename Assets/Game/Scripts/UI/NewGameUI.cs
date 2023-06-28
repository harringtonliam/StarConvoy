using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SC.Attributes;
using SC.SceneControl;

namespace SC.UI
{


    public class NewGameUI : MonoBehaviour
    {
        [SerializeField] Button startGameButton;
        [SerializeField] Button cancelButton;
        [SerializeField] Button leftButton;
        [SerializeField] Button rightButton;
        [SerializeField] TMP_InputField characterName;
        [SerializeField] GameObject newGameUI;
        [SerializeField] Image selectedPlayerPortrait;
        [SerializeField] Image selectedPlayerFullImage;
 
        GameObject player;
        PlayerInformationStore playerInformationStore;
        private int playerInfoKeysIndex;
        private string[] playerInfoKeys;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerInformationStore = player.GetComponent<PlayerInformationStore>();

            startGameButton.onClick.AddListener(StartGameButtonClicked);
            cancelButton.onClick.AddListener(cancelButtonClicked);
            leftButton.onClick.AddListener(LeftButtonClicked);
            rightButton.onClick.AddListener(RighButtonClicked);

            playerInfoKeysIndex = 0;
            playerInfoKeys = new string[PlayerInformation.GetPlayerInfoLookUpCache().Count];

            foreach (var item in PlayerInformation.GetPlayerInfoLookUpCache())
            {
                playerInfoKeys[playerInfoKeysIndex] = item.Key;
                playerInfoKeysIndex++;
            }
            playerInfoKeysIndex = 0;

            Redraw();
        }


        public void ShowHideUI(bool isVisible)
        {
            newGameUI.SetActive(isVisible);

        }

        public void Redraw()
        {
            PlayerInformation playerInformation = PlayerInformation.GetFromID(playerInfoKeys[playerInfoKeysIndex]);
            selectedPlayerPortrait.sprite = playerInformation.Portrait;
            selectedPlayerFullImage.sprite = playerInformation.FullImage;
        }

        private void StartGameButtonClicked()
        {
            playerInformationStore.SetPlayerInformation(characterName.text, PlayerInformation.GetFromID(playerInfoKeys[playerInfoKeysIndex]));
            SceneController.Instance.StartNewGame();
        }

        private void cancelButtonClicked()
        {
            ShowHideUI(false);
        }

        private void LeftButtonClicked()
        {
            playerInfoKeysIndex--;
            if (playerInfoKeysIndex < 0)
            {
                playerInfoKeysIndex = playerInfoKeys.Length - 1;
            }
            Redraw();
        }

        private void RighButtonClicked()
        {
            playerInfoKeysIndex++;
            if (playerInfoKeysIndex >= playerInfoKeys.Length)
            {
                playerInfoKeysIndex = 0;
            }
            Redraw();

        }
    }
}

