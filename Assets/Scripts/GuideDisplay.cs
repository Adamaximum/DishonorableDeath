using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideDisplay : MonoBehaviour
{
    public TextMeshProUGUI helpText;
    public bool displayText;
    public float scaleSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        helpText = GameObject.Find("HelpText").GetComponent<TextMeshProUGUI>();
        helpText.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (displayText)
        {
            if (helpText.transform.localScale.x < 1)
            {
                helpText.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
            }
        }
        else
        {
            if (helpText.transform.localScale.x > 0)
            {
                helpText.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
            }
        }

        if (helpText.transform.localScale.x < 0)
        {
            helpText.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            displayText = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            displayText = false;
        }
    }
}
