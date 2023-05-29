using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SC.Combat;

namespace SC.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        public static SceneController Instance {  get { return _instance; } }

        GameObject player;
        CombatTarget playerCombatTarget;

        public event Action onSceneStarted;
        public event Action onSceneEnded;

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

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerCombatTarget = player.GetComponent<CombatTarget>();
            playerCombatTarget.onIsHidden += EndScene;
        }

        private void OnDisable()
        {
            playerCombatTarget.onIsHidden -= EndScene;
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

        public void StartScene()
        {
            Time.timeScale = 1f;
        }

        public void EndScene()
        {
            Debug.Log("SCENE ENDED");
            PauseGame();
            if (onSceneEnded != null)
            {
                onSceneEnded();
            }

        }
    }

}


