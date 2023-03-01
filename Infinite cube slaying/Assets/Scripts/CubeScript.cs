using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private void Start()
    {
        GameManager.CubesAlive++;
        CubeMovement cubeMovement = gameObject.GetComponent<CubeMovement>();
        gameObject.transform.position = cubeMovement.RandomPosition();
    }

    private void OnDestroy()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        GameManager.CubesAlive--;

        if (gameManager != null)
        {
            gameManager.CubeDied();
        }
    }
}
