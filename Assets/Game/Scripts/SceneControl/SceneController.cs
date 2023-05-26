using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SC.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        public static SceneController Instance {  get { return _instance; } }

        public event Action onSceneStarted;

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
            PauseGame();
            if (onSceneStarted != null)
            {
                onSceneStarted();
            }
        }

        void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
        }
    }

}


