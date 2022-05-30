using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidWaveScript : MonoBehaviour
{
    public float power = 3;
    public float scale = 1;
    public float timeScale = 1;
    private float xOffset;
    private float yOffset;
    public float size = 1;
    public int gridSize = 16;
    private MeshFilter filter;

    void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
    }


    // Update is called once per frame
    void Update()
    {
        MakeNoise();
        xOffset += Time.deltaTime * timeScale;
        yOffset += Time.deltaTime * timeScale;
    }

    private Mesh GenerateMesh()
    {
        Mesh mesh = filter.mesh;

        List<Vector3> verticies = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < gridSize + 1; i++)
        {
            for (int j = 0; j < gridSize + 1; j++)
            {
                verticies.Add(new Vector3(-size * 0.5f + size * (i / ((float)gridSize)), 0, -size * 0.5f + size * (j / ((float)gridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(i / (float)gridSize, j / (float)gridSize));
            }
        }

        List<int> triangles = new List<int>();
        int vertCount = gridSize + 1;

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

    float CalculateHeight(float x, float y)
    {
        float xCord = x * scale + xOffset;
        float yCord = y * scale + yOffset;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
