using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSceneManager : MonoBehaviour {
	
	[System.Serializable]
	public class CutScene{

		public Transform focus;
		public string whoTalk;
		public string talk;

	}

	public CutScene[] cut;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
