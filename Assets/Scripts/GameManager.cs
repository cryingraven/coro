using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private Text coinText;
    [SerializeField]
    private Text liveText;

    private int collectedCoins;

    private int live;

    private int maxCoro;

    private int maxLevel;

    private int currentLevel;

    private int numberCoro;

    private int numberBoss;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }


    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }

        set
        {
            coinText.text = "Score : "+value.ToString();

            PlayerPrefs.SetInt("collectedCoins", value);
            this.collectedCoins = value;
        }
    }
    public int Live
    {
        get
        {
            return live;
        }

        set
        {
            liveText.text = "Life : " + value.ToString();
            PlayerPrefs.SetInt("live", value);
            this.live = value;
        }
    }
    public int NumberCoro
    {
        get
        {
            return numberCoro;
        }

        set
        {
            PlayerPrefs.SetInt("numberCoro", value);
            this.numberCoro = value;
        }
    }
    public int NumberBoss
    {
        get
        {
            return numberBoss;
        }

        set
        {
            PlayerPrefs.SetInt("numberBoss", value);
            this.numberBoss = value;
        }
    }
    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt("HighScore");
        }

        set
        {
            PlayerPrefs.SetInt("HighScore", value);
        }
    }
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            PlayerPrefs.SetInt("currentLevel", value);
            this.currentLevel = value;
        }
    }
    public int MaxCoro
    {
        get
        {
            return maxCoro;
        }

        set
        {
            PlayerPrefs.SetInt("maxCoro", value);
            this.maxCoro = value;
        }
    }
    public int MaxLevel
    {
        get
        {
            return maxLevel;
        }
    }
    public void reset()
    {
        if (PlayerPrefs.GetInt("scoring") ==0 || !PlayerPrefs.HasKey("scoring"))
        {
            this.collectedCoins = 0;
            this.live = 3;
            this.maxCoro = 5;
            this.maxLevel = 40;
            this.currentLevel = 1;
            this.numberCoro = 1;
            this.numberBoss = 0;
            PlayerPrefs.SetInt("scoring", 1);
            PlayerPrefs.SetInt("collectedCoins", this.collectedCoins);
            PlayerPrefs.SetInt("live", this.live);
            PlayerPrefs.SetInt("maxCoro", this.maxCoro);
            PlayerPrefs.SetInt("maxLevel", this.maxLevel);
            PlayerPrefs.SetInt("currentLevel", this.currentLevel);
            PlayerPrefs.SetInt("numberCoro", this.numberCoro);
            PlayerPrefs.SetInt("numberBoss", this.numberBoss);
            coinText.text = "Score : " + this.collectedCoins;
            liveText.text = "Life : " + this.live;
        }
        else
        {
            this.collectedCoins =PlayerPrefs.GetInt("collectedCoins");
            this.live = PlayerPrefs.GetInt("live");
            this.maxCoro = PlayerPrefs.GetInt("maxCoro");
            this.maxLevel = PlayerPrefs.GetInt("maxLevel");
            this.currentLevel = PlayerPrefs.GetInt("currentLevel");
            this.numberCoro = PlayerPrefs.GetInt("numberCoro");
            this.numberBoss = PlayerPrefs.GetInt("numberBoss");
            coinText.text = "Score : " + this.collectedCoins;
            liveText.text = "Life : " + this.live;
        }
    }
}
