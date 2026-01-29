# WorkshopManager


Aplicación web desarrollada con **ASP.NET Core MVC** para la gestión de un taller mecánico pequeño (clientes, vehículos, citas y órdenes de trabajo).

El objetivo del proyecto es **aprender y aplicar buenas prácticas profesionales** en el desarrollo web con .NET, utilizando una arquitectura limpia y tecnologías actuales orientadas a la empleabilidad.

---

## 🚀 Tecnologías utilizadas

- ASP.NET Core 8 (MVC)
- C#
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap 5
- JavaScript
- Git & GitHub

---

## 🧱 Arquitectura

El proyecto sigue principios de **Clean Architecture** y separación de responsabilidades:

- **Domain**: Entidades y lógica de negocio
- **Application**: Casos de uso, servicios, DTOs y validaciones
- **Infrastructure**: Acceso a datos, EF Core y repositorios
- **Web**: Capa de presentación (MVC)

---

## 📌 Funcionalidades previstas

- Autenticación y roles (administración / mecánico)
- Gestión de clientes
- Gestión de vehículos
- Citas y órdenes de trabajo
- Historial de reparaciones
- Base para futuras ampliaciones (facturación, stock, API)

---

## 🛠️ Estado actual

Actualmente el proyecto incluye:

- CRUD completo de Clientes
- CRUD completo de Vehículos
- Gestión de Citas con estados y validaciones
- Arquitectura en capas con servicios y repositorios

---

## 📂 Control de versiones

El proyecto utiliza Git con un historial de commits claros y descriptivos, siguiendo buenas prácticas.

---

## 📄 Licencia

Proyecto de uso educativo y demostrativo.

## ▶️ Cómo ejecutar el proyecto

1. Clonar el repositorio
2. Configurar la cadena de conexión a SQL Server
3. Ejecutar las migraciones con Entity Framework
4. Ejecutar el proyecto desde Visual Studio

---

## 📚 Aprendizajes clave

- Aplicación de Clean Architecture en ASP.NET Core MVC
- Separación clara entre dominio, aplicación y UI
- Uso de Entity Framework Core con repositorios
- Manejo de validaciones y flujos MVC reales
- Uso profesional de Git y commits incrementales
