using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public T Value { get; set; }
    public List<Node<T>> Neighbors { get; private set; }
    public object Key { get; set; }  

    public Node(T value)
    {
        Value = value;
        Neighbors = new List<Node<T>>();
    }

    public void AddNeighbor(Node<T> neighbor)
    {
        if (!Neighbors.Contains(neighbor))
        {
            Neighbors.Add(neighbor);
        }
    }

}
