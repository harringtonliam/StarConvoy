using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SC.Combat;
using SC.JumpGate;

namespace SC.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] string sceneTitle = "Scene default title";
        [SerializeField] bool enablePlayerCntrolsOnSceneStart = true;

        private static SceneController _instance;

        public static SceneController Instance {  get { return _instance; } }

        GameObject player;
        CombatTarget playerCombatTarget;
        Health playerHealth;
        JumpGateBehaviour playerJumpGateBehaviour;

        public event Action onSceneStarted;
        public event Action onSceneEnded;
        public event Action onPlayerDestroyed;

        public string SceneTitle {  get { return sceneTitle; } }


        private void Awake()
        {
            Debug.Log("***SceneController Awake***");
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
            Debug.Log("***SceneController onEnable***");
            player = GameObject.FindGameObjectWithTag("Player");
            playerCombatTarget = player.GetComponent<CombatTarget>();
            playerHealth = player.GetComponent<Health>();
            playerJumpGateBehaviour = player.GetComponent<JumpGateBehaviour>();
            playerCombatTarget.onIsHidden += EndScene;
            playerHealth.onDeath += PlayerDestroyed;
        }

        private void OnDisable()
        {
            playerCombatTarget.onIsHidden -= EndScene;
            playerHealth.onDeath -= PlayerDestroyed;
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("***SceneController Start***");
            PauseScene();
            playerJumpGateBehaviour.EnableDisablePlayerControls(false);
            if (onSceneStarted != null)
            {
                Debug.Log("***SceneController trigger onSceneStarted***");
                onSceneStarted();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndScene();
            }
        }

        void PauseScene()
        {
            Time.timeScale = 0f;
        }

        

        public void StartScene()
        {
            Time.timeScale = 1f;
            playerJumpGateBehaviour.EnableDisablePlayerControls(enablePlayerCntrolsOnSceneStart);
        }
        

        public void EndScene()
        {
            Debug.Log("SCENE ENDED");
            PauseScene();
            if (onSceneEnded != null)
            {
                onSceneEnded();
            }

        }

        private void PlayerDestroyed()
        {
            Debug.Log("SCENE ENDED player destroyed");
            PauseScene();
            if (onPlayerDestroyed != null)
            {
                onPlayerDestroyed();
            }
        }
        
    }

}


