# El Juego de la Ruleta

Este proyecto es una implementación del juego de la ruleta que cumple con los requisitos de la prueba técnica. El proyecto incluye una interfaz gráfica desarrollada en Vue.js 3 y una API REST implementada usando ASP.NET Core 8. El servidor web utilizado es IIS (Internet Information Service).

## Descripción del Proyecto

El objetivo del proyecto es permitir a los usuarios jugar a la ruleta con las siguientes características:

1. **Interfaz de Usuario**: Diseñada en Vue.js 3, permite a los usuarios interactuar con el juego.
2. **Inicio del Juego**: Los usuarios pueden iniciar el juego declarando un monto inicial como su saldo y una apuesta.
3. **Resultados de la Ruleta**: Al iniciar el juego, se muestra el número obtenido en la ruleta, así como si es par/impar y su color (rojo/negro).
4. **Apuestas**: Los usuarios pueden apostar por colores (rojo/negro) o por pares/impares de un color dado.
5. **Resultados de las Apuestas**: Los usuarios pueden ver si han ganado o perdido según la apuesta realizada.
6. **Gestión del Saldo**: Los usuarios pueden guardar su monto ganado asignándolo a su nombre.
7. **Carga de Saldo**: Permite cargar el saldo de un usuario a partir de su nombre.

## Características de la API REST

La API REST implementada en ASP.NET Core 8 incluye los siguientes endpoints:

1. **Obtener Número y Color Aleatorio**: Devuelve un número entre 0 y 36 y un color (rojo/negro).
2. **Guardar Monto de Usuarios**: Guarda el saldo de los usuarios en una base de datos SQL. Si el usuario ya existe, se actualiza el saldo.
3. **Devolver Monto de Premio**: Calcula y devuelve el monto del premio basado en la apuesta del usuario.

## Requisitos del Sistema

- **Frontend**: Vue.js 3
- **Backend**: ASP.NET Core 8
- **Servidor Web**: IIS (Internet Information Service)
- **Base de Datos**: PostgreSQL

## Instalación y Configuración

### Frontend

1. Clonar el repositorio:
   ```sh
   git clone https://github.com/danielureta/Prueba-Tecnica-Juego-Ruleta.git
   cd Prueba-Tecnica-Juego-Ruleta/frontend
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
   git clone https://github.com/danielureta/Prueba-Tecnica-Juego-Ruleta.git
   cd Prueba-Tecnica-Juego-Ruleta/backend
   ```

2. Abrir el proyecto en Visual Studio.

3. Configurar la cadena de conexión a la base de datos en `appsettings.json`.

4. Construir y ejecutar la aplicación desde Visual Studio.


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

