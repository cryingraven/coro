using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using admob;
public class HomeScript : MonoBehaviour {
    [SerializeField]
    private Text HighScoreText;
    // Use this for initialization
    void Start()
    {
		Admob ad = Admob.Instance();
		ad.initAdmob("ca-app-pub-1352957177021550/4590395020", "ca-app-pub-1352957177021550/3967626229");
		Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScoreText.text = "Best Score : " + PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            HighScoreText.text = "Best Score : 0";
        }
        if (!PlayerPrefs.HasKey("SoundEffects") || !PlayerPrefs.HasKey("BGM"))
        {
            PlayerPrefs.SetInt("SoundEffects", 1);
            PlayerPrefs.SetInt("BGM", 1);
        }
        if (PlayerPrefs.GetInt("BGM") == 0)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.volume = 0;
        }
    }
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
