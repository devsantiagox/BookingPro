# Sistema de Gestión de Reservas 🏢📅

## Introducción 🚀
El objetivo del proyecto es desarrollar un Sistema de Gestión de Reservas utilizando **.NET Framework 4.8**, **Dapper** como ORM para interactuar con una base de datos **SQL Server**, y una interfaz web funcional que integre **CSHTML (Razor)**, **JavaScript**, y **CSS**. Este sistema permitirá a los usuarios gestionar salas y reservas de manera eficiente.

## Requerimientos Técnicos 🛠️
### Backend
- Lenguaje: C#
- Framework: .NET Framework 4.8
- Conexión a Base de Datos: Utilizar Dapper para interactuar con SQL Server
- Patrón de Diseño: Implementar el Patrón Repositorio
- Procedimientos Almacenados: CRUD y funcionalidades específicas

### Base de Datos
- Tablas:
  - Salas: Gestión de salas disponibles
  - Reservas: Gestión de reservas de los usuarios
- Procedimientos Almacenados Requeridos:
  - CRUD completo para Salas y Reservas
  - Validación de disponibilidad de una sala antes de reservar
  - Consulta de reservas filtradas por fecha y/o sala

### Frontend
- Vistas:
  - Gestión de Salas: CRUD de salas
  - Gestión de Reservas: CRUD de reservas con validaciones
- Estilización:
  - CSS para un diseño limpio y ordenado
  - Uso opcional de Bootstrap u otras librerías
- Interactividad:
  - JavaScript para validaciones de formularios
  - AJAX, Fetch, o Axios para actualizaciones en tiempo real

## Descripción del Sistema 📝
### Gestión de Salas
- Los usuarios pueden agregar, editar, eliminar y consultar salas
- Atributos de las salas:
  - ID: Autogenerado
  - Nombre: Nombre de la sala
  - Capacidad: Número de personas
  - Disponibilidad: Activa o inactiva

### Gestión de Reservas
- Los usuarios pueden reservar una sala para una fecha específica
- Validaciones para evitar reservas duplicadas
- Atributos de las reservas:
  - ID: Autogenerado
  - Sala: Relación con la tabla de Salas
  - Fecha de la Reserva: Fecha seleccionada
  - Usuario: Nombre del usuario

### Consulta de Reservas
- Filtrar reservas por rango de fechas o sala específica

## Entregables 📦
1. **Código Fuente**:
   - Proyecto completo en .NET Framework 4.8
   - Código organizado en capas: Controladores, Repositorios, Modelos y Vistas
   - Uso de Dapper para la interacción con la base de datos
2. **Base de Datos**:
   - Script SQL para la creación de tablas y procedimientos almacenados
3. **Frontend**:
   - Vistas funcionales en CSHTML con interactividad mediante JavaScript
   - Estilización básica en CSS
4. **Documentación**:
   - Archivo README.md con instrucciones claras

## Instrucciones para Ejecutar el Proyecto ▶️
1. **Configuración del Proyecto**:
   - Clonar el repositorio: `git clone <URL-del-repositorio>`
   - Abrir el proyecto en Visual Studio
2. **Inicialización de la Base de Datos**:
   - Ejecutar el script SQL para crear las tablas y procedimientos almacenados
   - Configurar la cadena de conexión en el archivo `appsettings.json`
3. **Ejecución del Proyecto**:
   - Compilar y ejecutar el proyecto desde Visual Studio
   - Acceder a la interfaz web desde el navegador
4. **Datos de Prueba**:
   - Cargar datos de prueba en la base de datos (si aplica)

## Solución de Problemas Comunes 🔧
- **Errores de Conexión a la Base de Datos**:
  - Verificar la cadena de conexión en `appsettings.json`
  - Asegurarse de que el servidor SQL esté en ejecución

## Criterios de Evaluación 📋
1. **Calidad del Código**:
   - Limpieza, organización y uso de buenas prácticas
2. **Funcionalidad**:
   - Correcto funcionamiento de las operaciones CRUD
   - Validaciones en el frontend y backend
3. **Estética y Usabilidad**:
   - Diseño funcional, limpio y fácil de usar
4. **Base de Datos**:
   - Uso correcto de procedimientos almacenados y relaciones entre tablas

¡Gracias por revisar este proyecto! 🙌
