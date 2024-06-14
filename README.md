# El Juego de la Ruleta

Este proyecto es una implementación del juego de la ruleta. El proyecto incluye una interfaz gráfica desarrollada en Vue.js 3 y una API REST implementada usando ASP.NET Core 8.

## Descripción del Proyecto

El objetivo del proyecto es permitir a los usuarios jugar a la ruleta con las siguientes características:

1. **Interfaz de Usuario**: Diseñada en Vue.js 3, permite a los usuarios interactuar con el juego.
2. **Inicio del Juego**: Se puede iniciar el juego creando un usuario con un nombre o logueandose con su nombre, recargando un monto inicial como su saldo y haciendo una apuesta.
3. **Resultados de la Ruleta**: Al iniciar el juego, se muestra el número obtenido en la ruleta, así como si es par/impar y su color (rojo/negro).
4. **Apuestas**: Los usuarios pueden apostar por colores (rojo/negro) o por pares/impares de un color dado.
5. **Resultados de las Apuestas**: Los usuarios pueden ver si han ganado o perdido según la apuesta realizada.
6. **Gestión del Saldo**: Los usuarios pueden guardar su monto ganado.
7. **Carga de Saldo**: Permite cargar el saldo de un usuario.

**Nota**: Si el usuario recarga o cierra la ventana, se perderán las apuestas que se han realizado y no se han guardado.

## Características de la API REST

La API REST implementada en ASP.NET Core 8 incluye los siguientes endpoints:

1. **Obtener Número y Color Aleatorio**: Devuelve un número entre 0 y 36 y un color (rojo/negro).
2. **Guardar Monto de Usuarios**: Guarda el saldo de los usuarios en una base de datos SQL. Si el usuario ya existe, se actualiza el saldo.
3. **Devolver Monto de Premio**: Calcula y devuelve el monto del premio basado en la apuesta del usuario.

## Estructura de la Base de Datos

El proyecto utiliza tres tablas principales:

1. **Usuarios**: Almacena información sobre el nombre de los usuarios que es su identificador principal y sus saldos actuales.
2. **Apuestas**: Almacena las apuestas realizadas y guardadas por los usuarios.
3. **ApuestasTemporales**: Almacena las apuestas que se están realizando pero no se han guardado aún. Estas apuestas se perderán si el usuario recarga o cierra la ventana.

## Requisitos del Sistema

- **Frontend**: Vue.js 3
- **Backend**: ASP.NET Core 8
- **Base de Datos**: PostgreSQL

## Instalación y Configuración

### Frontend

1. Clonar el repositorio:
   ```sh
   git clone https://github.com/Daniel349167/Ruleta-VUE-NET.git
   cd Ruleta-VUE-NET/ruleta-app
   ```

2. Instalar dependencias:
   ```sh
   npm install
   ```

3. Ejecutar el servidor de desarrollo:
   ```sh
   npm run serve
   ```

### Backend

1. Clonar el repositorio:
   ```sh
   git clone https://github.com/Daniel349167/Ruleta-VUE-NET.git
   cd Ruleta-VUE-NET/RuletaAPI
   ```

2. Abrir el proyecto en Visual Studio.

3. Configurar la cadena de conexión a la base de datos en `appsettings.json`.

4. Ejecutar las migraciones para crear y actualizar la base de datos en PostgreSQL:
   ```sh
   dotnet ef database update
   ```

6. Construir y ejecutar la aplicación desde Visual Studio.

## Uso

1. Abrir el navegador y navegar a la dirección del frontend (por defecto `http://localhost:8080`).

2. Iniciar el juego declarando un nombre y posteriormente un monto inicial y apuesta.

3. Ver el resultado de la ruleta y saber si ganó o perdió.

4. Guardar el monto ganado si así lo desea.

## Limitaciones

- La implementación actual solo permite apuestas por colores (rojo/negro) y pares/impares de un color dado. No se permite apostar por números específicos.

## Imágenes del Proyecto

![image](https://github.com/Daniel349167/Ruleta-VUE-NET/assets/62466867/68c73506-d0f7-4dce-93f8-b6af32b1d103)

![image](https://github.com/Daniel349167/Ruleta-VUE-NET/assets/62466867/52d3a01a-a085-4b47-ad50-498ddaa9bb9f)

---

Para más información, por favor contacta a [Daniel Ureta](https://github.com/Daniel349167).

---

¡Gracias por revisar este proyecto! Espero que encuentres esta implementación útil y clara.

