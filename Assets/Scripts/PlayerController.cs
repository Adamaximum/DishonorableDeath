using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int playerState;
    // 0 = Alive
    // 1 = Heaven
    // 2 = Hell

    public float playerSpeed = 0.2f;

    float horizontalInput;
    float verticalInput;
    
    float edgeLeft = -11.63f;
    float edgeRight = 11.63f;
    float edgeUp = 6.13f;
    float edgeDown = -6.13f;

    [Header ("Jump Settings")]
    public float jumpVelocity = 2;
    public bool jumpCheck;
    public float fallMultiplier = 2.5f;

    public LayerMask groundMask;

    Rigidbody2D playerRB;

    SpriteRenderer playerSR;

    [Header("State Sprites")]
    public Sprite alive;
    public Sprite heaven;
    public Sprite hell;

    public bool hellContact;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        playerSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        MovementInput();
        GroundCheck();

        if(playerState == 0)
        {
            playerSR.sprite = alive;
            //playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 255);
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (playerState == 1)
        {
            playerSR.sprite = heaven;
            //playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 127);
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (playerState == 2)
        {
            playerSR.sprite = hell;
            //playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 127);
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (hellContact)
        {
            playerRB.velocity = new Vector2(0f, 0f);
        }
    }

    void MovementInput()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > edgeLeft)
            {
                horizontalInput = -playerSpeed;
            }
            else
            {
                horizontalInput = 0;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < edgeRight)
            {
                horizontalInput = playerSpeed;
            }
            else
            {
                horizontalInput = 0;
            }
        }
        else
        {
            horizontalInput = 0;
        }

        if (playerState == 2 && hellContact)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (transform.position.y > edgeDown)
                {
                    verticalInput = -playerSpeed;
                }
                else
                {
                    verticalInput = 0;
                }
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (transform.position.y < edgeUp)
                {
                    verticalInput = playerSpeed;
                }
                else
                {
                    verticalInput = 0;
                }
            }
            else
            {
                verticalInput = 0;
            }
        }
        else
        {
            verticalInput = 0;
        }

        transform.position += new Vector3(horizontalInput, verticalInput, 0f);

        if (playerState == 0) // Jumping is Normal
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCheck == true)
            {
                playerRB.velocity += Vector2.up * jumpVelocity;
            }
        }
        else if (playerState == 1) // Jumping is Infinite
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerRB.velocity = Vector2.up * jumpVelocity;
            }
        }
        if (playerState < 2) // Fall Multiplier is not active when Hell is active
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void GroundCheck()
    {
        float rayCastDist = 1;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, rayCastDist, groundMask);

        if (hit.collider != null)
        {
            jumpCheck = true;
        }
        else
        {
            jumpCheck = false;
        }

        Debug.DrawRay(transform.position, rayCastDist * Vector2.down, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision) // Trigger Enter
    {
        if (collision.gameObject.tag == "HeavenDeath" && playerState == 0)
        {
            playerState = 1;
            //playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 127);
        }
        if (collision.gameObject.tag == "HellDeath" && playerState == 0)
        {
            playerState = 2;
            playerRB.velocity = new Vector2(0f, 0f);
            //playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 127);
        }

        if (collision.gameObject.name == "Resurrector")
        {
            playerState = 0;
            //playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 255);
        }

        if (collision.gameObject.tag == "Hazard" && playerState == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.tag == "Angel" && playerState == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.tag == "Demon" && playerState == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.tag == "AllEnemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // Trigger Stay
    {
        if (collision.gameObject.tag == "Ground" && playerState == 2)
        {
            playerRB.gravityScale = 0;
            hellContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // Trigger Exit
    {
        if (collision.gameObject.tag == "Ground" && playerState == 2)
        {
            if (playerState == 2)
            {
                playerRB.gravityScale = 1;
            }
            hellContact = false;
        }
    }
}
