using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsInventory : MonoBehaviour
{
    [SerializeField] private List<Image> ballsList;

    public void UpdateBallsCount(int count)
    {
        for(int i = 0; i < count; i++)
        {
            ballsList[i].gameObject.SetActive(true);
        }
        for(int i = count; i < ballsList.Count; i++)
        {
            ballsList[i].gameObject.SetActive(false);
        }
    }
}
