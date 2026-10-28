namespace GestorTareas.Core;

public class ServicioTareas
{
    private readonly List<Tarea> _tareas = new();
    private int _siguienteId = 1;

    public const int LongitudMaximaTitulo = 100;

    public IReadOnlyList<Tarea> Listar() => _tareas.AsReadOnly();

    // RF-01: Agregar tarea
    public Tarea Agregar(string titulo, Prioridad prioridad)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título es obligatorio.");

        titulo = titulo.Trim();

        if (titulo.Length > LongitudMaximaTitulo)
            throw new ArgumentException($"El título no puede superar {LongitudMaximaTitulo} caracteres.");

        var tarea = new Tarea
        {
            Id = _siguienteId++,
            Titulo = titulo,
            Prioridad = prioridad,
            Completada = false
        };
        _tareas.Add(tarea);
        return tarea;
    }

    // RF-03: Completar tarea
    public void Completar(int id)
    {
        Buscar(id).Completada = true;
    }

    // RF-04: Eliminar tarea
    public void Eliminar(int id)
    {
        _tareas.Remove(Buscar(id));
    }

    private Tarea Buscar(int id)
    {
        return _tareas.FirstOrDefault(t => t.Id == id)
            ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}.");
    }
}
