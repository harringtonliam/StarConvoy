using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SC.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {


        public void PlayTimeline()
        {
            Debug.Log("Cinematic trigge rplay timeline");
            GetComponent<PlayableDirector>().Play();
        }

    }

}


