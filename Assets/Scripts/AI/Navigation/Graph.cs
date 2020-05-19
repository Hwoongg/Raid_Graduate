using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph : MonoBehaviour
{
    public GameObject vertexPrefab;
    protected List<Vertex> vertices;
    protected List<List<Vertex>> neighbours;
    protected List<List<float>> costs;


    void Start()
    {
        Load();
    }

    public virtual void Load()
    {

    }
    
    // 그래프의 크기를 구하기 위한 함수
    public virtual int GetSize()
    {
        if (ReferenceEquals(vertices, null))
            return 0;

        return vertices.Count;
    }

    // 주어진 점에서 가장 가까운 지점을 구하기 위한 함수
    public virtual Vertex GetNearestVertex(Vector3 position)
    {
        return null;
    }

    // id값을 통해 점을 구하는 함수
    public virtual Vertex GetVertexObj(int id)
    {
        if (ReferenceEquals(vertices, null) || vertices.Count == 0)
            return null;

        if (id < 0 || id >= vertices.Count)
            return null;

        return vertices[id];

    }

    // 한 지점을 통해 인접한 지점들을 구하는 함수
    public virtual Vertex[] GetNeighbours(Vertex v)
    {
        // == 연산자 null check는 신뢰성이 떨어지기 때문에 RefEquals()를 사용한다.
        if (ReferenceEquals(neighbours, null) || neighbours.Count == 0)
            return new Vertex[0];
        if (v.id < 0 || v.id >= neighbours.Count)
            return new Vertex[0];

        return neighbours[v.id].ToArray(); // List를 Array로 return 해주는 함수.
    }


}
