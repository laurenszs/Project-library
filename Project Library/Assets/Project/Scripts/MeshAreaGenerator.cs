using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshAreaGenerator : MonoBehaviour
{
    private Mesh _mesh;

    private Vector3[] _vertices;
    private int[] _triangles;

    public int xSize = 20;
    public int zSize = 20;


    public float Ystrength = 2f;
    public float xSeed = .3f, zSeed = .3f;

    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;


    }

    private void Update()
    {
        UpdateMesh();
        CreateArea();
    }

    void CreateArea()
    {
        _vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * xSeed, z * zSeed) * Ystrength;
                _vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        _triangles = new int[xSize * zSize * 6];
        var vert = 0;
        var tri = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                _triangles[tri + 0] = vert + 0;
                _triangles[tri + 1] = vert + xSize + 1;
                _triangles[tri + 2] = vert + 1;
                _triangles[tri + 3] = vert + 1;
                _triangles[tri + 4] = vert + xSize + 1;
                _triangles[tri + 5] = vert + xSize + 2;

                vert++;
                tri += 6;
            }

            vert++;
        }
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
    }

    private void OnDrawGizmos()
    {
        if (_vertices == null) return;

        foreach (var t in _vertices)
        {
            Gizmos.DrawSphere(t, .1f);
        }
    }
}