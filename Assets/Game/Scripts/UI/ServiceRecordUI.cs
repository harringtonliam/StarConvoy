using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Attributes;
using TMPro;

namespace SC.UI
{
    public class ServiceRecordUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI shipsSavedText;
        [SerializeField] TextMeshProUGUI shipsAbandonedText;
        [SerializeField] TextMeshProUGUI shipsLostText;
        [SerializeField] TextMeshProUGUI enemyDestroyedText;
        [SerializeField] TextMeshProUGUI totalScoreText;


        GameObject player;
        Score playerScore;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScore = player.GetComponent<Score>();
            Redraw();
        }

        public void Redraw()
        {
            Score.ScoreInformation scoreInformation = playerScore.GetScore();

            shipsSavedText.text = scoreInformation.shipsSaved.ToString();
            shipsAbandonedText.text = scoreInformation.shipsAbandoned.ToString();
            shipsLostText.text = scoreInformation.shipsLost.ToString();
            enemyDestroyedText.text = scoreInformation.enemyDestroyed.ToString();
            totalScoreText.text = playerScore.GetCurrentTotalScore().ToString();
        }


    }
}



