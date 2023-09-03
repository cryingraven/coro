using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using admob;
public class SettingsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Admob.Instance().removeBanner();
        if (PlayerPrefs.GetInt("SoundEffects") == 1)
        {
            Toggle soundEffects = GameObject.Find("SoundEffects").GetComponent<Toggle>();
            soundEffects.isOn = true;
        }else
        {
            Toggle soundEffects = GameObject.Find("SoundEffects").GetComponent<Toggle>();
            soundEffects.isOn = false;
        }
        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            Toggle BGM = GameObject.Find("BGM").GetComponent<Toggle>();
            BGM.isOn = true;
        }
        else
        {
            Toggle BGM = GameObject.Find("BGM").GetComponent<Toggle>();
            BGM.isOn = false;
        }
    }
	void Update(){
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.LoadLevel("home");
		}
	}
}
