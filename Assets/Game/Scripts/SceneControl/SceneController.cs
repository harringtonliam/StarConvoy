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
        [SerializeField] string sceneTitle = "Scene default title";
        [SerializeField] bool enablePlayerControlsOnSceneStart = true;
        [SerializeField] float endSceneDelaySeconds = 2f;
        [SerializeField] EndSceneConditions endSceneConditions;

        [Serializable]
        public struct EndSceneConditions
        {
            public bool endOnPlayerDeath;
            public bool endOnPlayerHidden;
            public bool endOnAllConvoySafe;
            public bool endOnAllConvoyDeath;
            public bool endOnAllEnemyDestoryed;
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
            PauseScene();
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
            playerJumpGateBehaviour.EnableDisablePlayerControls(enablePlayerControlsOnSceneStart);
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
            SavingProcess();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        private void SavingProcess()
        {
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper saveingWrapper = FindObjectOfType<SavingWrapper>();
            saveingWrapper.AutoSave();
            saveingWrapper.Load();
            saveingWrapper.AutoSave();
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            if(TargetStore.Instance.ConvoyShipsThatAreNotSafe(playerCombatTarget.GetFaction()).Count == 0 && endSceneConditions.endOnAllConvoySafe)
            {
                StartCoroutine(StartEndScene());
                return;
            }

            if (TargetStore.Instance.CombatTargetsNotInFaction(playerCombatTarget.GetFaction()).Count == 0 && endSceneConditions.endOnAllEnemyDestoryed)
            {
                StartCoroutine(StartEndScene());
                return;
            }
        }

        private void AddSceneScoreToPlayerScore()
        {
            Score playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
            var sceneScore = playerScore.CalculateScoreForScene();
            Debug.Log("AddSceneScoreToPlayerScore " + sceneScore.enemyDestroyed.ToString());
            playerScore.AddToScore(sceneScore);
        }

        private IEnumerator StartEndScene()
        {
            yield return new WaitForSeconds(endSceneDelaySeconds);
            EndScene();
        }

    }

}


