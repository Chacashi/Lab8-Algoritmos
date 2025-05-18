
using UnityEngine;

public class NodoMapa : Node<NodoMapa>
{
    public string id { get; set; }
    public int cost { get; set; }

    public Vector3 position { get; set; }
    public NodoMapa(string id, int cost, Vector3 position) : base(null)
    {
        this.id = id;
        this.cost = cost;
        this.Value = this;
        this.position = position;

    }
    
}



