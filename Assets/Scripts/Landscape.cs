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

    private static float VectorProduct(Vector2 from, Vector2 to1, Vector2 to2)
    {
        return (to2.x - from.x) * (to1.y - from.y) - (to1.x - from.x) * (to2.y - from.y);
    }

    public static List<List<Vector2>> GetTopParts(List<Vector2> polygon)
    {
        List<List<Vector2>> answer = new List<List<Vector2>>();

        int minIndx = -1;
        Vector2 minPoint = polygon[0];

        for (int i = 0; i < polygon.Count; ++i)
        {
            if (minIndx == -1 || minPoint.x < polygon[i].y)
            {
                minIndx = i;
                minPoint = polygon[i];
            }
        }
        Vector2 previous = minIndx == 0 ? polygon[polygon.Count - 1] : polygon[minIndx - 1];
        Vector2 next = minIndx == polygon.Count - 1 ? polygon[0] : polygon[minIndx + 1];

        float product = VectorProduct(previous, next, minPoint);
        bool IsClockwise;
        if (product == 0)
        {
            IsClockwise = previous.y < next.y;
        }
        else
        {
            IsClockwise = product < 0;
        }

        bool wasAdded = false;
        int j = minIndx;
        while (true)
        {
            int nextIndex = j == polygon.Count - 1 ? 0 : j + 1;

            if ((IsClockwise && (polygon[nextIndex] - polygon[j]).x > 0) || 
                (!IsClockwise && (polygon[nextIndex] - polygon[j]).x < 0))
            {
                if (!wasAdded)
                {
                    answer.Add(new List<Vector2>());
                    answer[answer.Count - 1].Add(polygon[j]);
                }
                answer[answer.Count - 1].Add(polygon[nextIndex]);
            }
            else
            {
                wasAdded = false;
            }

            j = nextIndex;
            if (j == minIndx)
            {
                break;
            }
        }

        return answer;
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
            Sprite sprite = new Sprite();
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
