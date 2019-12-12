using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalControl : MonoBehaviour
{
    Scene current;
    string sceneName;

    public SpriteRenderer blackout;
    bool transition;

    public PlayerController control;

    // Start is called before the first frame update
    void Start()
    {
        current = SceneManager.GetActiveScene();
        sceneName = current.name;

        blackout = GameObject.Find("Blackout").GetComponent<SpriteRenderer>();

        control = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (transition)
        {
            blackout.color += new Color(0, 0, 0, 0.01f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            transition = true;
            control.playerRB.isKinematic = true;
            control.playerRB.velocity = new Vector2(0f, 0f);
            control.enabled = false;
            StartCoroutine(DelayTransition());
        }
    }

    IEnumerator DelayTransition()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(current.buildIndex + 1);
    }
}
