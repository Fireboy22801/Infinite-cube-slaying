using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float launchHeight = 10f;
    [SerializeField] private float flightTime = 2f;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private TMP_Text ballCooldownText;

    public int currentBallCount = 5;

    private float shootCooldown = 0.5f;
    private float ballReloadCooldown = 0f;
    private Camera mainCamera;
    private BallsInventory inventory;

    private void Start()
    {
        mainCamera = Camera.main;
        inventory = GetComponent<BallsInventory>();
    }

    private void Update()
    {
        inventory.UpdateBallsCount(currentBallCount);

        if (ballReloadCooldown <= 0f && currentBallCount <= 4f)
        {
            ballReloadCooldown = 1f;
            currentBallCount++;
        }
        else
        {
            ballReloadCooldown -= Time.deltaTime;
            ballReloadCooldown = Mathf.Clamp01(ballReloadCooldown);
            ballCooldownText.text = ballReloadCooldown.ToString("F2");
        }

        if (shootCooldown > 0 || currentBallCount <= 0)
        {
            shootCooldown -= Time.deltaTime;
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 launchPosition = mainCamera.transform.position + mainCamera.transform.forward + offSet;
            GameObject ball = Instantiate(ballPrefab, launchPosition, Quaternion.identity);

            StartCoroutine(MoveBall(ball));

            currentBallCount--;
            shootCooldown = 0.5f;
        }
    }

    private IEnumerator MoveBall(GameObject ball)
    {
        Vector3 startPos = ball.transform.position;
        Vector3 endPos = GetTargetPosition();

        float timeElapsed = 0f;
        while (timeElapsed < flightTime && ball != null)
        {
            float t = timeElapsed / flightTime;
            float height = launchHeight * Mathf.Sin(Mathf.PI * t);
            ball.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(ball);
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = Vector3.zero;

        if (Input.touchCount > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }
        }

        return targetPosition;
    }
}
