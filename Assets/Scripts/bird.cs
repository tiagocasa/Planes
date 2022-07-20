using UnityEngine;

public class bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public CameraShake cameraShake;

    public GameObject ComoJogar;
    public GameObject Mao;
    public SpriteRenderer spriteHeli;
    public Sprite skinSprite;
    public float upForce = 200f;
    public GameObject Aura;
    public GameObject Wind;
    public GameObject Helice;
   

    // Start is called before the first frame update
    public void Start()
    {
        Time.timeScale = 0f;
        
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 1;
        rb2d.velocity = Vector2.zero;

        anim = GetComponent<Animator>();
        //Skin
 
       
        spriteHeli = GetComponent<SpriteRenderer>();
        skinSprite = MenuManager.instance.SkinSprite;
        spriteHeli.sprite = skinSprite;
        Debug.Log(spriteHeli.sprite);
        Helice.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Helicoptero");
    }
     public void ChangeSking()
    {
        PlayerPrefs.SetString("SkinSelec", "false");    
    }

    public void SkinRed()
    {
        PlayerPrefs.SetString("SkinSelec", "true");
    }
    // Update is called once per frame
    void Update()
    {

        //skinSprite = sm.GetSkin("Maluca");
        //Debug.Log(skinSprite);
        //spriteHeli.sprite = skinSprite;
        //Debug.Log(spriteHeli.sprite + "Update2");
        //if (!skinChange)
        //{
        //    skinSprite = sm.GetSkin("Maluca");
        //    Debug.Log(skinSprite);
        //    spriteHeli.sprite = skinSprite;
        //    skinChange = true;
        //    Debug.Log(spriteHeli.sprite + "Update2");
        //}
        if (isDead == false & !GameControl.instance.gamePaused && !GameControl.instance.isDash)
        {
            rb2d.gravityScale = 1;
            if (Input.GetMouseButtonDown(0))
            {
                if (!GameControl.instance.isDash)
                {
                    Time.timeScale = 1f;
                    ComoJogar.SetActive(false);
                    Mao.SetActive(false);
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, upForce));
                    anim.SetTrigger("isFlap");
                    Aura.SetActive(false);
                }
            }
        }

        if (GameControl.instance.isDash == true)
        {

            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            Aura.SetActive(true);
            
        }
        else if (!GameControl.instance.isDash)
        {
            Aura.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("Aura");
        }

            if (GameControl.instance.isIma == true)
        {
            Wind.SetActive(true);
            
        }
        else if (!GameControl.instance.isIma)
        {
            Wind.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("MagnetField");
        }

        if (GameControl.instance.gasoline <= 0 && !GameControl.instance.gameOver)
        {
            GameControl.instance.isDash = false;
            GameControl.instance.isIma = false;
            Wind.SetActive(false);
            Aura.SetActive(false);
            Helice.SetActive(false);
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            FindObjectOfType<AudioManager>().Play("Morte");
            anim.SetTrigger("isDead");
            rb2d.velocity = Vector2.zero;
            StartCoroutine(cameraShake.Shake(.2f, .2f));
            FindObjectOfType<AudioManager>().Stop("Musica");
            FindObjectOfType<AudioManager>().Stop("Helicoptero");
            FindObjectOfType<AudioManager>().Stop("MagnetField");
            FindObjectOfType<AudioManager>().Stop("Aura");
            isDead = true;

            GameControl.instance.BirdDied();
            

        }
    }
    
    private void LateUpdate()
    {
        if (!GameControl.instance.gameOver)
        {
            spriteHeli.sprite = skinSprite;
        }
        
    }
    public void OnCollisionEnter2D()
    {


        if (isDead == false)
        {
            if (!GameControl.instance.isDash)
            {
                rb2d.gravityScale = 0;
                rb2d.velocity = Vector2.zero;
                FindObjectOfType<AudioManager>().Play("Morte");
                anim.SetTrigger("isDead");
                Helice.SetActive(false);
                Wind.SetActive(false);
                Aura.SetActive(false);
                rb2d.velocity = Vector2.zero;
                StartCoroutine(cameraShake.Shake(.2f, .2f));
                FindObjectOfType<AudioManager>().Stop("Musica");
                FindObjectOfType<AudioManager>().Stop("Helicoptero");
                FindObjectOfType<AudioManager>().Stop("MagnetField");
                FindObjectOfType<AudioManager>().Stop("Aura");
                isDead = true;
                
                GameControl.instance.BirdDied();
                
            }   
            else
            {

                //anim.SetTrigger("Die");
                //Physics.gravity = new Vector3(0, 0, 0);
                
            }

        }

    }


}



