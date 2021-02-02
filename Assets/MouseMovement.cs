using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float MoveSpeed = 1, inputSpeed = 1;

    private Vector2 lookInput, screenCenter, mouseDistance;
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;   
    }

    private void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        transform.position +=  Vector3.right * MoveSpeed * Time.deltaTime;

        MoveSpeed = Mathf.Lerp(MoveSpeed, Input.GetAxisRaw("Horizontal") * inputSpeed, Time.deltaTime);


        if (Input.GetMouseButtonUp(0))
        {
            Bonk();
        }
    }

    private void Bonk()
    {
        Instantiate(prefab, lookInput, Quaternion.identity);
    }
}