using System.Collections.Generic;
using UnityEngine;

public class PatrolNPC : MonoBehaviour
{
    public MapaNavegable mapa { get; set; }
    private int keyIndex = 0;
    private string currentNodeID;
    private int direction = 1;

    [SerializeField] private NodoMapaSO[] Keys;
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float avoidDistance = 1.5f;

    private Queue<NodoMapa> currentPath = new Queue<NodoMapa>();

    private void Start()
    {
        currentNodeID = Keys[0].id;
    }

    private void Update()
    {
        if (mapa == null || !mapa.Nodes.ContainsKey(currentNodeID)) return;

        var nextNode = mapa.Nodes[currentNodeID].Value;

        if (Vector3.Distance(player.position, nextNode.position) < avoidDistance)
        {
            RecalcularCaminoEvadiendoJugador();
            return;
        }

        if (currentPath.Count > 0)
        {
            NodoMapa siguiente = currentPath.Peek();
            transform.position = Vector3.MoveTowards(transform.position, siguiente.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, siguiente.position) < 0.1f)
            {
                currentPath.Dequeue();
                if (currentPath.Count == 0)
                    CambiarNodo();
            }

            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextNode.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextNode.position) < 0.1f)
        {
            CambiarNodo();
        }
    }

    private void CambiarNodo()
    {
        keyIndex += direction;

        if (keyIndex >= Keys.Length)
        {
            keyIndex = Keys.Length - 2;
            direction = -1;
        }
        else if (keyIndex < 0)
        {
            keyIndex = 1;
            direction = 1;
        }

        currentNodeID = Keys[keyIndex].id;
    }

    private void RecalcularCaminoEvadiendoJugador()
    {
        string inicio = currentNodeID;
        string destino = Keys[(keyIndex + direction >= Keys.Length || keyIndex + direction < 0)
            ? keyIndex : keyIndex + direction].id;

        var path = mapa.FindPathAStar(inicio, destino);

        if (path != null && path.Count > 0)
        {
            currentPath.Clear();
            foreach (var node in path)
            {
                if (node.id != inicio)
                    currentPath.Enqueue(node);
            }
        }
    }
}
