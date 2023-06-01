using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SC.SceneControl;
using SC.Combat;
using TMPro;
using System;

namespace SC.UI
{
    public class EndSceneCanvasUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] GameObject playerDestroyedUICanvas = null;
        [SerializeField] TextMeshProUGUI enemyDestroyedText;
        [SerializeField] TextMeshProUGUI totalScoreText;

        GameObject player;

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
            var safeConvoyShips = TargetStore.Instance.SafeCombatTargetsInFaction(player.GetComponent<CombatTarget>().GetFaction(), true);
            int savedConvoyShipsTotal = safeConvoyShips.Count;
            var abandonedConvoyShips = TargetStore.Instance.SafeCombatTargetsInFaction(player.GetComponent<CombatTarget>().GetFaction(), false);
            int abandonedConvoyShipsTotal = safeConvoyShips.Count;
            var destroyedConvoyShips = TargetStore.Instance.SafeCombatTargetsInFaction(player.GetComponent<CombatTarget>().GetFaction(), false);
            int destroyedConvoyShipsTotal = safeConvoyShips.Count;

            var enemyCombatTargets = TargetStore.Instance.CombatTargetsNotInFaction(player.GetComponent<CombatTarget>().GetFaction());
            int destroyedEnemyTargetsTotal = TargetStore.Instance.DestroyedTargetsNotInFaction(player.GetComponent<CombatTarget>().GetFaction()).Count;

            enemyDestroyedText.text = destroyedEnemyTargetsTotal.ToString();
            int totalScore = CalculateTotalScore(savedConvoyShipsTotal, abandonedConvoyShipsTotal, destroyedConvoyShipsTotal, destroyedEnemyTargetsTotal);
            totalScoreText.text = totalScore.ToString();
        }

        public void ToggleUI()
        {
            uiCanvas.SetActive(!uiCanvas.activeSelf);
        }

        private int  CalculateTotalScore(int savedConvoyShipsTotal, int abandonedConvoyShipsTotal, int destroyedConvoyShipsTotal, int destroyedEnemyTargetsTotal)
        {
            int totalScore = 0;

            totalScore += savedConvoyShipsTotal;
            totalScore -= abandonedConvoyShipsTotal;
            totalScore -= (destroyedConvoyShipsTotal * 2);
            totalScore += destroyedEnemyTargetsTotal;

            return totalScore;
        }

        public void NextSceneButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
