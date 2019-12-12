using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool vertEnemy;
    
    public float movingSpeed = 0.2f;

    //public float horizontalMovement;
    public float edgeRight = 10.83f;
    public float edgeLeft = -10.83f;

    //public float verticalMovement;
    public float edgeUp = 5.9f;
    public float edgeDown = -5.9f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vertEnemy)
        {
            transform.position += new Vector3(0f, movingSpeed, 0f);

            if (transform.position.y >= edgeUp || transform.position.y <= edgeDown)
            {
                movingSpeed *= -1;
            }
        }
        else
        {
            transform.position += new Vector3(movingSpeed, 0f, 0f);

            if (transform.position.x >= edgeRight || transform.position.x <= edgeLeft)
            {
                movingSpeed *= -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            }
        }
    }
}
