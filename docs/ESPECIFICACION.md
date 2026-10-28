# Especificación del sistema: Gestor de Tareas (SDD)

## 1. Requerimientos
**Necesidad:** el equipo de desarrollo de la empresa necesita organizar sus tareas pendientes en una aplicación de escritorio simple.

| ID | Requerimiento funcional |
|----|-------------------------|
| RF-01 | Agregar una tarea con título y prioridad (Alta, Media, Baja). |
| RF-02 | Listar todas las tareas con su Id, título, prioridad y estado. |
| RF-03 | Marcar una tarea como completada. |
| RF-04 | Eliminar una tarea existente. |
| RF-05 | Mostrar la cantidad de tareas pendientes. |

## 2. Especificación (contratos, criterios y restricciones)
**Reglas de negocio**
- RN-01: el título es obligatorio (no vacío ni solo espacios).
- RN-02: el título no puede superar 100 caracteres.
- RN-03: toda tarea nueva inicia como "Pendiente".
- RN-04: cada tarea recibe un Id único y autoincremental.

**Criterios de aceptación**
- CA-01: al agregar una tarea válida, aparece en la lista como "Pendiente".
- CA-02: al agregar un título vacío, se muestra un mensaje de validación y no se agrega.
- CA-03: al completar una tarea, su estado cambia a "Completada".
- CA-04: al eliminar una tarea (con confirmación), desaparece de la lista.
- CA-05: eliminar o completar un Id inexistente lanza `KeyNotFoundException`.
- CA-06: el contador solo cuenta las tareas no completadas.

**Restricciones**
- Lenguaje C#, Windows Forms, Visual Studio 2026.
- Datos en memoria (sin base de datos).
- La lógica va separada de la interfaz (proyecto `GestorTareas.Core`).

## 3. Plan técnico
- **Arquitectura:** 3 proyectos: `GestorTareas.Core` (lógica), `GestorTareas` (WinForms), `GestorTareas.Tests` (xUnit).
- **Tareas de desarrollo:** una rama por función: `feature/agregar-tarea`, `feature/listar-tareas`, `feature/completar-tarea`, `feature/eliminar-tarea`.

## 4. Implementación
Código generado con ayuda de IA a partir de esta especificación.

## 5. Verificación
Pruebas unitarias xUnit (una por criterio de aceptación) ejecutadas automáticamente por GitHub Actions en cada Pull Request.

## 6. Evolución
Todo cambio se registra primero en esta especificación, luego se implementa en una rama y entra a `main` mediante Pull Request revisado.
