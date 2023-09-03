using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using admob;
using Facebook.Unity;
public class Functions : MonoBehaviour {
    void Awake()
    {
		/*
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
        */
    }

    private void InitCallback()
    {
       if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FB.FeedShare(
             linkName: "Coro Mania",
				linkCaption: "My Score in Coro Mania : " + PlayerPrefs.GetInt("collectedCoins"),
             linkDescription: "Let's Play Coro Mania !!!",
             picture: new System.Uri("http://limadigit.com/images/apps/Logo-Coro-Mania.png"),
             callback: FeedCallback
           );
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void NextLevelButton(int index){
        AudioListener.pause = false;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("scoring", 0);
        PlayerPrefs.SetInt("Paused", 0);
        Application.LoadLevel(index);
    }
 
    public void NextLevelButton(string levelName){
        AudioListener.pause = false;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("scoring", 0);
        PlayerPrefs.SetInt("Paused", 0);
        Application.LoadLevel(levelName);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        PlayerPrefs.SetInt("Paused", 1);
        SceneManager.LoadScene("paused", LoadSceneMode.Additive);
        AudioListener.pause = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
		Admob.Instance().removeBanner();
        PlayerPrefs.SetInt("Paused", 0);
        AudioListener.pause = false;
        SceneManager.UnloadScene("paused");
        Time.timeScale = 1f;
    }
    public void setSoundEffects()
    {
        Toggle soundEffects = GameObject.Find("SoundEffects").GetComponent<Toggle>();
        if (soundEffects.isOn)
        {
            PlayerPrefs.SetInt("SoundEffects", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SoundEffects", 0);
        }
    }
    public void setBGM()
    {
        Toggle BGM = GameObject.Find("BGM").GetComponent<Toggle>();
        if (BGM.isOn)
        {
            PlayerPrefs.SetInt("BGM", 1);
        }
        else
        {
            PlayerPrefs.SetInt("BGM", 0);
        }
    }
    public void shareFB()
    {
        
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
			FB.FeedShare(
				linkName: "Coro Mania",
				linkCaption: "My Score in Coro Mania : " + PlayerPrefs.GetInt("collectedCoins"),
				linkDescription: "Let's Play Coro Mania !!!",
				picture: new System.Uri("http://limadigit.com/images/apps/Logo-Coro-Mania.png"),
				callback: FeedCallback
			);
        }
    }
    public void FeedCallback(IResult response)
    {

    }
    public void openUrl()
    {
        Application.OpenURL("http://limadigit.com");
    }
    string ReplaceSpace(string val)
    {
        return val.Replace(" ", "%20");
    }
}