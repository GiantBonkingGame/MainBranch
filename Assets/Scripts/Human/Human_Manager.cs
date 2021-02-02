using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Manager : MonoBehaviour
{
    [Tooltip("top left, top right, bottom left, bottom right")]
    [SerializeField] private Vector2[] gameView = new Vector2[4];
    [SerializeField] private Vector3 navMeshPos = new Vector3();
    public Vector3 NavMeshPos => navMeshPos;
    [SerializeField] private Vector2 navMeshSize = Vector2.one;
    [Space]
    [SerializeField] private Transform test = null; // <- prob gona be an array for all humans


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private Vector2 CalculatePos(Vector3 pos)
    {
        Vector3 localPos = pos - navMeshPos;

        float xPercentage = (localPos.x + navMeshSize.x / 2) / navMeshSize.x;
        float yPercentage = (localPos.z + navMeshSize.y / 2) / navMeshSize.y;


        Vector2 start1 = Vector2.Lerp(gameView[0], gameView[1], xPercentage);
        Vector2 end1 = Vector2.Lerp(gameView[2], gameView[3], xPercentage);
        Vector4 line1 = new Vector4(start1.x, start1.y, end1.x, end1.y);

        Vector2 start2 = Vector2.Lerp(gameView[2], gameView[0], yPercentage);
        Vector2 end2 = Vector2.Lerp(gameView[3], gameView[1], yPercentage);
        Vector4 line2 = new Vector4(start2.x, start2.y, end2.x, end2.y);

        if (LineIntersection(out Vector2 newPos, line1, line2))
            return newPos;

        Debug.LogWarning("lines dont intersect, wich is impossiple in a good setup");

        return new Vector2();
    }

    private bool LineIntersection(out Vector2 intersection, Vector4 line1, Vector4 line2)
    {
        // Line AB represented as a1x + b1y = c1  
        /*double a1 = B.y - A.y;
        double b1 = A.x - B.x;
        double c1 = a1 * (A.x) + b1 * (A.y);

        // Line CD represented as a2x + b2y = c2  
        double a2 = D.y - C.y;
        double b2 = C.x - D.x;
        double c2 = a2 * (C.x) + b2 * (C.y);

        double determinant = a1 * b2 - a2 * b1;

        if (determinant == 0)
        {
            // The lines are parallel. This is simplified  
            // by returning a pair of FLT_MAX  
            return new Point(double.MaxValue, double.MaxValue);
        }
        else
        {
            double x = (b2 * c1 - b1 * c2) / determinant;
            double y = (a1 * c2 - a2 * c1) / determinant;
            return new Point(x, y);
        }*/

        intersection = Vector2.zero;

        Vector2 xDiff = new Vector2(line1.x - line1.z, line2.x - line2.z);
        Vector2 yDiff = new Vector2(line1.y - line1.w, line2.y - line2.w);

        float det(Vector2 a, Vector2 b) // <- did not know this is possible
        {
            return a.x * b.y - a.y * b.x;
        }

        float div = det(xDiff, yDiff);
        if (div == 0)
            return false;

        Vector2 d = new Vector2(det(line1, new Vector2(line1.z, line1.w)), det(line2, new Vector2(line2.z, line2.w)));
        float x = det(d, xDiff) / div;
        float y = det(d, yDiff) / div;

        intersection = new Vector2(x, y);
        return true;

        //thanks: https://stackoverflow.com/questions/20677795/how-do-i-compute-the-intersection-point-of-two-lines
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine((Vector3)gameView[0] + transform.position, (Vector3)gameView[1] + transform.position);
        Gizmos.DrawLine((Vector3)gameView[2] + transform.position, (Vector3)gameView[3] + transform.position);

        Gizmos.DrawLine((Vector3)gameView[0] + transform.position, (Vector3)gameView[2] + transform.position);
        Gizmos.DrawLine((Vector3)gameView[1] + transform.position, (Vector3)gameView[3] + transform.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(navMeshPos, new Vector3(navMeshSize.x, 0f, navMeshSize.y));
    }
}
