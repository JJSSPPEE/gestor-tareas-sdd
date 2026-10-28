namespace GestorTareas.Core;

public enum Prioridad
{
    Alta,
    Media,
    Baja
}

public class Tarea
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public Prioridad Prioridad { get; set; }
    public bool Completada { get; set; }
}
