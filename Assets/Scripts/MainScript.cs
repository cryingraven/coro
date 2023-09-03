using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using admob;
public class MainScript : MonoBehaviour {
    private AudioSource audio;
    void Start () {
		Admob.Instance().removeBanner();
        this.audio = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            this.audio.volume = 0.771f;
        }
        else
        {
            this.audio.volume = 0;
        }
    }
	void Update(){
		if (Input.GetKeyUp (KeyCode.Escape)) {
			PlayerPrefs.SetInt("Paused", 1);
			SceneManager.LoadScene("paused", LoadSceneMode.Additive);
			AudioListener.pause = true;
			Time.timeScale = 0f;
		}
	}
}
