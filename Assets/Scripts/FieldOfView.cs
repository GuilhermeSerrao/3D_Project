using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldOfView : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float fov = 90;

    [SerializeField]
    private Vector3 origin = Vector3.zero;

    [SerializeField]
    int rayCount = 2;

    [SerializeField]
    private float angle = 0;

    [SerializeField]
    private float viewDistance = 50;

    private Mesh mesh;

    private float angleIncrease;

    private float startAngle;

    void Start()
    {
        origin = transform.position;
        startAngle = angle;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


    }

    // Update is called once per frame
    void Update()
    {
        angleIncrease = fov / rayCount;
        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        angle = startAngle;

        for (int i = 0; i < rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit hit;

            var raycastHit = Physics.Raycast(origin, GetVectorFromAngle(angle), out hit, viewDistance, layerMask);

            //RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);

            if (hit.collider == null)
            {
                print("null");
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                print(hit.point);
                vertex = hit.point;
            }

            vertices[vertexIndex] = vertex;


            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
