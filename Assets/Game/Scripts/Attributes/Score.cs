using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Saving;

namespace SC.Attributes
{
    public class Score : MonoBehaviour, ISaveable
    {


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public object CaptureState()
        {
            throw new System.NotImplementedException();
        }

        public void RestoreState(object state)
        {
            throw new System.NotImplementedException();
        }
    }

}


