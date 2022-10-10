using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour
{


    public AudioClip contact_sound;
    public AudioClip explode_sound;
    public AudioSource audio_source;
    public float force = 15;
    public Rigidbody2D rg;
    public GameObject loading;


    public GameObject explosion;


    PlayerController playerController;


    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();

    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rg.AddForce(Vector2.up * force);

    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Teleport")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        if (collision.gameObject.tag == "Balloon")
        {
            audio_source.Stop();
        }
        else
        {
            audio_source.PlayOneShot(contact_sound);
        }

        if (collision.gameObject.tag == "Explode" && playerController.elapsedTime > 2)
        {
            audio_source.PlayOneShot(explode_sound);
            Instantiate(explosion, transform.position, Quaternion.identity);

            playerController.UpdateLifeCanvas();
            Destroy(gameObject, 0.1f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && transform.tag == "Item")
        {
            StartCoroutine(explodeBalloon());
        }

        if (collision.gameObject.tag == "Player" && transform.tag == "FinalBalloon")
        {
            StartCoroutine(explodeBalloon());
            StartCoroutine(LoadScene());
        }

    }


    IEnumerator explodeBalloon()
    {
        audio_source.PlayOneShot(explode_sound);
        Instantiate(explosion, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(activeBalloonAgain());

    }

    IEnumerator activeBalloonAgain()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator LoadScene()
    {
        loading.SetActive(true);

        // loading.GetComponent<Animator>().Play("LoadingSquare");

        yield return new WaitForSecondsRealtime(1.4f);
        if (SceneManager.GetActiveScene().name == "Second")
            SceneManager.LoadScene("First");
        else
            SceneManager.LoadScene("Second");

    }


}
