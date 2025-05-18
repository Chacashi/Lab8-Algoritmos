using UnityEngine;

[CreateAssetMenu(fileName = "NodoMapa", menuName = "ScriptableObjects/NodoMapaData", order = 1)]
public class NodoMapaSO : ScriptableObject
{
    public string id;
    public int cost;
    public Vector3 position;

}
