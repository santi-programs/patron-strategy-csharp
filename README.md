# Trabajo Práctico: Patrón Strategy

**Alumno:** Santino Loray  
**Materia:** Trabajo de Diploma  
**Universidad:** Universidad Abierta Interamericana (UAI)  

---

## Descripción

Este repositorio contiene la implementación del **Patrón de Diseño Strategy** en C#, basado en el ejemplo del mundo real disponible en [DoFactory](https://www.dofactory.com/net/strategy-design-pattern).

## ¿Qué es el Patrón Strategy?

El Patrón Strategy es un patrón de diseño **comportamental** que define una familia de algoritmos, encapsula cada uno de ellos y los hace intercambiables. Strategy permite que el algoritmo varíe independientemente de los clientes que lo usan.

## Participantes del Patrón

| Participante | Clase | Rol |
|---|---|---|
| **Strategy** | `SortStrategy` | Clase abstracta que declara la interfaz común a todos los algoritmos |
| **ConcreteStrategy** | `QuickSort`, `ShellSort`, `MergeSort` | Implementan el algoritmo usando la interfaz de SortStrategy |
| **Context** | `SortedList` | Mantiene una referencia a un objeto SortStrategy y permite cambiarla dinámicamente |

## Diagrama de Clases

```
┌─────────────────────────────────────────────────────────────────┐
│                          <<abstract>>                           │
│                         SortStrategy                           │
│─────────────────────────────────────────────────────────────────│
│  + Sort(list: List<string>) : void  [abstract]                 │
└──────────────────────┬──────────────────────────────────────────┘
                       │
          ┌────────────┼────────────┐
          │            │            │
          ▼            ▼            ▼
   ┌────────────┐ ┌──────────┐ ┌───────────┐
   │ QuickSort  │ │ShellSort │ │ MergeSort │
   │────────────│ │──────────│ │───────────│
   │+Sort(list) │ │+Sort(list│ │+Sort(list)│
   └────────────┘ └──────────┘ └───────────┘

┌───────────────────────────────────┐
│           SortedList              │  ◄──── Context
│───────────────────────────────────│
│ - _list: List<string>             │
│ - _sortStrategy: SortStrategy     │──────► SortStrategy
│───────────────────────────────────│
│ + SetSortStrategy(s:SortStrategy) │
│ + Add(name: string)               │
│ + Sort()                          │
└───────────────────────────────────┘

┌───────────────────────────────────┐
│             MainApp               │
│───────────────────────────────────│
│ + Main()                          │──────► SortedList
└───────────────────────────────────┘
```

## Estructura del Proyecto

```
PatronStrategy/
├── PatronStrategy/
│   ├── Program.cs          ← Código fuente principal
│   ├── PatronStrategy.csproj
├── README.md
└── .gitignore
```

## Cómo Ejecutar

### Requisitos
- .NET 8 SDK o superior
- Visual Studio 2022 o VS Code

### Desde línea de comandos
```bash
cd PatronStrategy
dotnet run
```

### Desde Visual Studio
1. Abrir `PatronStrategy.csproj`
2. Presionar `F5` para ejecutar con depurador
3. Colocar breakpoints en las líneas deseadas (clic en el margen izquierdo)

## Salida Esperada del Programa

```
QuickSorted list
 Christopher
 Dave
 Jimmy
 Samual
 Sandra

ShellSorted list
 Christopher
 Dave
 Jimmy
 Samual
 Sandra

MergeSorted list
 Christopher
 Dave
 Jimmy
 Samual
 Sandra
```

## Análisis del Funcionamiento - Ejecución Paso a Paso

### Paso 1: Creación del Contexto
```csharp
SortedList studentRecords = new SortedList();
```
Se crea el objeto contexto `SortedList`. En este punto `_sortStrategy` es `null` y `_list` es una lista vacía.

### Paso 2: Carga de Datos
```csharp
studentRecords.Add("Samual");
studentRecords.Add("Jimmy");
// ... etc
```
La lista interna `_list` contiene: `["Samual", "Jimmy", "Sandra", "Dave", "Christopher"]`

### Paso 3: Selección de Estrategia - QuickSort
```csharp
studentRecords.SetSortStrategy(new QuickSort());
studentRecords.Sort();
```
- `_sortStrategy` ahora apunta a una instancia de `QuickSort`
- Al llamar `Sort()`, se delega a `QuickSort.Sort(list)` → usa `list.Sort()` interno de C#
- Resultado: lista ordenada alfabéticamente

### Paso 4: Cambio de Estrategia - ShellSort
```csharp
studentRecords.SetSortStrategy(new ShellSort());
studentRecords.Sort();
```
- Sin tocar los datos, simplemente se reemplaza la estrategia
- `_sortStrategy` ahora apunta a `ShellSort`
- Se aplica el algoritmo Shell Sort a la lista (ya ordenada)

### Paso 5: Cambio de Estrategia - MergeSort
```csharp
studentRecords.SetSortStrategy(new MergeSort());
studentRecords.Sort();
```
- `_sortStrategy` ahora apunta a `MergeSort`
- Se aplica Merge Sort recursivo

## Puntos de Interrupción Recomendados

| Línea | Qué inspeccionar |
|---|---|
| `studentRecords.SetSortStrategy(new QuickSort())` | Verificar que `_sortStrategy` cambia de tipo |
| `_sortStrategy.Sort(_list)` | Observar el dispatch polimórfico |
| Dentro de `QuickSort.Sort()` | Ver el estado de `list` antes y después del sort |
| El `foreach` final | Ver cada elemento mientras se itera |

## Preguntas para el Foro

1. ¿Por qué se utiliza una clase abstracta (`SortStrategy`) en lugar de una interfaz para definir el contrato del algoritmo?
2. ¿Qué ventaja tiene usar el patrón Strategy respecto a usar múltiples `if/else` o `switch` para seleccionar el algoritmo?
3. ¿El objeto `SortedList` (Context) conoce los detalles internos de cómo ordena cada estrategia? ¿Por qué esto es beneficioso?
4. ¿Podría agregarse una nueva estrategia de ordenamiento (ej: BubbleSort) sin modificar el código existente? ¿Qué principio SOLID aplica aquí?
5. ¿Qué diferencia existe entre el Patrón Strategy y el Patrón Template Method para reutilización de algoritmos?

## Fuentes

- [DoFactory - Strategy Design Pattern](https://www.dofactory.com/net/strategy-design-pattern)
- [Gang of Four - Design Patterns](https://en.wikipedia.org/wiki/Design_Patterns)
