using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MouseMovement : MonoBehaviour
{
    public float MoveSpeed = 1;

    // Link it in unity
    [SerializeField] Animation animation;

    private List<GameObject> CollisionList;
    private float mouseposX;
    private Vector3 rand;
    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
        {
            mouseposX = rayHit.point.x;
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseposX, MoveSpeed * Time.deltaTime), 0, 0);

        if (Input.GetMouseButtonUp(0))
            Bonk();
    }

    private void Bonk()
    {
        //start animation

        //start this code once animation is finished. (change the 0 value to hammer hit)
        if (animation.clip.length == 0f)
        {
            foreach  (GameObject collider in CollisionList)
            {
                collider.SetActive(false);
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        CollisionList.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        CollisionList.Remove(other.gameObject);
    }

}