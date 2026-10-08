<div align="center">

# 📇 Agenda Interna para Consultora
### *2º DAM • María Ledesma Zotano*

![C#](https://img.shields.io/badge/Language-C%23-blueviolet?style=for-the-badge&logo=csharp)
![.NET](https://img.shields.io/badge/Framework-.NET%208.0-512BD4?style=for-the-badge&logo=dotnet)
![Status](https://img.shields.io/badge/Fase%20Actual-Fase%202%20Completada-brightgreen?style=for-the-badge)

</div>

---

## 📋 Índice
1. [📖 Descripción del Proyecto](#-descripción-del-proyecto)
2. [🚀 Guía Paso a Paso para Principiantes (Desde Cero)](#-guía-paso-a-paso-para-principiantes-desde-cero)
   - [Paso 1: Descargar e instalar el entorno (.NET SDK)](#paso-1-descargar-e-instalar-el-entorno-net-sdk)
   - [Paso 2: Descargar el código fuente](#paso-2-descargar-el-código-fuente)
   - [Paso 3: Abrir la consola de comandos](#paso-3-abrir-la-consola-de-comandos)
   - [Paso 4: Ejecutar la aplicación](#paso-4-ejecutar-la-aplicación)
3. [💻 Guía para Desarrolladores (Visual Studio / VS Code)](#-guía-para-desarrolladores-visual-studio--vs-code)
4. [📌 Fases de Desarrollo del Proyecto](#-fases-de-desarrollo-del-proyecto)
5. [🗂️ Estructura del Código Fuente](#️-estructura-del-código-fuente)
6. [🛡️ Funcionalidades Clave y Robustez](#️-funcionalidades-clave-y-robustez)
7. [🎨 Estética de la Aplicación](#-estética-de-la-aplicación)

---

## 📖 Descripción del Proyecto

La **Agenda Interna para Consultora** es un programa desarrollado en C# que permite centralizar y organizar la información de las personas de contacto y las distintas entidades empresariales con las que colabora una firma consultora.

El programa destaca por su **usabilidad**, **diseño defensivo** (imposible de bloquear ante errores tipográficos o entradas inesperadas) y una **arquitectura limpia orientada a objetos**.

---

## 🚀 Guía Paso a Paso para Principiantes (Desde Cero)

> **¿Nunca has programado o no tienes nada instalado en tu ordenador?** No te preocupes. Sigue estos sencillos pasos para hacer funcionar la agenda en cuestión de minutos.

### Paso 1: Descargar e instalar el entorno (.NET SDK)

Para que tu ordenador sepa cómo ejecutar programas de C#, necesitas instalar un motor gratuito creado por Microsoft llamado **.NET SDK**.

1. Entra en la web oficial de descargas de Microsoft: [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
2. Haz clic en el botón verde **Download .NET SDK** (se recomienda la versión .NET 8.0 o superior).
3. Abre el archivo descargado y completa la instalación haciendo clic en *"Siguiente"* / *"Instalar"* como con cualquier otro programa.

---

### Paso 2: Descargar el código fuente

1. Si estás en **GitHub**, haz clic en el botón verde **`<Code>`** situado en la parte superior derecha y selecciona **Download ZIP**.
2. Descomprime la carpeta descargada en un lugar fácil de encontrar (por ejemplo, en el **Escritorio**).

---

### Paso 3: Abrir la consola de comandos

#### 🪟 En Windows:
1. Abre la carpeta descomprimida del proyecto donde están los archivos `.cs`.
2. Haz clic en la barra de direcciones superior de la carpeta (donde se ve la ruta de directorios).
3. Escribe `cmd` y pulsa <kbd>Enter</kbd>. Se abrirá una ventana negra (Símbolo del sistema) exactamente en esa carpeta.

#### 🍎 En macOS / 🐧 Linux:
1. Abre la aplicación **Terminal**.
2. Escribe `cd ` (con un espacio al final) y arrastra la carpeta del proyecto directamente dentro de la ventana de la Terminal.
3. Pulsa <kbd>Enter</kbd>.

---

### Paso 4: Ejecutar la aplicación

En la consola de comandos que acabas de abrir, escribe el siguiente comando y pulsa <kbd>Enter</kbd>:

```bash
dotnet run
```

¡Listo! El programa se compilará automáticamente y verás aparecer el **Menú Principal** de la agenda en la pantalla.

---

## 💻 Guía para Desarrolladores (Visual Studio / VS Code)

Si ya eres estudiante o profesional con entorno de desarrollo instalado:

### Con Visual Studio:
1. Abre Visual Studio.
2. Selecciona **Abrir un proyecto o una solución** o añade los archivos `.cs` en una nueva *Aplicación de consola (.NET Core)*.
3. Pulsa la tecla <kbd>F5</kbd> para ejecutar en modo depuración.

### Con Visual Studio Code:
1. Abre la carpeta del proyecto en VS Code.
2. Abre la terminal integrada (`Ctrl + ~` o `Cmd + ~`).
3. Ejecuta `dotnet run`.

---

## 📌 Fases de Desarrollo del Proyecto

### 🟡 Fase 1: Gestión de Personas con Menú *(Completada)*
- Menú interactivo dedicado a personas.
- Registro completo de campos obligatorios: *Nombre, Apellidos, Teléfono, Correo electrónico y Empresa asignada*.
- **Identificador autogenerado** (`idPersona`) secuencial e independiente.
- Búsqueda flexible (por ID numérico o por fragmento de texto en nombre/apellidos).
- Modificación de datos con previsualización del registro.
- Baja con confirmación previa.

### 🟡 Fase 2: Gestión de Empresas y Submenús *(Completada)*
- Arquitectura modular con submenús independientes (*Personas* y *Empresas*) unificados desde un **Menú Principal**.
- Alta, listado, búsqueda, modificación y baja de entidades empresariales.
- Validación obligatoria de **CIF** y **Nombre Comercial**.
- **Borrado Lógico (Estado 1/0):** Las empresas no se eliminan físicamente de la memoria al dar de baja; cambian su propiedad `Activo` a `false`. Esto preserva la integridad de los datos para no romper relaciones activas.

### 🔴 Fase 3: Relación e Integración de Entidades *(Próximamente)*
- Vinculación formal de tipo **1:N** (Una empresa puede tener asociadas múltiples personas).
- Asignación de personas a empresas mediante seleccionador de ID existente.
- Muestreo detallado de relaciones (ver la ficha completa de una empresa junto con el listado de personas que pertenecen a ella).

---

## 🗂️ Estructura del Código Fuente

El proyecto aplica el principio de **Responsabilidad Única (SRP)** manteniendo los archivos estrictamente organizados:

```
📁 AgendaConsultora/
│
├── 📄 Program.cs             # Punto de entrada principal. Contiene el menú raíz.
├── 📄 GestionPersonas.cs     # Lógica de negocio, lista y submenú para Personas.
├── 📄 GestionEmpresas.cs     # Lógica de negocio, lista y submenú para Empresas (Borrado lógico).
├── 📄 Persona.cs             # Modelo de entidad de Persona con ID auto-incremental.
├── 📄 Empresa.cs             # Modelo de entidad de Empresa con propiedad de borrado lógico (Activo).
└── 📄 Validador.cs           # Módulo transversal de validación de datos por consola.
```

---

## 🛡️ Funcionalidades Clave y Robustez

- 🔒 **Inmunidad a bloqueos:** Si el usuario introduce letras en un campo numérico o una opción no existente en los menús, el sistema muestra un mensaje de advertencia y vuelve a solicitar el dato sin cerrarse.
- 📧 **Validación con Regex:** Uso de Expresiones Regulares para verificar que los teléfonos y correos electrónicos tengan una estructura coherente.
- 💬 **Confirmaciones explícitas:** Las acciones críticas (modificaciones y bajas) requieren una confirmación explícita `(S/N)` antes de aplicarse.

---

## 🎨 Estética de la Aplicación

La interfaz de consola hace uso de secuencias de escape **ANSI** para ofrecer un diseño moderno y agradable con encabezados destacados en tono **Rosa Pastel** (`\x1b[38;2;255;182;193m`):

```text
==================================
   AGENDA 2º DAM (Menú Principal)   
==================================
1. Gestión de Personas
2. Gestión de Empresas
3. Salir
==================================
Seleccione una opción (1-3):
```

---

<div align="center">
  <sub>Desarrollo de Interfaces • 2026</sub>
</div>