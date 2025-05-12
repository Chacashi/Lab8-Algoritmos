using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapaNavegable : Graph<string, NodoMapa>
{
    public List<string> EncontrarRutaBFS(string inicioKey, string finKey)
    {
        if (!Nodes.ContainsKey(inicioKey) || !Nodes.ContainsKey(finKey)) return null;

        Queue<string> cola = new();
        Dictionary<string, string> visitados = new(); // Nodo actual => Nodo anterior

        cola.Enqueue(inicioKey);
        visitados[inicioKey] = null;

        while (cola.Count > 0)
        {
            string actual = cola.Dequeue();
            if (actual == finKey) break;

            foreach (var vecino in Nodes[actual].Neighbors)
            {
                string vecinoKey = Nodes.First(kvp => kvp.Value == vecino).Key;
                if (!visitados.ContainsKey(vecinoKey) && !vecino.Key.EsBloqueado)
                {
                    cola.Enqueue(vecinoKey);
                    visitados[vecinoKey] = actual;
                }
            }
        }

        if (!visitados.ContainsKey(finKey)) return null; // no hay camino

        // Reconstruir ruta
        List<string> ruta = new();
        string nodo = finKey;
        while (nodo != null)
        {
            ruta.Insert(0, nodo);
            nodo = visitados[nodo];
        }

        return ruta;
    }


    public List<string> ObtenerVecinosDisponibles(string nodoKey)
    {
        if (!Nodes.ContainsKey(nodoKey)) return new List<string>();

        return Nodes[nodoKey].Neighbors
            .Where(n => !n.Key.EsBloqueado)
            .Select(n => Nodes.First(kvp => kvp.Value == n).Key)
            .ToList();
    }


    public void DesbloquearNodo(string nodoKey)
    {
        if (Nodes.ContainsKey(nodoKey))
        {
            Nodes[nodoKey].Key.EsBloqueado = false;
        }
    }

}
    

