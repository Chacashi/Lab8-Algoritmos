using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapaNavegable : Graph<string, NodoMapa>
{
    public List<NodoMapa> FindPathAStar(string startId, string endId)
    {
        List<NodoMapa> path = new List<NodoMapa>();

        if (!Nodes.ContainsKey(startId) || !Nodes.ContainsKey(endId))
            return path;

        var start = Nodes[startId].Value;
        var goal = Nodes[endId].Value;

        var openSet = new PriorityQueue<NodoMapa>();
        openSet.Enqueue(start, 0);

        Dictionary<string, string> cameFrom = new Dictionary<string, string>();
        Dictionary<string, float> gScore = Nodes.Keys.ToDictionary(k => k, k => float.PositiveInfinity);
        Dictionary<string, float> fScore = Nodes.Keys.ToDictionary(k => k, k => float.PositiveInfinity);

        gScore[start.id] = 0;
        fScore[start.id] = Vector3.Distance(start.position, goal.position);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current.id == goal.id)
            {
                // Reconstruir el camino
                string currentId = goal.id;
                while (cameFrom.ContainsKey(currentId))
                {
                    path.Insert(0, Nodes[currentId].Value);
                    currentId = cameFrom[currentId];
                }
                path.Insert(0, start);
                return path;
            }

            foreach (var neighborNode in Nodes[current.id].Neighbors)
            {
                var neighbor = neighborNode.Value;
                float tentativeG = gScore[current.id] + Vector3.Distance(current.position, neighbor.position);

                if (tentativeG < gScore[neighbor.id])
                {
                    cameFrom[neighbor.id] = current.id;
                    gScore[neighbor.id] = tentativeG;
                    fScore[neighbor.id] = tentativeG + Vector3.Distance(neighbor.position, goal.position);

                    if (!openSet.Contains(neighbor))
                        openSet.Enqueue(neighbor, fScore[neighbor.id]);
                }
            }
        }

        // Si llegamos aquí, no hay camino
        return path;
    }

}
