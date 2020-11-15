using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Chaikin : MonoBehaviour
{

    private float minRadius = 1.5f;
    private float maxRadius = 3.5f;
    private int num = 25;
    private LineRenderer lr;
    private Vector3[] points;

    //params of LineRenderer
    public Color color = Color.green;
    public float width = 0.2f;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        GenerateRandomPath();
        SetLR(points);

        lr.useWorldSpace = true;
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
    }

    //generate random paths
    void GenerateRandomPath()
    {
        points = new Vector3[num];

        float angle = 360.0f / num;

        for (int i = 0; i < num; i++)
        {
            points[i] = Quaternion.AngleAxis((angle * i), Vector3.forward) * Vector3.up * Random.Range(minRadius, maxRadius);
        }
    }

    void SetLR(Vector3[] pts)
    {
        lr.positionCount = pts.Length;
        for (int i = 0; i < pts.Length; i++)
        {
            lr.SetPosition(i, pts[i]);
        }
    }

    //G to smooth and SpaceBar to change the randomly generated path
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            points = ChaikinVector(points);
            SetLR(points);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            GenerateRandomPath();
            SetLR(points);
        }
    }

    Vector3[] ChaikinVector(Vector3[] pts)
    {
        Vector3[] newPts = new Vector3[(pts.Length - 2) * 2 + 2];
        newPts[0] = pts[0];
        newPts[newPts.Length - 1] = pts[pts.Length - 1];

        int j = 1;
        for (int i = 0; i < pts.Length - 2; i++)
        {
            newPts[j] = pts[i] + (pts[i + 1] - pts[i]) * 0.75f;
            newPts[j + 1] = pts[i + 1] + (pts[i + 2] - pts[i + 1]) * 0.25f;
            j += 2;
        }
        return newPts;
    }
}
