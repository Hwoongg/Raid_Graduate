using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//
// 인접하는 지점과 함께 비용(cost)을 저장하는 클래스
//

[System.Serializable]
public class Edge : IComparable<Edge>
{

    public float cost;
    public Vertex vertex;

    public Edge(Vertex vertex = null, float cost = 1f)
    {
        this.vertex = vertex;
        this.cost = cost;
    }
     // 비교함수
    public int CompareTo(Edge other)
    {
        float result = cost - other.cost;
        int idA = vertex.GetInstanceID();
        int idB = other.vertex.GetInstanceID();
        if (idA == idB)
            return 0;

        return (int)result;
    }

    // 두 모서리를 비교하는 함수
    public bool Equals(Edge other)
    {
        return (other.vertex.id == this.vertex.id);
    }

    // 두 객체를 비교하는 함수
    public override bool Equals(object obj)
    {
        Edge other = (Edge)obj;
        return (other.vertex.id == this.vertex.id);
    }

    // 해시코드를 가져오는 함수
    public override int GetHashCode()
    {
        return this.vertex.GetHashCode();
    }
}
