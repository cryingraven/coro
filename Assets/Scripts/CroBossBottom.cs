using UnityEngine;
using System.Collections;

public class CroBossBottom : MonoBehaviour {
	private Animator myanimator;
	private Rigidbody2D myrigid;
	private bool Die = false;
	private int bossHealth;
	private SpriteRenderer sprite;
	// Use this for initialization
	void Start()
	{
		this.myanimator = GetComponent<Animator>();
		this.myrigid = GetComponent<Rigidbody2D>();
		this.sprite = GetComponent<SpriteRenderer> ();
		this.myrigid.position = new Vector3(GenerateX(), GenerateY(), myrigid.transform.position.z);
		this.bossHealth= Random.Range(30, 60);
		if (PlayerPrefs.GetInt("SoundEffects") == 0)
		{
			AudioSource audio = GetComponent<AudioSource>();
			audio.volume = 0;
		}
		GameManager.Instance.reset();
	}
	void Awake()
	{
		this.myanimator = GetComponent<Animator>();
		this.myrigid = GetComponent<Rigidbody2D>();
		this.myrigid.position = new Vector3(GenerateX(), GenerateY(), myrigid.transform.position.z);
		this.myanimator.SetBool("Destroy", false);
		this.Die = false;
	}
	// Update is called once per frame
	void Update()
	{

	}
	void FixedUpdate()
	{
		if (!this.Die && !this.myanimator.GetCurrentAnimatorStateInfo(0).IsName("destroy"))
		{
			if (this.myrigid.transform.position.y <= 4.66f)
			{
				this.myrigid.position = new Vector3(myrigid.transform.position.x, myrigid.transform.position.y + 0.01f, myrigid.transform.position.z);
			}
			else
			{
				this.myrigid.position = new Vector3(GenerateX(), GenerateY(), myrigid.transform.position.z);
				if (GameManager.Instance.Live > 0)
				{
					GameManager.Instance.Live--;
				}
				if (GameManager.Instance.Live < 1)
				{
					Application.LoadLevel("gameover");
					PlayerPrefs.SetInt("isnotlive", 1);
					if (GameManager.Instance.CollectedCoins > GameManager.Instance.HighScore)
					{
						GameManager.Instance.HighScore = GameManager.Instance.CollectedCoins;
					}
				}
			}
			this.sprite.color=new Color(1f, 1f, 1f,1f);
		}
		if (this.Die && this.myanimator.GetCurrentAnimatorStateInfo(0).IsName("destroy"))
		{
			this.myanimator.SetBool("Destroy", false);
			this.Die = false;
			GameManager.Instance.CollectedCoins+=30;
			GameManager.Instance.NumberBoss--;
			GameManager.Instance.CurrentLevel = 10;
			GameManager.Instance.MaxCoro=15;
			if (GameManager.Instance.CollectedCoins % 20 == 0)
			{
				if (GameManager.Instance.CurrentLevel < GameManager.Instance.MaxLevel)
				{
					GameManager.Instance.CurrentLevel++;
					GameManager.Instance.MaxCoro++;
				}
			}
			//Application.LoadLevel("main");
			Destroy(this.gameObject);
		}
	}
	private float GenerateX()
	{
		float x = Random.Range(-2.43f, 2.43f);
		return x;
	}
	private float GenerateY()
	{
		float y = Random.Range(-7.5f, -8f);
		return y;
	}
	void OnMouseDown()
	{
		//Debug.LogAssertion("clicked");
		if (this.bossHealth < 1 && PlayerPrefs.GetInt("Paused") == 0)
		{
			this.myanimator.SetBool("Destroy", true);
			this.myrigid.velocity = new Vector2(0f, 0f);
			this.Die = true;
		}else
		{
			this.bossHealth--;
			this.sprite.color=new Color(1f, 0f, 0f, 0.5f);
		}
	}

}
