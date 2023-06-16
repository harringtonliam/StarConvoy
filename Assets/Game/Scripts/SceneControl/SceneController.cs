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
            if (onSceneEnded != null)
            {
                onSceneEnded();
            }

        }

        public void MoveToNextScene()
        {

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

            Debug.Log("Check end scene conditions not safe convoy ships " + TargetStore.Instance.ConvoyShipsThatAreNotSafe(playerCombatTarget.GetFaction()).Count.ToString());
            if (playerCombatTarget.GetIsHidden() && endSceneConditions.endOnPlayerHidden)
            {
                Debug.Log("Player hidden end scene");
                StartCoroutine(StartEndScene());
                return;
            }
            if(TargetStore.Instance.ConvoyShipsThatAreNotSafe(playerCombatTarget.GetFaction()).Count == 0 && endSceneConditions.endOnAllConvoySafe)
            {
                Debug.Log("Convoy safe  end scene");
                StartCoroutine(StartEndScene());
                return;
            }

            if (TargetStore.Instance.CombatTargetsNotInFaction(playerCombatTarget.GetFaction()).Count == 0 && endSceneConditions.endOnAllEnemyDestoryed)
            {
                Debug.Log("all enemy destoryed safe  end scene");
                StartCoroutine(StartEndScene());
                return;
            }
        }

        private IEnumerator StartEndScene()
        {
            yield return new WaitForSeconds(endSceneDelaySeconds);
            EndScene();
        }

    }

}


