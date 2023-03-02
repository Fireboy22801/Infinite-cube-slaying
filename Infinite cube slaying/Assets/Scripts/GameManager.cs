using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startCubesCount;
    [SerializeField] private TMP_Text cubesDeadText;

    public static int CubesAlive;
    public static int CubesDead;

    private Spawner spawner;

    private void Start()
    {
        cubesDeadText.text = CubesDead.ToString();
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
        CubesDead++;
        cubesDeadText.text = CubesDead.ToString();
    }
}
