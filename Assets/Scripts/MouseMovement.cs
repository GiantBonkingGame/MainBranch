using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float smoothTime = 1;

    // Link it in unity
    [SerializeField] Sprite[] sprites;

    public SpriteRenderer spriteholder;
    private List<GameObject> CollisionList;
    private float mouseposX;


    private bool bonked;
    private Vector3 rand;
    [SerializeField] float TimeBetweenFrames;
    [SerializeField] float offset;
    [SerializeField] float hammerOffset;
    [SerializeField] Vector2 hammerSize;

    private Vector3 CurrentVelocity;
    [SerializeField] float ClampMin, ClampMax;

    // Gets the mouse hit info so we can get the x positition as a float, clamp it between 2 points
    // and make the transform use Smoothdamp to smoothly move towards the mouse X axis.
    private void Update()
    {
        if (!bonked)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                mouseposX = Mathf.Clamp(rayHit.point.x, ClampMin, ClampMax);
            }
            transform.position = Vector3.SmoothDamp(transform.position, Vector3.up * offset + Vector3.right * mouseposX, ref CurrentVelocity, smoothTime);
            //transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseposX, MoveSpeed * Time.deltaTime), offset, 0);
            
            // Starts the 'bonk' function using a coroutine.
            if (Input.GetMouseButtonUp(0))
            {
                
                StartCoroutine(animator());
            }
        }


    }


    // The IEnumerator is used to make the animation and 'slash' work together.
    private IEnumerator animator()
    {
        bonked = true;
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteholder.sprite = sprites[i];

            if (i > 1 && i < 5) { spriteholder.sortingOrder = 4; }

            else { spriteholder.sortingOrder = 1; }

            if (i == 2)
            {
                GameManager.instance.SmashAt(transform.position.x); //<- for the human AI

                Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position + Vector3.down * hammerOffset, hammerSize, 0f);
                foreach (Collider2D collider in cols)
                {
                    collider.gameObject.GetComponent<Human_AI>()?.Smashed();
                }

                if(cols.Length > 0)
                    AudioManager.instance.Smash(true);
                else AudioManager.instance.Smash(false);
            }

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * hammerOffset, hammerSize);
        Gizmos.DrawLine(Vector3.up * offset + Vector3.right * ClampMin, Vector3.up * offset + Vector3.right * ClampMax);
    }


}