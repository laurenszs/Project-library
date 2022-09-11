using Unity.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class CreateFace : MonoBehaviour
{
    public int nVerts = 4;
    public Vector3[] vertPoints;
    [ReadOnly] [SerializeField] private Vector2[] uv;

    private Vector3[] _verts;
    private int[] _tris;

    private Mesh _mesh;


    // Start is called before the first frame update
    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        _verts = new Vector3[nVerts];

        for (int i = 0; i < nVerts; i++)
        {
            _verts[i] = vertPoints[i];
        }

        _tris = new int[6];
        _tris[0] = 0;
        _tris[1] = 1;
        _tris[2] = 2;

        _tris[3] = 3;
        _tris[4] = 2;
        _tris[5] = 1;

        // uv = new Vector2[nVerts];
        // var uvIndex = 0;
        // for (int i = 0; i < nVerts; i++)
        // {
        //     uv[uvIndex] = new Vector2(uvIndex, uvIndex);
        //     uv[uvIndex + 1] = new Vector2(uvIndex, uvIndex + 1);
        //     uv[uvIndex + 2] = new Vector2(uvIndex + 1, uvIndex);
        //     uv[uvIndex + 3] = new Vector2(uvIndex + 1, uvIndex + 1);
        // }


        _mesh.vertices = _verts;
        _mesh.triangles = _tris;
        _mesh.uv = new[]
        {
            Vector2.zero, Vector2.right, Vector2.up
        };
    }
}