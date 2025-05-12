using System.Collections.Generic;
using UnityEngine;

public class MapaVizualizador : MonoBehaviour
{
    public GameObject NodePrefab;
    public GameObject LineRendererPrefab;
    public Transform NodeHolder;

    public MapaNavegable mapa; // Asignar en otro script o en Start()

    private Dictionary<string, GameObject> nodosVisuales = new();

    public void VisualizarMapa()
    {
        if (mapa == null || mapa.Nodes.Count == 0)
        {
            Debug.LogWarning("Mapa vacío o no asignado.");
            return;
        }

        int i = 0;
        float spacing = 5f;

        // 1. Instanciar nodos visuales
        foreach (var kvp in mapa.Nodes)
        {
            string key = kvp.Key;
            NodoMapa datos = kvp.Value.Key;

            Vector3 pos = new Vector3(Mathf.Cos(i) * spacing, 0, Mathf.Sin(i) * spacing);
            GameObject nodoGO = Instantiate(NodePrefab, pos, Quaternion.identity, NodeHolder);
            nodoGO.name = datos.Nombre;

            nodosVisuales[key] = nodoGO;
            i++;
        }

        // 2. Dibujar líneas entre vecinos
        HashSet<string> conexionesHechas = new(); // evita duplicados

        foreach (var kvp in mapa.Nodes)
        {
            string key1 = kvp.Key;
            var nodo1GO = nodosVisuales[key1];

            foreach (var vecino in kvp.Value.Neighbors)
            {
                string key2 = BuscarClaveDeNodo(mapa, vecino);
                if (key2 == null || conexionesHechas.Contains(key1 + "-" + key2) || conexionesHechas.Contains(key2 + "-" + key1))
                    continue;

                var nodo2GO = nodosVisuales[key2];

                GameObject linea = Instantiate(LineRendererPrefab, NodeHolder);
                var lr = linea.GetComponent<LineRenderer>();
                lr.SetPosition(0, nodo1GO.transform.position);
                lr.SetPosition(1, nodo2GO.transform.position);

                conexionesHechas.Add(key1 + "-" + key2);
            }
        }
    }

    private string BuscarClaveDeNodo(MapaNavegable mapa, Node<NodoMapa> nodo)
    {
        foreach (var kvp in mapa.Nodes)
        {
            if (kvp.Value == nodo) return kvp.Key;
        }
        return null;
    }

   

}
