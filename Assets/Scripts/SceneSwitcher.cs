using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private float waitTime;
    private Scene scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
        waitTime = 4;
    }

    public void Update()
    {

        StartCoroutine(ChangeScene());
        scene = SceneManager.GetActiveScene();
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(waitTime);
        callScene();
        
    }

    public void callScene()
    {
        if(scene.name == "Loading1")
        {
            SceneManager.LoadScene("Level 02");
        }
        if(scene.name == "Loading2")
        {
            SceneManager.LoadScene("Level 01");
        }
    }


}
