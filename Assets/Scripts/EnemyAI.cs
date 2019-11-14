using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float timerNum = 5;
    float timerStartNum;

    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        timerStartNum = timerNum;

        firePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timerNum -= Time.deltaTime;

        if (timerNum <= 0)
        {
            Instantiate(bullet, new Vector3(firePoint.position.x, firePoint.position.y, 0f), Quaternion.identity);

            timerNum = timerStartNum;
        }
    }
}
