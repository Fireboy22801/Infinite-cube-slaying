using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float halfWidth;
    [SerializeField] private float halfHeight;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;

    private float currentMoveSpeed;
    private Vector3 destination;
    private float dashTimer;

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
        dashTimer = Random.Range(3f, 6f);
        transform.position = RandomPosition();
        destination = transform.position;
    }

    private void Update()
    {
        MoveCube();

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            dashTimer = Random.Range(3f, 6f);
            Dash();
        }
    }

    public Vector3 RandomPosition()
    {
        float x = Random.Range(-halfWidth, halfWidth);
        float z = Random.Range(-halfHeight, halfHeight);
        return new Vector3(x, transform.position.y, z);
    }

    private void MoveCube()
    {
        if (transform.position != destination)
        {
            Quaternion rotation = Quaternion.LookRotation(destination - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * currentMoveSpeed);
        }
        else
            destination = RandomPosition();
    }


    private void Dash()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        float initialSpeed = currentMoveSpeed;
        float timer = 0f;

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            currentMoveSpeed = Mathf.Lerp(initialSpeed, 0f, timer / 0.5f);
            yield return null;
        }

        timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            currentMoveSpeed = Mathf.Lerp(0f, dashSpeed, timer / 0.5f);
            yield return null;
        }

        currentMoveSpeed = moveSpeed;
    }
}

