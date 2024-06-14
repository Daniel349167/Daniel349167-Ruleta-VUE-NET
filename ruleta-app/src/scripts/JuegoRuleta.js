export default {
  name: 'JuegoRuleta',
  data() {
    return {
      saldo: 0,
      apuesta: 0,
      resultado: null,
      mostrarResultado: false,
      ruletaImg: require('@/assets/ruleta.png'),
      ruletaSound: require('@/assets/ruleta.mp3'),
      errorSound: require('@/assets/error2.mp3'),
      girando: false,
      numeroRuleta: null,
      posicionBola: { top: '50%', left: '50%' },
      selectedApuestas: [],
      mensaje: '',
      mensajeResultado: '',
      tipoMensaje: '',
      usuarioId: null,
      nombreUsuario: '',
      montoRecarga: 0,
      modalVisible: false,
      modalGuardarSaldoVisible: false,
      angulosRuleta: {
        0: 0.2, 32: 8.5, 15: 18, 19: 27, 4: 36.5,
        21: 45, 2: 55, 25: 65, 17: 75, 34: 85,
        6: 95.5, 27: 106, 13: 115, 36: 125.5,
        11: 137, 30: 147, 8: 157, 23: 167.5,
        10: 178.07, 5: 187.8, 24: 197.53, 16: 207.26,
        33: 218, 1: 226.72, 20: 236.45, 14: 246.18,
        31: 255.91, 9: 265, 22: 273, 18: 290,
        29: 298.5, 7: 307, 28: 316, 12: 324.02,
        35: 333.75, 3: 342, 26: 351.5
      }
    };
  },
  async mounted() {
    await this.cargarSaldo();
  },
  methods: {
    async cargarSaldo() {
      const nombreUsuario = localStorage.getItem('usuarioNombre');
      if (nombreUsuario) {
        this.nombreUsuario = nombreUsuario;
        try {
          const response = await fetch(`${process.env.VUE_APP_API_URL}/buscar-usuario/${nombreUsuario}`);
          if (response.ok) {
            const usuario = await response.json();
            this.saldo = usuario.saldo;
            this.usuarioId = usuario.id;
          } else {
            this.mostrarMensaje('Error al cargar el saldo.', 'danger');
          }
        } catch (error) {
          this.mostrarMensaje('Error de conexión.', 'danger');
        }
      }
    },
    
    async guardarSaldoYGuardarApuestas() {
      const nombreUsuario = localStorage.getItem('usuarioNombre');
      if (nombreUsuario) {
        try {
          const response = await fetch(`${process.env.VUE_APP_API_URL}/guardar-apuestas/${nombreUsuario}`, {
            method: 'POST'
          });
          if (response.ok) {
            const data = await response.json();
            this.saldo = data.saldo;
            this.mostrarMensaje('Saldo Guardado.', 'success');
          } else {
            this.mostrarMensaje('Error al guardar el saldo y las apuestas.', 'danger');
          }
        } catch (error) {
          this.mostrarMensaje('Error de conexión.', 'danger');
        }
      }
    },
    
    mostrarModal() {
      this.modalVisible = true;
    },
    
    mostrarModalGuardarSaldo() {
      this.modalGuardarSaldoVisible = true;
    },
    
    async confirmarGuardarSaldo() {
      await this.guardarSaldoYGuardarApuestas();
      this.modalGuardarSaldoVisible = false;
    },
    
    async recargarSaldo() {
      const nombreUsuario = localStorage.getItem('usuarioNombre');
      if (nombreUsuario && this.montoRecarga > 0) {
        try {
          const response = await fetch(`${process.env.VUE_APP_API_URL}/recargar-saldo/${nombreUsuario}`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({ monto: this.montoRecarga })
          });
          if (response.ok) {
            const data = await response.json();
            this.saldo = data.saldo;
            this.mostrarMensaje('Saldo recargado exitosamente.', 'success');
          } else {
            this.mostrarMensaje('Error al recargar el saldo.', 'danger');
          }
        } catch (error) {
          this.mostrarMensaje('Error de conexión.', 'danger');
        }
      }
    },
    
    async girarRuleta() {
      if (this.apuesta > this.saldo) {
        const audio = new Audio(this.errorSound);
        audio.play();
        this.mostrarMensaje('Saldo Insuficiente', 'danger');
        return;
      }
      const nombreUsuario = localStorage.getItem('usuarioNombre');
      if (nombreUsuario && this.usuarioId !== null) {
        const apuesta = {
          usuarioId: this.usuarioId,
          tipo: this.selectedApuestas.length > 1 ? 'color_y_paridad' : this.selectedApuestas[0] === 'par' || this.selectedApuestas[0] === 'impar' ? 'paridad' : 'color',
          valor: this.selectedApuestas.length > 1 ? null : this.selectedApuestas[0],
          color: this.selectedApuestas.includes('rojo') || this.selectedApuestas.includes('negro') ? this.selectedApuestas.find(a => a === 'rojo' || a === 'negro') : null,
          paridad: this.selectedApuestas.includes('par') || this.selectedApuestas.includes('impar') ? this.selectedApuestas.find(a => a === 'par' || a === 'impar') : null,
          monto: this.apuesta
        };
        try {
          const response = await fetch(`${process.env.VUE_APP_API_URL}/apuesta`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(apuesta)
          });
          if (response.ok) {
            const data = await response.json();
            this.iniciarAnimacionRuleta(data);
          } else {
            this.mostrarMensaje('Error al realizar la apuesta.', 'danger');
          }
        } catch (error) {
          this.mostrarMensaje('Error de conexión.', 'danger');
        }
      }
    },
    
    toggleApuesta(tipo) {
      const index = this.selectedApuestas.indexOf(tipo);
      if (index !== -1) {
        this.selectedApuestas.splice(index, 1);
      } else {
        if (['rojo', 'negro'].includes(tipo) && this.selectedApuestas.some(a => ['rojo', 'negro'].includes(a))) {
          this.selectedApuestas = this.selectedApuestas.filter(a => !['rojo', 'negro'].includes(a));
        }
        if (['par', 'impar'].includes(tipo) && this.selectedApuestas.some(a => ['par', 'impar'].includes(a))) {
          this.selectedApuestas = this.selectedApuestas.filter(a => !['par', 'impar'].includes(a));
        }
        this.selectedApuestas.push(tipo);
      }
    },
    
    iniciarAnimacionRuleta(data) {
      this.girando = true;
      this.mostrarResultado = false;
      this.mensaje = '';
      this.mensajeResultado = '';
      this.numeroRuleta = data.number;
      this.numeroActual = this.calcularNumeroInicial(this.numeroRuleta);
      this.pasos = (this.numeroRuleta + 37 - this.numeroActual) % 37;
      this.$refs.ruletaAudio.currentTime = 0.9;
      this.$refs.ruletaAudio.play();
      this.intervalId = setInterval(this.moverBola, 5000 / this.pasos);
      setTimeout(() => {
        clearInterval(this.intervalId);
        this.girando = false;
        this.posicionBola = this.calcularPosicionBola(this.numeroRuleta);
        this.$refs.ruletaAudio.pause();
        this.$refs.ruletaAudio.currentTime = 0;

        // Determinar si ganó o perdió
        if (data.gano) {
          this.saldo += data.premio;
          if (this.apuesta !== 0) {
            this.mostrarMensaje(`Ganaste <span class="azul"> ${data.premio}</span> soles`, 'success');
          } else {
            this.mostrarMensaje('Ganaste', 'success');
          }
        } else {
          this.saldo -= this.apuesta;
          this.mostrarMensaje('Perdiste', 'danger');
        }

        this.mostrarResultado = true;
        this.resultado = data;
        this.mostrarMensajeResultado(data);
      }, 5000);
    },

    moverBola() {
      this.numeroActual = (this.numeroActual + 1) % 37;
      this.posicionBola = this.calcularPosicionBola(this.numeroActual);
    },
    
    calcularPosicionBola(numero) {
      const angulo = this.angulosRuleta[numero];
      const x = 50 + 35 * Math.cos((angulo - 90) * (Math.PI / 180));
      const y = 50 + 35 * Math.sin((angulo - 90) * (Math.PI / 180));
      return { left: `${x}%`, top: `${y}%` };
    },
    
    calcularNumeroInicial(numeroRuleta) {
      const anguloObjetivo = this.angulosRuleta[numeroRuleta];
      let anguloInicial = (anguloObjetivo - (360 * 5) / (5000 / 50)) % 360;
      if (anguloInicial < 0) anguloInicial += 360;
      const numeroInicial = Object.keys(this.angulosRuleta).reduce((acc, numero) => {
        const angulo = this.angulosRuleta[numero];
        return Math.abs(angulo - anguloInicial) < Math.abs(this.angulosRuleta[acc] - anguloInicial) ? numero : acc;
      }, 0);
      return parseInt(numeroInicial);
    },
    
    mostrarMensajeResultado(data) {
      this.mensajeResultado = `<span class="azul">Resultado: ${data.color ? data.color : ''} - ${data.paridad ? data.paridad : ''}</span>`;
    },
    
    mostrarMensaje(mensaje, tipo) {
      this.mensaje = mensaje;
      this.tipoMensaje = tipo;
      if (tipo === 'success') {
        this.$nextTick(() => {
          const alertElement = document.querySelector('.alert-success');
          if (alertElement) {
            alertElement.classList.add('ganancia');
          }
        });
      }
    },
    async borrarApuestasTemporales() {
      const nombreUsuario = localStorage.getItem('usuarioNombre');
      if (nombreUsuario) {
        await fetch(`${process.env.VUE_APP_API_URL}/borrar-apuestas-temporales/${nombreUsuario}`, {
          method: 'DELETE'
        });
      }
    }
  },
  created() {
    window.addEventListener('beforeunload', this.borrarApuestasTemporales);
  },
  beforeDestroy() {
    window.removeEventListener('beforeunload', this.borrarApuestasTemporales);
  }
};