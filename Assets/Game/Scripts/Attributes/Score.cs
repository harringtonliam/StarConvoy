using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Saving;
using SC.Combat;

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
            return scoreInformation.shipsSaved - scoreInformation.shipsAbandoned - scoreInformation.shipsLost + scoreInformation.enemyDestroyed;
        }

        public ScoreInformation GetScore()
        {
            return currentScoreInformation;
        }

        public void AddToScore(ScoreInformation additionToScore)
        {
            currentScoreInformation.shipsSaved += additionToScore.shipsSaved;
            currentScoreInformation.shipsAbandoned += additionToScore.shipsAbandoned;
            currentScoreInformation.shipsLost += additionToScore.shipsLost;
            currentScoreInformation.enemyDestroyed += additionToScore.enemyDestroyed;
        }

        public int GetCurrentTotalScore()
        {
            return CalculateTotalScore(currentScoreInformation);
        }

        public ScoreInformation CalculateScoreForScene()
        {
            ScoreInformation sceneScore = new Score.ScoreInformation();

            var safeConvoyShips = TargetStore.Instance.SafeConvoyShipsInFaction(GetComponent<CombatTarget>().GetFaction(), true);
            sceneScore.shipsSaved = safeConvoyShips.Count;
            var abandonedConvoyShips = TargetStore.Instance.SafeConvoyShipsInFaction(GetComponent<CombatTarget>().GetFaction(), false);
            sceneScore.shipsAbandoned = abandonedConvoyShips.Count;
            var destroyedConvoyShips = TargetStore.Instance.DestroyedTargetsInFaction(GetComponent<CombatTarget>().GetFaction());
            sceneScore.shipsLost = destroyedConvoyShips.Count;
            sceneScore.enemyDestroyed = TargetStore.Instance.DestroyedTargetsNotInFaction(GetComponent<CombatTarget>().GetFaction()).Count;
            return sceneScore;
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


