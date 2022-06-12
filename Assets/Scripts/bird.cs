using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public CameraShake cameraShake;
    //public AudioManager morte;
    public GameObject ComoJogar;
    public GameObject Mao;
    public SpriteRenderer sprite;
    public float upForce = 200f;
    public GameObject Aura;
    public GameObject Wind;


    public AnimatorOverrideController Dragao;
    public AnimatorOverrideController Tartaruga;

    // Start is called before the first frame update
    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();


        //Mudar Skin
        sprite.color = new Color(PlayerPrefs.GetFloat("SkinSelecionadaRed", 255), PlayerPrefs.GetFloat("SkinSelecionadaGreen", 255), PlayerPrefs.GetFloat("SkinSelecionadaBlue", 255), 255);
        ChangeSkin();
        Time.timeScale = 0f;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 1;
        rb2d.velocity = Vector2.zero;
        

    }

    // Update is called once per frame
    void Update()
    {

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
                    anim.SetTrigger("Flap");
                    Aura.SetActive(false);
                }
            }
        }

        if (GameControl.instance.isDash == true)
        {

            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            Aura.SetActive(true);
        }else if (!GameControl.instance.isDash)
        {
            Aura.SetActive(false);
        }

            if (GameControl.instance.isIma == true)
        {
            Wind.SetActive(true);
        }else if (!GameControl.instance.isIma)
        {
            Wind.SetActive(false);
        }

        if (GameControl.instance.gasoline <= 0 && !GameControl.instance.gameOver)
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            FindObjectOfType<AudioManager>().Play("Morte");
            anim.SetTrigger("Die");
            ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
            particles.Stop();
            rb2d.velocity = Vector2.zero;
            StartCoroutine(cameraShake.Shake(.2f, .2f));
            FindObjectOfType<AudioManager>().Stop("Musica");
            FindObjectOfType<AudioManager>().Stop("Helicoptero");
            isDead = true;
            Wind.SetActive(false);
            GameControl.instance.BirdDied();
            
        }
    }

    public void OnCollisionEnter2D()
    {


        if (isDead == false)
        {
            if (GameControl.instance.isDash == false)
            {
                rb2d.gravityScale = 0;
                rb2d.velocity = Vector2.zero;
                FindObjectOfType<AudioManager>().Play("Morte");
                anim.SetTrigger("Die");
                ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
                particles.Stop();
                rb2d.velocity = Vector2.zero;
                StartCoroutine(cameraShake.Shake(.2f, .2f));
                FindObjectOfType<AudioManager>().Stop("Musica");
                FindObjectOfType<AudioManager>().Stop("Helicoptero");
                isDead = true;
                Wind.SetActive(false);
                GameControl.instance.BirdDied();
                
            }
            else
            {

                //anim.SetTrigger("Die");
                Physics.gravity = new Vector3(0, 0, 0);
                
            }

        }

    }


    public void ChangeSkin()
    {
        int SkinN = PlayerPrefs.GetInt("SkinSelecionada", 0);
        if(SkinN == 5)
        {
            GetComponent<Animator>().runtimeAnimatorController = Dragao as RuntimeAnimatorController;
            ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
            particles.Stop();
        }
        else if (SkinN == 6){
            GetComponent<Animator>().runtimeAnimatorController = Tartaruga as RuntimeAnimatorController;
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Helicoptero");
        }
        
    }

}



