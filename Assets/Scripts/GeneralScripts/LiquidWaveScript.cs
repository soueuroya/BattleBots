using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidWaveScript : MonoBehaviour
{
    public float power = 0.3f;
    public Vector2 scale = Vector2.right * 0.2f + Vector2.up * 0.2f;
    public Vector2 timeScale = Vector2.right * 0.5f + Vector2.up * 0.5f;
    private float xOffset;
    private float yOffset;
    public Vector2 size = Vector2.right * 10f + Vector2.up * 10f;
    public int gridSizeX = 128;
    public int gridSizeY = 64;
    private MeshFilter filter;
    private List<Rigidbody> floatingBodies;
    private List<Rigidbody> unfloatingBodies;
    public float maxDrag = 6;
    public float dragOnExit = 4;
    public float dragOnEnter = 1;
    public float dragOnStay = 0;
    public float forceUp = 8;
    public bool UPDATE;

    private void Start()
    {
        UpdateMesh();
    }

    void UpdateMesh()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
        floatingBodies = new List<Rigidbody>();
        unfloatingBodies = new List<Rigidbody>();
    }

    private void OnValidate()
    {
        if (UPDATE)
        {
            UPDATE = false;
            UpdateMesh();
        }
    }

    void Update()
    {
        MakeNoise();
        xOffset += Time.deltaTime * timeScale.x;
        yOffset += Time.deltaTime * timeScale.y;

        foreach (var body in unfloatingBodies)
        {
            body.drag = dragOnExit;
            floatingBodies.Remove(body);
        }
        unfloatingBodies.Clear();
        foreach (var body in floatingBodies)
        {
            if (body.drag < maxDrag)
            {
                body.drag += dragOnStay;
            }
            else
            {
                body.drag = maxDrag;
            }
            body.AddForce(Vector3.up * forceUp, ForceMode.Force);
        }
    }

    private Mesh GenerateMesh()
    {
        Mesh mesh = filter.mesh;
        List<Vector3> verticies = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < gridSizeX + 1; i++)
        {
            for (int j = 0; j < gridSizeY + 1; j++)
            {
                verticies.Add(new Vector3(-size.y * 0.5f + size.y * (i / ((float)gridSizeX)), 0, -size.x * 0.5f + size.x * (j / ((float)gridSizeY))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(i / (float)gridSizeX, j / (float)gridSizeY));
            }
        }

        List<int> triangles = new List<int>();
        int vertCount = (gridSizeX < gridSizeY ? gridSizeX : gridSizeY) + 1;

        for (int k = 0; k < vertCount * vertCount - vertCount; k++)
        {
            if ((k + 1) % vertCount == 0)
            {
                continue;
            }
            triangles.AddRange(new List<int>()
            {
                k+1+vertCount, k+vertCount, k, k, k+1, k+vertCount+1
            });
        }

        mesh.SetVertices(verticies);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }

    void MakeNoise()
    {
        Vector3[] verts = filter.mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i].y = CalculateHeight(verts[i].x, verts[i].z) * power;
        }

        filter.mesh.vertices = verts;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PieceScript>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = dragOnEnter;
                floatingBodies.Add(rb);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PieceScript>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                unfloatingBodies.Add(rb);
            }
        }
    }

    float CalculateHeight(float x, float y)
    {
        float xCord = x * scale.x + xOffset;
        float yCord = y * scale.y + yOffset;
        return Mathf.PerlinNoise(xCord, yCord);
    }
}
