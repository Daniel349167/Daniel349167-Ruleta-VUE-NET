<template>
  <div class="juego-ruleta">
    <div class="container d-flex flex-column justify-content-center align-items-center min-vh-100">
      <div class="juego-ruleta-card card p-4 shadow" style="width: 28rem;">
        <h1 class="juego-ruleta-h1 card-title text-center fw-bold">Juego de la Ruleta</h1>
        <div class="card-body">
          <p class="mb-3 saldo-text">
            <span class="fw-bold">Nombre: </span><span class="nombre-usuario">{{ nombreUsuario }}</span>
            <br>
            <span class="fw-bold">Saldo: </span><span class="saldo-valor">S/{{ saldo }}</span>
          </p>
          <div class="mb-3">
            <input v-model.number="apuesta" type="number" class="form-control" placeholder="Monto de Apuesta" />
          </div>
          <div class="text-center mb-3">
            <button @click="girarRuleta" class="btn btn-primary" :disabled="girando || !selectedApuestas.length">Empezar Juego</button>
          </div>
          <div class="text-center ruleta-container mb-4">
            <img :src="ruletaImg" alt="Ruleta" class="ruleta" :class="{ 'girar': girando }" />
            <div class="bola" :class="{ 'animar-bola': girando }" :style="posicionBola"></div>
            <div v-if="mostrarResultado && resultado" class="resultado" :style="{ color: resultado.color === 'rojo' ? 'red' : resultado.color === 'negro' ? 'black' : 'green' }">
              {{ resultado.numero }}
            </div>
          </div>
          <div class="d-grid gap-2 mt-3">
            <div class="btn-group mb-2">
              <button @click="toggleApuesta('rojo')" :class="['btn', selectedApuestas.includes('rojo') ? 'btn-danger' : 'btn-outline-danger']">Rojo</button>
              <button @click="toggleApuesta('negro')" :class="['btn', selectedApuestas.includes('negro') ? 'btn-dark' : 'btn-outline-dark']">Negro</button>
            </div>
            <div class="btn-group">
              <button @click="toggleApuesta('par')" :class="['btn', selectedApuestas.includes('par') ? 'btn-success' : 'btn-outline-success']">Par</button>
              <button @click="toggleApuesta('impar')" :class="['btn', selectedApuestas.includes('impar') ? 'btn-warning' : 'btn-outline-warning']">Impar</button>
            </div>
          </div>
          <div class="mt-3 text-center mb-3">
            <button @click="mostrarModalGuardarSaldo" class="btn btn-success me-2">Guardar Saldo</button>
            <button @click="mostrarModal" class="btn btn-info">Recargar Saldo</button>
          </div>
          <div v-if="mensajeResultado" class="alert alert-info" role="alert" v-html="mensajeResultado"></div>
          <div v-if="mensaje" :class="['alert', tipoMensaje === 'success' ? 'alert-success' : 'alert-danger', tipoMensaje === 'success' ? 'ganancia' : '']" role="alert" v-html="mensaje"></div>
        </div>
        <audio ref="ruletaAudio" :src="ruletaSound" preload="auto"></audio>
      </div>
    </div>

    <!-- Modal para recargar saldo -->
    <b-modal v-model="modalVisible" title="Recargar Saldo" @ok="recargarSaldo">
      <div>
        <label for="monto-recarga">Ingrese el monto a recargar:</label>
        <input v-model="montoRecarga" id="monto-recarga" type="number" class="form-control" placeholder="Monto" />
      </div>
    </b-modal>

    <!-- Modal para confirmar guardar saldo -->
    <b-modal v-model="modalGuardarSaldoVisible" title="Guardar Saldo" @ok="confirmarGuardarSaldo">
      <div>
        <p>¿Está seguro de que desea guardar el saldo de las apuestas hechas hasta el momento?</p>
      </div>
    </b-modal>
  </div>
</template>


<script src="../scripts/JuegoRuleta.js"></script>
<style src="../styles/JuegoRuleta.css"></style>
