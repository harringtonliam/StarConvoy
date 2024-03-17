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

        // Start is called before the first frame update
        void Start()
        {
            mouseAndKeyBoardToggle.onValueChanged.AddListener(delegate { ToggleMouseAndKeyboard(mouseAndKeyBoardToggle); });
            mouseAndKeyBoardToggle.onValueChanged.AddListener(delegate { ToggleGameController(gameControllerToggle); });
        }



        private void ToggleMouseAndKeyboard(Toggle change)
        {
            mouseAndKeyBoard.SetActive(change.isOn);
            PlayerPrefs.SetString(PlayerSettings.MouseOrControllerKey, PlayerSettings.UseMouseSetting);
        }

        public void ToggleGameController(Toggle change)
        {
            gameController.SetActive(change.isOn);
            PlayerPrefs.SetString(PlayerSettings.MouseOrControllerKey, PlayerSettings.UseControllerSetting);
        }
    }

}
