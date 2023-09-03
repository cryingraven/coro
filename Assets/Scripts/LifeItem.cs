using UnityEngine;
using System.Collections;

public class LifeItem : MonoBehaviour {
    private bool dropItem = false;
    private Rigidbody2D myrigid;
    private bool tapIn;
    private AudioSource audio;
    private Collider2D colider;
	private Animator animator;
    void Start()
    {
        this.myrigid = this.GetComponent<Rigidbody2D>();
        this.colider = this.GetComponent<Collider2D>();
		this.animator = this.GetComponent<Animator>();
        this.myrigid.transform.position = new Vector3(GenerateX(), GenerateY(), this.transform.position.z);
        this.dropItem = true;
        this.audio = this.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("SoundEffects") == 1)
        {
            this.audio.volume = 1;
        }else
        {
            this.audio.volume = 0;
        }
        this.tapIn = false;
    }
    public bool DropItem
    {
        get
        {
            return dropItem;
        }
        set
        {
            dropItem = value;
        }
    }
    void Update()
    {
        int fingerCount = Input.touchCount;

        for (int x = 0; x < fingerCount; x++)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(x).position);
            Vector2 touchPosGame = new Vector2(touchPos.x, touchPos.y);
            Collider2D hit = Physics2D.OverlapPoint(touchPosGame);
            if (hit.transform.gameObject == this.gameObject && Input.GetTouch(x).phase==TouchPhase.Began && PlayerPrefs.GetInt("Paused")==0)
            {
                this.dropItem = false;
				this.animator.SetBool("GetLife", true);
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                this.audio.Play();
                this.tapIn = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (this.myrigid.transform.position.y >= -4.66f && !this.tapIn)
        {
            if (dropItem)
            {
                if (this.transform.position.x > -2.7f && this.transform.position.x < 2.7f)
                {
                    this.myrigid.velocity = new Vector2(this.myrigid.velocity.x+ GenerateXVelocity(), this.myrigid.velocity.y - 0.01f);
                }else
                {
                    if(this.transform.position.x > 0)
                    {
                        this.myrigid.velocity = new Vector2(this.myrigid.velocity.x-0.01f, this.myrigid.velocity.y - 0.01f);

                    }
                    else
                    {
                        this.myrigid.velocity = new Vector2(this.myrigid.velocity.x+0.01f, this.myrigid.velocity.y - 0.01f);

                    }
                }
            }
        }else if (this.tapIn && !this.audio.isPlaying)
        {
            GameManager.Instance.Live += 1;
            Destroy(this.gameObject);
        }else if (this.tapIn && this.audio.isPlaying)
        {
           // playing audio
        }else
        {
            this.dropItem = false;
            Destroy(this.gameObject);
        }
    }
    private float GenerateX()
    {
        float x = Random.Range(-2.53f, 2.53f);
        return x;
    }
    private float GenerateXVelocity()
    {
        float x = Random.Range(-0.01f, 0.01f);
        return x;
    }
    private float GenerateY()
    {
        float y = Random.Range(5.62f, 6.3f);
        return y;
    }
    /*
    void OnMouseDown()
    {
        //Debug.LogAssertion("clicked");
        this.dropItem = false;
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        this.audio.Play();
        this.tapIn = true;
    }
    */
}
