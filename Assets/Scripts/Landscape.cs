using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Landscape : MonoBehaviour
{
    EdgeCollider2D pc2;
    void Start()
    {
        pc2 = gameObject.GetComponent<EdgeCollider2D>();
        //Render thing
        //Render thing
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying)
            return;
        if (pc2 != null)
        {
            //AllTheRenderThingFromTheStart ();
            int pointCount = 0;
            pointCount = pc2.pointCount; //.GetTotalPointCount();
            MeshFilter mf = GetComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            Vector2[] points = pc2.points;
            Vector3[] vertices = new Vector3[pointCount];
            Vector2[] uv = new Vector2[pointCount];
            for (int j = 0; j < pointCount; j++)
            {
                Vector2 actual = points[j];
                vertices[j] = new Vector3(actual.x, actual.y, 0);
                uv[j] = actual;
            }
            Triangulator tr = new Triangulator(points);
            int[] triangles = tr.Triangulate();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mf.mesh = mesh;
        }
    }
#endif
}
