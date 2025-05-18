using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Graph<TKey, TNodeValue>
{
    public Dictionary<TKey, Node<TNodeValue>> Nodes { get; private set; }

    public Graph()
    {
        Nodes = new Dictionary<TKey, Node<TNodeValue>>();
    }

    public bool AddNode(TKey key, TNodeValue value)
    {
        if (Nodes.ContainsKey(key))
            return false;

        Nodes[key] = new Node<TNodeValue>(value);
        return true;
    }

    public void AddEdge(TKey key1, TKey key2)
    {
        if (!Nodes.ContainsKey(key1) || !Nodes.ContainsKey(key2))
        {
            Debug.LogWarning("Uno o ambos nodos no existen en el grafo.");
            return;
        }

        Node<TNodeValue> n1 = Nodes[key1];
        Node<TNodeValue> n2 = Nodes[key2];

        n1.AddNeighbor(n2);
        n2.AddNeighbor(n1);
    }

    public void DisplayGraphAsMatrix()
    {
        int size = Nodes.Count;
        if (size == 0)
        {
            Debug.Log("El grafo está vacío.");
            return;
        }

        var keys = new List<TKey>(Nodes.Keys);
        var keyToIndex = new Dictionary<TKey, int>();
        var nodeToKey = new Dictionary<Node<TNodeValue>, TKey>();
        int[,] matrix = new int[size, size];

        for (int i = 0; i < keys.Count; i++)
        {
            keyToIndex[keys[i]] = i;
            nodeToKey[Nodes[keys[i]]] = keys[i];
        }

        foreach (var kvp in Nodes)
        {
            int i = keyToIndex[kvp.Key];
            foreach (var neighbor in kvp.Value.Neighbors)
            {
                if (nodeToKey.TryGetValue(neighbor, out TKey neighborKey))
                {
                    int j = keyToIndex[neighborKey];
                    matrix[i, j] = 1;
                    matrix[j, i] = 1; // Simétrico
                }
            }
        }

        PrintAdjacencyMatrix(keys, matrix);
    }

    private void PrintAdjacencyMatrix(List<TKey> keys, int[,] matrix)
    {
        string header = "".PadRight(6);
        foreach (var key in keys)
            header += $"{key.ToString().PadRight(6)}";

        Debug.Log(header);

        for (int i = 0; i < keys.Count; i++)
        {
            string row = $"{keys[i].ToString().PadRight(6)}";
            for (int j = 0; j < keys.Count; j++)
            {
                row += (matrix[i, j] == 1 ? "Si" : "No").PadRight(6);
            }
            Debug.Log(row);
        }
    }

    public void DisplayGraphAsList()
    {
        var nodeToKey = new Dictionary<Node<TNodeValue>, TKey>();
        foreach (var kvp in Nodes)
        {
            nodeToKey[kvp.Value] = kvp.Key;
        }

        foreach (var kvp in Nodes)
        {
            string line = $"Nodo {kvp.Key}: ";
            foreach (var neighbor in kvp.Value.Neighbors)
            {
                if (nodeToKey.TryGetValue(neighbor, out TKey neighborKey))
                {
                    line += $"{neighborKey}, ";
                }
            }
            Debug.Log(line.TrimEnd(' ', ','));
        }
    }
    public List<TKey> BFS(TKey startKey)
    {
        var visitados = new HashSet<Node<TNodeValue>>();
        var resultado = new List<TKey>();

        if (!Nodes.ContainsKey(startKey))
            return resultado;

        var cola = new Queue<Node<TNodeValue>>();
        var startNode = Nodes[startKey];
        cola.Enqueue(startNode);
        visitados.Add(startNode);

        var nodeToKey = Nodes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        while (cola.Count > 0)
        {
            var actual = cola.Dequeue();
            resultado.Add(nodeToKey[actual]);

            foreach (var vecino in actual.Neighbors)
            {
                if (!visitados.Contains(vecino))
                {
                    visitados.Add(vecino);
                    cola.Enqueue(vecino);
                }
            }
        }

        return resultado;
    }
    public List<TKey> DFS(TKey startKey)
    {
        var visitados = new HashSet<Node<TNodeValue>>();
        var resultado = new List<TKey>();

        if (!Nodes.ContainsKey(startKey))
            return resultado;

        var nodeToKey = Nodes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        void DFSRecursivo(Node<TNodeValue> actual)
        {
            visitados.Add(actual);
            resultado.Add(nodeToKey[actual]);

            foreach (var vecino in actual.Neighbors)
            {
                if (!visitados.Contains(vecino))
                {
                    DFSRecursivo(vecino);
                }
            }
        }

        DFSRecursivo(Nodes[startKey]);
        return resultado;
    }


    public List<TKey> FindPathBFS(TKey startKey, TKey targetKey)
    {
        var path = new List<TKey>();

        if (!Nodes.ContainsKey(startKey) || !Nodes.ContainsKey(targetKey))
            return path;

        var startNode = Nodes[startKey];
        var targetNode = Nodes[targetKey];

        var visited = new HashSet<Node<TNodeValue>>();
        var parentMap = new Dictionary<Node<TNodeValue>, Node<TNodeValue>>();
        var queue = new Queue<Node<TNodeValue>>();

        queue.Enqueue(startNode);
        visited.Add(startNode);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == targetNode)
                break;

            foreach (var neighbor in current.Neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parentMap[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Si no se encontró el target
        if (!parentMap.ContainsKey(targetNode) && startNode != targetNode)
            return path;

        // Reconstruir el camino desde el target hacia el start
        var currentNode = targetNode;
        var nodeToKey = Nodes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        while (currentNode != null)
        {
            path.Insert(0, nodeToKey[currentNode]);
            parentMap.TryGetValue(currentNode, out currentNode);
        }

        return path;
    }
}
     