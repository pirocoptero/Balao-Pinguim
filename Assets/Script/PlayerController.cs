using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    public GameObject[] lifeCanvas;

    public GameObject gameover;
    public Text scoreText;


    private int life;



    private int scoreNum;

    public GameObject btnReset;


    private string currentScene;

    ScreenShake screenShake;

    public float elapsedTime = 2f;


    public int Life
    {
        get
        {
            return this.life;
        }
        set
        {
            life = value;
        }
    }

    // Use this for initialization
    void Start()
    {

        screenShake = FindObjectOfType<ScreenShake>();

        //foreach(Transform child in transform) {
        //    if(child.name == "Balloon"){
        //        life++;
        //    }
        //}

        //Debug.Log(life);

        Life = 3;
        //UpdateLifeCanvas();

        currentScene = "First";

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    //responsável por mostrar os baloes no canvas referentes a quantidade de vida que o player possui
    public void UpdateLifeCanvas()
    {
        Life -= 1;
        lifeCanvas[Life].SetActive(false);
        if (Life == 0)
        {
            StartCoroutine(GameOver());
        }
        elapsedTime = 0;

    }

    IEnumerator GameOver()
    {

        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<MovePlayer>().bounceSpeed = 0;
        gameObject.GetComponent<MovePlayer>().speed = 0;
        gameover.SetActive(true);
        gameover.GetComponent<Animator>().SetBool("over", true);


        yield return new WaitForSeconds(2);

        gameObject.GetComponent<Rigidbody2D>().simulated = false;

        gameObject.SetActive(false);

        btnReset.SetActive(true);

        //SceneManager.LoadScene(currentScene);

    }

    public void ResetScene()
    {
        SceneManager.LoadScene(currentScene);
        btnReset.SetActive(false);
    }

    public void DestroyParent(GameObject parent)
    {
        Destroy(parent);
    }

    public void DisableParent(GameObject parent)
    {
        parent.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            StartCoroutine(screenShake.Shake(0.1f, 0.1f));
            scoreNum++;
            scoreText.text = scoreNum.ToString();
        }
    }
}
