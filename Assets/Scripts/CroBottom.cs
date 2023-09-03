using UnityEngine;
using System.Collections;

public class CroBottom : MonoBehaviour {
    private Animator myanimator;
    private Rigidbody2D myrigid;
    [SerializeField]
    private GameObject lifeItem;
    [SerializeField]
    private GameObject croBoss;
    [SerializeField]
    private GameObject croReverse;
	[SerializeField]
	private GameObject croBossBottom;
    private bool Die = false;
    private float xDirectionAkhir;
    private bool goAhead;
    private AudioSource audio;
    private Collider2D colider;
    // Use this for initialization
    void Start()
    {
        this.myanimator = this.GetComponent<Animator>();
        this.myrigid = this.GetComponent<Rigidbody2D>();
        this.colider = this.GetComponent<Collider2D>();
        this.myrigid.position = new Vector3(GenerateX(), GenerateY(), myrigid.transform.position.z);
        this.audio = this.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("SoundEffects") == 1)
        {
            this.audio.volume = 1;
        }
        else
        {
            this.audio.volume = 0;
        }
        if (PlayerPrefs.GetInt("isnotlive") == 1)
        {
            PlayerPrefs.GetInt("isnotlive", 0);
            PlayerPrefs.GetInt("scoring", 0);
        }
        GameManager.Instance.reset();
        xDirectionAkhir = GenerateX();
        int randomGoAhead = Random.Range(1, 100);
        if (randomGoAhead > 33 && randomGoAhead < 66)
        {
            goAhead = true;
        }
        else
        {
            goAhead = false;
        }
    }
    void Awake()
    {
        this.myanimator = this.GetComponent<Animator>();
        this.myrigid = this.GetComponent<Rigidbody2D>();
        this.colider = this.GetComponent<Collider2D>();
        this.myrigid.position = new Vector3(GenerateX(), GenerateY(), myrigid.transform.position.z);
        this.myanimator.SetBool("Destroy", false);
        this.Die = false;
        xDirectionAkhir = GenerateX();
        int randomGoAhead = Random.Range(1, 100);
        if (randomGoAhead > 33 && randomGoAhead < 66)
        {
            goAhead = true;
        }
        else
        {
            goAhead = false;
        }
        this.transform.Rotate(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        int fingerCount = Input.touchCount;

        for (int x = 0; x < fingerCount; x++)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(x).position);
            Vector2 touchPosGame = new Vector2(touchPos.x, touchPos.y);
            Collider2D hit = Physics2D.OverlapPoint(touchPosGame);
            if (hit.transform.gameObject == this.gameObject && Input.GetTouch(x).phase == TouchPhase.Began && PlayerPrefs.GetInt("Paused") == 0)
            {
                if (!this.Die)
                {
                    this.myanimator.SetBool("Destroy", true);
                    this.audio.Play();
                    this.Die = true;
                    this.myrigid.velocity = new Vector2(0f, 0f);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (!this.Die && !this.myanimator.GetBool("Destroy"))
        {
            if (this.myrigid.transform.position.y <=4.80f)
            {
                if (!goAhead)
                {
                    if (this.transform.position.x > -2.7f && this.transform.position.x < 2.7f)
                    {
                        this.myrigid.velocity = new Vector3(xDirectionAkhir, myrigid.velocity.y + 0.01f);
                    }
                    else
                    {
                        this.transform.Rotate(0, 0, 0);
                        if (this.transform.position.x > 0)
                        {
                            xDirectionAkhir -= 0.5f;
                            this.myrigid.velocity = new Vector3(xDirectionAkhir, myrigid.velocity.y + 0.01f);
                            float angle = Mathf.Atan2(this.myrigid.velocity.y, this.myrigid.velocity.x) * Mathf.Rad2Deg;
                            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q, 30 * Time.deltaTime);

                        }
                        else
                        {
                            xDirectionAkhir += 0.5f;
                            this.myrigid.velocity = new Vector3(xDirectionAkhir, myrigid.velocity.y + 0.01f);
                            float angle = Mathf.Atan2(this.myrigid.velocity.y, this.myrigid.velocity.x) * Mathf.Rad2Deg;
                            Quaternion q = Quaternion.AngleAxis(angle, Vector3.back);
                            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q, 30 * Time.deltaTime);

                        }
                    }
                    //RotateSprite(LastX, xDirectionAkhir);
                    // LastX = xDirectionAkhir;
                }
                else
                {
                    this.myrigid.velocity = new Vector2(myrigid.velocity.x, myrigid.velocity.y + 0.01f);
                }
            }
            else
            {
                Vector3 theScale = new Vector3(0.7955534f, -0.6623454f, 1f);
                GameObject clone = Instantiate(this.gameObject);
                clone.name = "coro";
                clone.transform.localScale = theScale;
                clone.transform.Rotate(0, 0, 0);
                Destroy(this.gameObject);
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
        }
        if (this.Die && this.myanimator.GetCurrentAnimatorStateInfo(0).IsName("destroy") && !this.audio.isPlaying)
        {
            this.myanimator.SetBool("Destroy", false);
            this.Die = false;
            GameManager.Instance.NumberCoro--;
            int RandomItem = Random.Range(1, 100);
            if (RandomItem > 98)
            {
                GameObject life = Instantiate(lifeItem);
                life.name = "life item";
            }
            for (int i = 1; i <= GameManager.Instance.MaxCoro; i++)
            {
                if (GameManager.Instance.NumberCoro < GameManager.Instance.MaxCoro)
                {

                    int randomSpawn = Random.Range(1, 100);
                    Vector3 theScale = new Vector3(0.7955534f, -0.6623454f, 1f);
                    if (randomSpawn > 50)
                    {
                        GameObject clone = Instantiate(this.gameObject);
                        clone.name = "coro";
                        clone.transform.localScale = theScale;
                        clone.transform.Rotate(0, 0, 0);
                        GameManager.Instance.NumberCoro++;
                    }
                    else
                    {
                        GameObject clone = Instantiate(this.croReverse);
                        clone.name = "coro";
                        clone.transform.localScale = theScale;
                        clone.transform.Rotate(0, 0, 0);
                        GameManager.Instance.NumberCoro++;
                    }
                }
            }
            GameManager.Instance.CollectedCoins++;
			if (GameManager.Instance.CollectedCoins % 20 == 0)
            {
                if (GameManager.Instance.CurrentLevel < GameManager.Instance.MaxLevel)
                {
                    GameManager.Instance.CurrentLevel++;
                    GameManager.Instance.MaxCoro++;
                }
            }
            //test load level
            RandomItem = Random.Range(1, 100);
            if (GameManager.Instance.NumberBoss < 2)
            {
                if (RandomItem > 98)
                {
					RandomItem = Random.Range (1, 100);
					if (RandomItem > 50) {
						GameObject clone = Instantiate(this.croBoss);
						GameManager.Instance.NumberBoss++;
						clone.name = "coro_boss";
					} else {
						GameObject clone = Instantiate(this.croBossBottom);
						GameManager.Instance.NumberBoss++;
						clone.name = "coro_boss";
					}
					GameManager.Instance.CurrentLevel = 1;
					GameManager.Instance.MaxCoro=5;
                }
            }
            Destroy(this.gameObject);
        }
    }
    private float GenerateX()
    {
        float x = Random.Range(-2.53f, 2.53f);
        return x;
    }
    private float GenerateY()
    {
        float y = Random.Range(-5.62f, -6.3f);
        return y;
    }
    /*
    void OnMouseDown()
    {
        if (!this.Die)
        {
            this.myanimator.SetBool("Destroy", true);
            this.audio.Play();
            this.Die = true;
        }
    }
    */
}
