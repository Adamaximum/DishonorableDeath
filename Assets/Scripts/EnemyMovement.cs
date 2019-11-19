using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float horizontalMovement;
    public float movingSpeed = 0.2f;

    public float edgeRight = 10.83f;
    public float edgeLeft = -10.83f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movingSpeed, 0f, 0f);

        if (transform.position.x >= edgeRight || transform.position.x <= edgeLeft)
        {
            movingSpeed *= -1;
        }
    }
}
