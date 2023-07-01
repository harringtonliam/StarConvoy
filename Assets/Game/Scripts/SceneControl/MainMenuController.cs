using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SC.SceneControl
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] SceneTransition sceneTransition;


        private static MainMenuController _instance;

        public static MainMenuController Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartNewGame()
        {
            sceneTransition.TransitionToNextScene("");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}


