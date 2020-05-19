using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SmoothingGroupExtension
{
    internal struct NormalSet
    {
        public Vector3 item1;
        public int item2;

        public NormalSet(Vector3 p1, int p2)
        {
            item1 = p1; item2 = p2;
        }
    };

    public static void MeshNormalAverage(ref Mesh mesh)
    {
        UnityEngine.Profiling.Profiler.BeginSample("Make averaged normal");
        var list = new List<NormalSet>();
        Vector3[] originalVertices = mesh.vertices;
        int vertexCount = mesh.vertexCount;

        for (int i = 0, cnt = 0; i < vertexCount; ++i, ++cnt)
        {
            list.Add(new NormalSet(originalVertices[i], cnt));
        }

        // Result storage.
        //var map = new Dictionary<Vector3, List<int>>();
        //// Add all the position of vertices into the dictionary.
        //for (int i = 0; i < mesh.vertexCount; ++i)
        //{
        //    if (false == map.ContainsKey(mesh.vertices[i]))
        //    {
        //        map.Add(mesh.vertices[i], new List<int>());
        //    }
        //    map[mesh.vertices[i]].Add(i);
        //}

        // Result normal array.
        Vector3[] normals = mesh.normals;
        Vector3 normal = default;

        int len = list.Count;
        for (int i = 0; i < len; ++i)
        {
            normal += mesh.normals[list[i].item2];
            normal /= list[i].item2;
            normals[i] = normal;
            normal = default;
        }
        mesh.normals = normals;
        UnityEngine.Profiling.Profiler.EndSample();

        //foreach (var e in map)
        //{
        //    normal = default;

        //    foreach (var n in e.Value)
        //    {
        //        normal += mesh.normals[n];
        //    }

        //    normal /= e.Value.Count;

        //    foreach (var n in e.Value)
        //    {
        //        normals[n] = normal;
        //    }
        //}
        //mesh.normals = normals;
    }

}
