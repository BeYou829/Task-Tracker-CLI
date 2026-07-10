# Task Tracker CLI

Una aplicación en línea de comandos desarrollada con .NET 10 que permite al usuario manipular tareas y almacenarlos en formato JSON.

La aplicación permite agregar, editar, borrar, actualizar estado y mostrar por pantalla el listado de tareas por estados.

## Caracteristicas

- Agregar tareas
- Actualizar descripción de las tareas
- Actualizar el estado de las tareas
- Borrar tareas
- Mostrar lista de todas las tareas
- Filtar el listado de tareas por estado.
- Persistencia de datos en JSON

## Tecnologías

- C#
- .NET 10
- System.Text.Json

## Requerimientos

- .NET 10 SDK

## Instalación

```bash
git clone https://github.com/username/task-cli.git

cd task-cli

dotnet restore

dotnet build
```

## Ejecutarlo

```bash
dotnet run
```

## Cómo usarlo

| Comandos                   | Descripción        |
| -------------------------- | ------------------ | ------ | -------------------- |
| add "nombre_tarea"         | Create a task      |
| update "id" "nombre_tarea" | Update description |
| delete "id"                | Delete task        |
| mark-in-progress "id"      | Change status      |
| mark-done "id"             | Change status      |
| list                       | Show tasks         |
| list "todo"                | "in-progress"      | "done" | Show tasks by status |

## Ejemplos

### Add

```bash
dotnet run add "Buy groceries"
```

### Update

```bash
dotnet run update 1 "Buy milk"
```

### Delete

```bash
dotnet run delete 1
```

### List

```bash
dotnet run list
```

### List completed

```bash
dotnet run list done
```

## Output

ID | Description | Status | Created At

1 | Buy milk | done | 2026-07-10
2 | Study C# | todo | 2026-07-10

## Estructura

task-cli
│
├── Program.cs
├── TaskTodo.cs
├── Status.cs
├── tasks.json
└── README.md
