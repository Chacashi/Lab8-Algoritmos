using UnityEngine;

public class NodoMapa 
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool EsBloqueado { get; set; }

    public NodoMapa(string nombre, string descripcion, bool esBloqueado = false)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        EsBloqueado = esBloqueado;
    }

    public override string ToString()
    {
        return $"{Nombre} - {(EsBloqueado ? "Bloqueado" : "Accesible")} | {Descripcion}";
    }

}
