using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.SceneControl;

namespace SC.UI
{
    public class SceneTitleUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI sceneTitletext = null;

        // Start is called before the first frame update
        void Start()
        {
            if (SceneController.Instance == null || sceneTitletext == null) return;

            sceneTitletext.text = SceneController.Instance.SceneTitle;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}


