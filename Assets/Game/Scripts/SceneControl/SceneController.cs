using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using SC.Combat;
using SC.JumpGate;
using SC.Attributes;

namespace SC.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] SceneTransition sceneTransition;
        [SerializeField] string sceneTitle = "Main Menu";
        [SerializeField] bool enablePlayerControlsOnSceneStart = true;
        [SerializeField] float endSceneDelaySeconds = 2f;
        [SerializeField] EndSceneConditions endSceneConditions;
        [SerializeField] bool isPauseSceneOnStart = true;
        [SerializeField] bool abortEndsScene = false;

        [Serializable]
        public struct EndSceneConditions
        {
            public bool endOnPlayerDeath;
            public bool endOnPlayerHidden;
            public bool endOnAllConvoySafe;
            public bool endOnAllConvoyDeath;
            public bool endOnAllEnemyDestroyed;
            public bool endOnAllEnemyDestroyedAndAllConvoySafe;
            public CombatTarget endOnThisTargetDestroyed;
        }

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
       
        private bool isPaused = false;


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
            playerHealth = player.GetComponent<Health>();
            playerJumpGateBehaviour = player.GetComponent<JumpGateBehaviour>();
            playerHealth.onDeath += PlayerDestroyed;
            TargetStore.Instance.TargetStoreUpdated += CheckEndSceneConditions;
        }

        private void OnDisable()
        {
            //playerCombatTarget.onIsHidden -= EndScene;
            playerHealth.onDeath -= PlayerDestroyed;
            TargetStore.Instance.TargetStoreUpdated -= CheckEndSceneConditions;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isPauseSceneOnStart)
            {
                PauseScene();
            }

            playerJumpGateBehaviour.EnableDisablePlayerControls(false);
            if (onSceneStarted != null)
            {
                onSceneStarted();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AbortScene();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }

        void PauseScene()
        {
            Time.timeScale = 0f;
        }

        private void TogglePause()
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                PauseScene();
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        public void StartScene()
        {
            Time.timeScale = 1f;
            playerJumpGateBehaviour.EnableDisablePlayerControls(enablePlayerControlsOnSceneStart);
        }
        

        private void AbortScene()
        {
            if (abortEndsScene)
            {
                EndScene();
            }
            else
            {
                RestartCurrentScene();
            }
        }

        public void EndScene()
        {
            PauseScene();
            AddSceneScoreToPlayerScore();
            if (onSceneEnded != null)
            {
                onSceneEnded();
            }
        }



        public void MoveToNextScene()
        {
            sceneTransition.TransitionToNextScene();
        }

        public void StartNewGame()
        {
            sceneTransition.StartNewGame();
        }




        public void RestartCurrentScene()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.LoadSavedGame(savingWrapper.MostRecentSavedGame);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void PlayerDestroyed()
        {
            if (!endSceneConditions.endOnPlayerDeath) return;
            PauseScene();
            if (onPlayerDestroyed != null)
            {
                onPlayerDestroyed();
            }
        }
        
        private void CheckEndSceneConditions()
        {
            if (playerCombatTarget.GetIsHidden() && endSceneConditions.endOnPlayerHidden)
            {
                StartCoroutine(StartEndScene());
                return;
            }

            if(endSceneConditions.endOnAllEnemyDestroyedAndAllConvoySafe)
            {
                if(TargetStore.Instance.ConvoyShipsThatAreNotSafe(playerCombatTarget.GetFaction()).Count == 0 && TargetStore.Instance.CombatTargetsNotInFaction(playerCombatTarget.GetFaction()).Count == 0)
                {
                    StartCoroutine(StartEndScene());
                    return;
                }

            }
       
            if (TargetStore.Instance.ConvoyShipsThatAreNotSafe(playerCombatTarget.GetFaction()).Count == 0 && endSceneConditions.endOnAllConvoySafe)
            {
            StartCoroutine(StartEndScene());
                return;
            }

            if (TargetStore.Instance.CombatTargetsNotInFaction(playerCombatTarget.GetFaction()).Count == 0 && endSceneConditions.endOnAllEnemyDestroyed)
            {
                StartCoroutine(StartEndScene());
                return;
            }
         


        }

        private void AddSceneScoreToPlayerScore()
        {
            Score playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
            var sceneScore = playerScore.CalculateScoreForScene();
            playerScore.AddToScore(sceneScore);
        }

        private IEnumerator StartEndScene()
        {
            yield return new WaitForSeconds(endSceneDelaySeconds);
            EndScene();
        }

    }

}


