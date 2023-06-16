using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SC.SceneControl;
using SC.Combat;
using TMPro;
using SC.Attributes;
using UnityEngine.UI;

namespace SC.UI
{
    public class EndSceneCanvasUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] GameObject playerDestroyedUICanvas = null;
        [SerializeField] TextMeshProUGUI enemyDestroyedText;
        [SerializeField] TextMeshProUGUI totalScoreText;

        GameObject player;

        Score.ScoreInformation sceneScore;


        // Start is called before the first frame update
        void OnEnable()
        {
            StartCoroutine(EnabledBehaviour());
        }

        IEnumerator EnabledBehaviour()
        {
            yield return new WaitForSeconds(2f);
            SceneController.Instance.onSceneEnded += ShowEndSceneUI;
            SceneController.Instance.onPlayerDestroyed += ShowPlayerDestroyedUI;
        }

        private void OnDisable()
        {
            SceneController.Instance.onSceneEnded -= ShowEndSceneUI;
            SceneController.Instance.onPlayerDestroyed -= ShowPlayerDestroyedUI;
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void ShowEndSceneUI()
        {
            uiCanvas.SetActive(true);
            CalculateScore();
        }

        private void ShowPlayerDestroyedUI()
        {
            playerDestroyedUICanvas.SetActive(true);
        }

        private void CalculateScore()
        {
            sceneScore = new Score.ScoreInformation();

            var safeConvoyShips = TargetStore.Instance.SafeConvoyShipsInFaction(player.GetComponent<CombatTarget>().GetFaction(), true);
            sceneScore.shipsSaved = safeConvoyShips.Count;
            var abandonedConvoyShips = TargetStore.Instance.SafeConvoyShipsInFaction(player.GetComponent<CombatTarget>().GetFaction(), false);
            sceneScore.shipsAbandoned = abandonedConvoyShips.Count;
            var destroyedConvoyShips = TargetStore.Instance.DestroyedTargetsInFaction(player.GetComponent<CombatTarget>().GetFaction());
            sceneScore.shipsLost = destroyedConvoyShips.Count;

            //var enemyCombatTargets = TargetStore.Instance.CombatTargetsNotInFaction(player.GetComponent<CombatTarget>().GetFaction());
            sceneScore.enemyDestroyed = TargetStore.Instance.DestroyedTargetsNotInFaction(player.GetComponent<CombatTarget>().GetFaction()).Count;

            enemyDestroyedText.text = sceneScore.enemyDestroyed.ToString();
            int totalScore = Score.CalculateTotalScore(sceneScore);
            totalScoreText.text = totalScore.ToString();
        }

        public void ToggleUI()
        {
            uiCanvas.SetActive(!uiCanvas.activeSelf);
        }



        public void NextSceneButtonClicked()
        {
            AddSceneScoreToPlayerScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void AddSceneScoreToPlayerScore()
        {
            Score playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
            playerScore.AddToScore(sceneScore);
        }

        public void PlayAgainButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitButtonClicked()
        {
            SceneManager.LoadScene(0);
        }
    }
}
