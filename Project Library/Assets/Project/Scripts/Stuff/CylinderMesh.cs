using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMesh : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _verts;
    public int nVerts = 4;
    private int[] _tris;
    private Vector2[] _uv;

    public Vector3[] vertPoints;
    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        _verts = new Vector3[nVerts];

        for (int i = 0; i < _verts.Length; i++)
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


        _uv = new Vector2[nVerts];
        _uv[0] = new Vector2(0, 0);
        _uv[1] = new Vector2(0, 1);
        _uv[2] = new Vector2(1, 0);
        _uv[3] = new Vector2(1, 1);

        _mesh.vertices = _verts;
        _mesh.triangles = _tris;
        _mesh.uv = _uv;
    }
}