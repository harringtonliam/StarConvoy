using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;
using TMPro;
using SC.Attributes;
using SC.Control;

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
            EnabledBehaviour();

        }

        void EnabledBehaviour()
        {
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
            player.GetComponent<PlayerController>().EnableDisablePlayerControl(false);
            uiCanvas.SetActive(true);
            CursorControl.Instance.SetNewCursor(CursorType.UICursor);
            DisplayScore();
        }

        private void ShowPlayerDestroyedUI()
        {
            player.GetComponent<PlayerController>().EnableDisablePlayerControl(false);
            playerDestroyedUICanvas.SetActive(true);
        }

        private void DisplayScore()
        {
            sceneScore = player.GetComponent<Score>().CalculateScoreForScene();

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
            SceneController.Instance.MoveToNextScene();
        }

        private void AddSceneScoreToPlayerScore()
        {
       
            Score playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
            playerScore.AddToScore(sceneScore);
        }

        public void PlayAgainButtonClicked()
        {
            player.GetComponent<PlayerController>().EnableDisablePlayerControl(true);
            SceneController.Instance.RestartCurrentScene();
        }

        public void QuitButtonClicked()
        {
            SceneController.Instance.LoadMainMenu();
        }
    }
}
