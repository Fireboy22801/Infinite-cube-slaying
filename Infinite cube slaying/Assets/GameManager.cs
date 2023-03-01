using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startCubesCount;

    public static int CubesAlive;

    private Spawner spawner;

    private void Start()
    {
        spawner = GetComponent<Spawner>();
        SpawnCubes(startCubesCount);
    }

    public void SpawnCubes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(spawner.SpawnCube(false));
        }
    }

    public void CubeDied()
    {
        StartCoroutine(spawner.SpawnCube(true));
    }
}
