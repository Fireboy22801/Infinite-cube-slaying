using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;


    public IEnumerator SpawnCube(bool isRandom)
    {
        if (isRandom)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Instantiate(cubePrefab);
        }
        else 
            Instantiate(cubePrefab);
    }
}
