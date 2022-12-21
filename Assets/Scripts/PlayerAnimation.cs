using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private Scene scene;


    [SerializeField] private bool changeState;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        OnScene();
        changeState = true;
    }

    // Update is called once per frame
    void Update()
    {
        OnSwitch();
        OnMove();
        OnRun();
        OnDeath();
        OnScene();

    }

    void OnScene()
    {
        if (scene.name == "Level 01")
        {

            anim.SetInteger("level", 0);
        }
        else
        {

            anim.SetInteger("level", 1);
        }
    }

    void OnSwitch()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            addItemGameValues(1, 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
           addItemGameValues(2, 1);
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }

    void addItemGameValues(int currentScene, int nextScene)
    {

        GameValues.scene = nextScene;

        if (currentScene == 1)
        {
            GameValues.playerPositionScreen1 = transform.position;

        }

        if (currentScene == 2)
        {
            GameValues.playerPositionScreen2 = transform.position;
        }
            


    }

    void OnMove()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            changeState = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            changeState = true;
        }

        if (player.direction.x > 0 && changeState)
        {
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (player.direction.x < 0 && changeState)
        {
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            anim.SetInteger("transition", 0);

        }
    }

    void OnRun()
    {
        if (player.isRunning)
        {
            anim.SetInteger("transition", 2);
        }
    }
    void OnDeath()
    {
        if (player.isDead)
        {
            anim.SetInteger("transition", 3);
            StartCoroutine(OnRespawn());

        }


    }
    IEnumerator OnRespawn()
    {
        yield return new WaitForSeconds(1);
        transform.position = player.respawnPoint;
    }
}
