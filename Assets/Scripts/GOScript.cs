using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using admob;
public class GOScript : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
    // Use this for initialization
    void Start () {
		Admob ad = Admob.Instance();
		ad.initAdmob("ca-app-pub-1352957177021550/4590395020", "ca-app-pub-1352957177021550/3967626229");
		Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.TOP_CENTER, 0);
        scoreText.text = "Score : " + PlayerPrefs.GetInt("collectedCoins");
		if (ad.isInterstitialReady())
		{
			ad.showInterstitial();
		}
		else
		{
			ad.loadInterstitial();
		}
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.LoadLevel("home");
		}
	}
}
