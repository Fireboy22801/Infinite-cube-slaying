using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsUI : MonoBehaviour
{
    public void ShowBallsInStock(int ballsInStock)
    {
        Debug.Log("Show Balls");
        for (int i = 0; i < ballsInStock; i++)
        {
            Vector3 position = new Vector3(transform.position.x + i * 5f, transform.position.y, transform.position.z);
            Instantiate(gameObject, position, Quaternion.identity);
        }
    }
}
