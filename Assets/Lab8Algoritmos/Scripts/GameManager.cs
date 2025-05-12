using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject NodePrefab;
    public GameObject LineRendererPrefab;
    public Transform NodeHolder;

    public Graph<int, string> graph = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        graph.AddNode(1, "Juan");
        graph.AddNode(2, "Michael");
        graph.AddNode(3, "Ingrind");
        graph.AddNode(4, "Carlos");
        graph.AddNode(5, "Hoschild");

        graph.AddEdge(1, 4);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 4);
        graph.AddEdge(4, 5);

        graph.DisplayGraphAsList();
        graph.DisplayGraphAsMatrix();
        TestConnection();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TestConnection()
    {
        GameObject A = Instantiate(NodePrefab, NodeHolder);
        A.transform.position = Vector3.zero;
        GameObject B = Instantiate(NodePrefab, NodeHolder);
        B.transform.position = Vector3.up * 10;

        GameObject LineR = Instantiate(LineRendererPrefab, NodeHolder);
        LineR.GetComponent<LineRenderer>().SetPosition(0, A.transform.position);
        LineR.GetComponent<LineRenderer>().SetPosition(1, B.transform.position);
    }
}


