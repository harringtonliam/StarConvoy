using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC.UI
{
    [RequireComponent(typeof(Button))]
    public class ControllsButtonUI : MonoBehaviour
    {

        Button button;
        InstructionsUI instructionsUI;

        // Start is called before the first frame update
        void Start()
        {
            button = GetComponent<Button>();

            instructionsUI = FindFirstObjectByType<InstructionsUI>();
            button.onClick.AddListener(ControllsButtonClicked);
        }

        private void ControllsButtonClicked()
        {
            if (instructionsUI == null) return;
            instructionsUI.ToggleUI(true);
        }

    }

}



