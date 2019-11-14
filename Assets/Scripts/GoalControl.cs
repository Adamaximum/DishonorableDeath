using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalControl : MonoBehaviour
{
    Scene current;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        current = SceneManager.GetActiveScene();
        sceneName = current.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sceneName == "level0")
            {
                SceneManager.LoadScene("level0");
            }
            if (sceneName == "level1")
            {
                SceneManager.LoadScene("level1");
            }
            if (sceneName == "level2")
            {
                SceneManager.LoadScene("level2");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (sceneName == "level0")
            {
                SceneManager.LoadScene("level1");
            }
            if (sceneName == "level1")
            {
                SceneManager.LoadScene("level2");
            }
            if (sceneName == "level2")
            {
                SceneManager.LoadScene("level0");
            }
        }
    }
}
