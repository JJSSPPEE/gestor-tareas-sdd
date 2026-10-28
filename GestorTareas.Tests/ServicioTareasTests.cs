using GestorTareas.Core;

namespace GestorTareas.Tests;

public class ServicioTareasTests
{
    [Fact]
    public void Agregar_TituloValido_AgregaTareaPendiente()
    {
        var servicio = new ServicioTareas();

        var tarea = servicio.Agregar("Estudiar SDD", Prioridad.Alta);

        Assert.Single(servicio.Listar());
        Assert.Equal("Estudiar SDD", tarea.Titulo);
        Assert.False(tarea.Completada);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Agregar_TituloVacio_LanzaExcepcion(string titulo)
    {
        var servicio = new ServicioTareas();

        Assert.Throws<ArgumentException>(() => servicio.Agregar(titulo, Prioridad.Media));
    }

    [Fact]
    public void Agregar_TituloMuyLargo_LanzaExcepcion()
    {
        var servicio = new ServicioTareas();
        var titulo = new string('a', ServicioTareas.LongitudMaximaTitulo + 1);

        Assert.Throws<ArgumentException>(() => servicio.Agregar(titulo, Prioridad.Baja));
    }

    [Fact]
    public void Completar_TareaExistente_LaMarcaComoCompletada()
    {
        var servicio = new ServicioTareas();
        var tarea = servicio.Agregar("Hacer informe", Prioridad.Media);

        servicio.Completar(tarea.Id);

        Assert.True(servicio.Listar()[0].Completada);
    }

    [Fact]
    public void Eliminar_TareaExistente_LaQuitaDeLaLista()
    {
        var servicio = new ServicioTareas();
        var tarea = servicio.Agregar("Crear repo", Prioridad.Alta);

        servicio.Eliminar(tarea.Id);

        Assert.Empty(servicio.Listar());
    }

    [Fact]
    public void Eliminar_IdInexistente_LanzaExcepcion()
    {
        var servicio = new ServicioTareas();

        Assert.Throws<KeyNotFoundException>(() => servicio.Eliminar(99));
    }
    [Fact]
    public void ContarPendientes_CuentaSoloLasNoCompletadas()
    {
        var servicio = new ServicioTareas();
        var t1 = servicio.Agregar("Tarea 1", Prioridad.Alta);
        servicio.Agregar("Tarea 2", Prioridad.Media);
        servicio.Completar(t1.Id);

        Assert.Equal(1, servicio.ContarPendientes());
    }
}
