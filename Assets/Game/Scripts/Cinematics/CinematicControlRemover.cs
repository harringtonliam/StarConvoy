using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SC.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector playableDirector;

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            playableDirector.played += DisablePlayerControls;
            playableDirector.stopped += EnablePlayerControls;
        }

        private void OnDisable()
        {
            playableDirector.played -= DisablePlayerControls;
            playableDirector.stopped -= EnablePlayerControls;
        }


        void EnablePlayerControls(PlayableDirector pb)
        {


        }

        void DisablePlayerControls(PlayableDirector pb)
        {


        }

        
    }

}

