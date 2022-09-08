using UnityEngine;

[ExecuteInEditMode]
public class CreateFace : MonoBehaviour
{
    public int nVerts = 4;
    public Vector3[] vertPoints;
    public Vector2[] uv;

    private Vector3[] _verts;
    private int[] _tris;

    private Mesh _mesh;


    // Start is called before the first frame update
    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;

        uv = new Vector2[nVerts];
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
   
        _mesh.vertices = _verts;
        _mesh.triangles = _tris;
        _mesh.uv = uv;
    }
}