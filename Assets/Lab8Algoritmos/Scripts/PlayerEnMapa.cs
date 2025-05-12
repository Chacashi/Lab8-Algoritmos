using UnityEngine;
using System.Linq;

public class PlayerEnMapa
{
    public MapaNavegable Mapa { get; private set; }
    public string NodoActualKey { get; private set; }

    public PlayerEnMapa(MapaNavegable mapa, string nodoInicialKey)
    {
        Mapa = mapa;
        NodoActualKey = nodoInicialKey;
    }

    public void MoverANodo(string nuevoNodoKey)
    {
        if (!Mapa.Nodes.ContainsKey(nuevoNodoKey) || !Mapa.Nodes.ContainsKey(NodoActualKey))
        {
            Debug.Log("Nodo no válido.");
            return;
        }

        var nodoActual = Mapa.Nodes[NodoActualKey];
        var nuevoNodo = Mapa.Nodes[nuevoNodoKey];

        if (nuevoNodo.Key.EsBloqueado)
        {
            Debug.Log($"{nuevoNodo.Key.Nombre} está bloqueado.");
            return;
        }

        if (nodoActual.Neighbors.Contains(nuevoNodo))
        {
            NodoActualKey = nuevoNodoKey;
            Debug.Log($"Te moviste a {nuevoNodo.Key.Nombre}.");
        }
        else
        {
            Debug.Log($"No hay conexión directa desde {nodoActual.Key.Nombre} hasta {nuevoNodo.Key.Nombre}.");
        }
    }

    public NodoMapa ObtenerNodoActual()
    {
        return Mapa.Nodes[NodoActualKey].Key;
    }
}
