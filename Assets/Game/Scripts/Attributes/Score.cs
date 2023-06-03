using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Saving;

namespace SC.Attributes
{
    public class Score : MonoBehaviour, ISaveable
    {
        [SerializeField] ScoreInformation currentScoreInformation;


        [System.Serializable]
        public struct ScoreInformation
        {
            public int shipsSaved;
            public int shipsAbandoned;
            public int shipsLost;
            public int enemyDestroyed;
        }

        public static int CalculateTotalScore( ScoreInformation scoreInformation)
        {
            return scoreInformation.shipsSaved = scoreInformation.shipsAbandoned - scoreInformation.shipsLost + scoreInformation.enemyDestroyed;

        }

        public ScoreInformation GetScore()
        {
            return currentScoreInformation;
        }

        public void AddToScore(ScoreInformation additionToScore)
        {
            currentScoreInformation.shipsSaved += additionToScore.shipsSaved;
            currentScoreInformation.shipsAbandoned += currentScoreInformation.shipsAbandoned;
            currentScoreInformation.shipsLost += currentScoreInformation.shipsLost;
            currentScoreInformation.enemyDestroyed += currentScoreInformation.enemyDestroyed;
        }

        public int GetCurrentTotalScore()
        {
            return CalculateTotalScore(currentScoreInformation);
        }

        public object CaptureState()
        {
            return currentScoreInformation;
        }

        public void RestoreState(object state)
        {
            currentScoreInformation = (ScoreInformation)state;
        }
    }

}


