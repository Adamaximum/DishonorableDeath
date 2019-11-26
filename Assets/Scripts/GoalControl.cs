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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(current.buildIndex + 1);
        }
    }
}
