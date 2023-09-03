using UnityEngine;
using System.Collections;
using admob;
public class CreditsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Admob.Instance().removeBanner();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.LoadLevel("home");
		}
	}
}
