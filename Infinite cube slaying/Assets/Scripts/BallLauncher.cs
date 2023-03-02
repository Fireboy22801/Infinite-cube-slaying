using System;
using System.Collections;
using TMPro;
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
    private bool canShoot;

    private void Start()
    {
        mainCamera = Camera.main;
        inventory = GetComponent<BallsInventory>();
    }

    private void Update()
    {
        inventory.UpdateBallsCount(currentBallCount);
        shootCooldown -= Time.deltaTime;
        ballReloadCooldown -= Time.deltaTime;
        ballReloadCooldown = Math.Clamp(ballReloadCooldown, 0f, 0.99f);
        ballCooldownText.text = Math.Round(ballReloadCooldown, 2).ToString("F2");

        if (ballReloadCooldown <= 0f && currentBallCount <= 4f)
        {
            ballReloadCooldown = 1f;
            currentBallCount++;
        }

        if (shootCooldown <= 0 && currentBallCount > 0)
        {
            shootCooldown = 0.5f;
            canShoot = true;
        }

        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 launchPosition = mainCamera.transform.position + mainCamera.transform.forward + offSet;
                GameObject ball = Instantiate(ballPrefab, launchPosition, Quaternion.identity);

                StartCoroutine(MoveBall(ball));

                currentBallCount--;
                canShoot = false;
            }
        }
    }

    private IEnumerator MoveBall(GameObject ball)
    {
        Vector3 startPos = ball.transform.position;
        Vector3 endPos = GetTargetPosition();

        float timeElapsed = 0f;
        while (timeElapsed < flightTime && ball != null)
        {
            float t = Mathf.Clamp01(timeElapsed / flightTime);
            float height = launchHeight * Mathf.Sin(Mathf.PI * t);
            ball.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = Vector3.zero;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }

        return targetPosition;
    }
}
