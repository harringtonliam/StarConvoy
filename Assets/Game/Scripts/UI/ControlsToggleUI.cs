using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SC.Core;

namespace SC.UI
{
    public class ControlsToggleUI : MonoBehaviour
    {
        [SerializeField] Toggle mouseAndKeyBoardToggle;
        [SerializeField] Toggle gameControllerToggle;
        [SerializeField] GameObject mouseAndKeyBoard;
        [SerializeField] GameObject gameController;


        private PlayerSettings playerSettings;

        // Start is called before the first frame update
        void Start()
        {
            mouseAndKeyBoardToggle.onValueChanged.AddListener(delegate { ToggleMouseAndKeyboard(mouseAndKeyBoardToggle); });
            mouseAndKeyBoardToggle.onValueChanged.AddListener(delegate { ToggleGameController(gameControllerToggle); });

            var player = GameObject.FindGameObjectWithTag("Player");
            playerSettings = player.GetComponent<PlayerSettings>();
            

            DisplayCurrentSetting();
        }

        private void DisplayCurrentSetting()
        {
            if (PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey) == PlayerSettings.UseMouseSetting)
            {
                mouseAndKeyBoard.SetActive(true);
                gameController.SetActive(false);
                mouseAndKeyBoardToggle.isOn = true;
            }
            else
            {
                gameController.SetActive(true);
                mouseAndKeyBoard.SetActive(false);
                gameControllerToggle.isOn = true;
            }
        }

        private void ToggleMouseAndKeyboard(Toggle change)
        {
            mouseAndKeyBoard.SetActive(change.isOn);
            if (change.isOn) 
            {
                PlayerPrefs.SetString(PlayerSettings.MouseOrControllerKey, PlayerSettings.UseMouseSetting);
                playerSettings.SettingsUpdated();
            }
            
        }

        private void ToggleGameController(Toggle change)
        {
            gameController.SetActive(change.isOn);
            if (change.isOn)
            {
                PlayerPrefs.SetString(PlayerSettings.MouseOrControllerKey, PlayerSettings.UseControllerSetting);
                playerSettings.SettingsUpdated();
            }

        }

    }

}
