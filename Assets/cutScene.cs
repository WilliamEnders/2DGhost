using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutScene : MonoBehaviour {


    [System.Serializable]
    public class CutScene {

        public Transform focus;
        public string whoTalk;
        public string talk;

    }

    public CutScene[] cut;
}
