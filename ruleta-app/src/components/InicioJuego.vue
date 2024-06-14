<template>
  <div class="inicio-juego">
    <div class="inicio-juego-container d-flex flex-column justify-content-center align-items-center min-vh-100">
      <div class="inicio-juego-card card p-4 shadow" style="width: 22rem;">
        <h1 class="inicio-juego-h1 card-title text-center">Juego de la Ruleta</h1>
        <div class="card-body">
          <div class="mb-3">
            <input v-model="nombre" class="form-control" placeholder="Tu Nombre" />
            <div v-if="error" class="text-danger mt-2">{{ error }}</div>
          </div>
          <button @click="empezarJuego" class="btn btn-primary w-100">Empezar Juego</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script src="../scripts/InicioJuego.js"></script>
<style src="../styles/InicioJuego.css"></style>