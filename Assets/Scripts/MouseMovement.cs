using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float MoveSpeed = 1;

    // Link it in unity
    [SerializeField] Sprite[] sprites;

    public SpriteRenderer spriteholder;
    private List<GameObject> CollisionList;
    private float mouseposX;

    private bool bonked;
    private Vector3 rand;
    [SerializeField] float TimeBetweenFrames;
    [SerializeField] float offset;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!bonked)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                mouseposX = rayHit.point.x;
            }
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseposX,  MoveSpeed * Time.deltaTime), offset, 0);

            if (Input.GetMouseButtonUp(0))
                StartCoroutine(animator());
        }


    }

    private IEnumerator animator()
    {
        bonked = true;
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteholder.sprite = sprites[i];

            if (i > 1 && i < 5) { spriteholder.sortingOrder = 4; }

            else { spriteholder.sortingOrder = 1; }                
            
            yield return new WaitForSeconds(TimeBetweenFrames);          
        }
        bonked = false;
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