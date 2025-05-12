using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapaVizualizador visualizador;

    private void Start()
    {
        MapaNavegable mapa = new MapaNavegable();
        mapa.AddNode("A", new NodoMapa("Inicio", "Punto de partida"));
        mapa.AddNode("B", new NodoMapa("Mitad", "Pasadiso", true));
        mapa.AddNode("C", new NodoMapa("Mitad 2", "Baño", true));
        mapa.AddNode("D", new NodoMapa("Mitad 3", "Pasadiso 2", true));
        mapa.AddNode("E", new NodoMapa("Final", "Sala de espera", true));
        mapa.AddEdge("A", "B");
        mapa.AddEdge("B", "C");
        mapa.AddEdge("C", "D");
        mapa.AddEdge("D", "E");

        visualizador.mapa = mapa;
        visualizador.VisualizarMapa();
    }
}
