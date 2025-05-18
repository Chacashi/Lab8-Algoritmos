using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NodoMapaSO[] nodesMapaData;
    [SerializeField] private PatrolNPC[] arrayNPCs;
    private MapaNavegable mapa;

    private void Awake()
    {
        mapa = new MapaNavegable();
        foreach (var data in nodesMapaData)
            mapa.AddNode(data.id, new NodoMapa(data.id, data.cost, data.position));

        mapa.AddEdge("0", "1");
        mapa.AddEdge("1", "2");
        mapa.AddEdge("2", "3");
        mapa.AddEdge("3", "4");
        mapa.AddEdge("4", "5");
        mapa.AddEdge("5", "6");
        mapa.AddEdge("6", "7");
        mapa.AddEdge("7", "8");
        mapa.AddEdge("8", "9");
        mapa.AddEdge("9", "10");

        foreach (var npc in arrayNPCs)
        {
            npc.mapa = mapa;
        }
    }
}
