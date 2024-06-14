export default {
  name: 'InicioJuego',
  data() {
    return {
      nombre: '',
      error: '',
      usuario: null,
    };
  },
  methods: {
    async empezarJuego() {
      if (!this.nombre) {
        this.error = 'Tu nombre no puede estar vacío.';
      } else {
        this.error = '';
        try {
          const response = await fetch(`${process.env.VUE_APP_API_URL}/buscar-usuario/${this.nombre}`);
          if (response.ok) {
            this.usuario = await response.json();
            localStorage.setItem('usuarioNombre', this.usuario.nombre); // Guardar el nombre del usuario en localStorage
            // Navegar a la vista del juego con el saldo del usuario existente
            this.$router.push({ name: 'JuegoRuleta', params: { nombre: this.nombre, saldo: this.usuario.saldo } });
          } else if (response.status === 404) {
            // Usuario no encontrado, crearlo
            const nuevoUsuario = { nombre: this.nombre };
            const createResponse = await fetch(`${process.env.VUE_APP_API_URL}/crear-usuario`, {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(nuevoUsuario)
            });
            if (createResponse.ok) {
              this.usuario = await createResponse.json();
              localStorage.setItem('usuarioNombre', this.usuario.nombre); // Guardar el nombre del usuario en localStorage
              // Navegar a la vista del juego con el nuevo usuario
              this.$router.push({ name: 'JuegoRuleta', params: { nombre: this.nombre, saldo: this.usuario.saldo } });
            } else {
              this.error = 'Hubo un error al crear el usuario.';
            }
          } else {
            this.error = 'Hubo un error al buscar el usuario.';
          }
        } catch (error) {
          this.error = 'Hubo un error de conexión.';
        }
      }
    },
    async borrarApuestasTemporales() {
      const nombreUsuario = localStorage.getItem('usuarioNombre');
      if (nombreUsuario) {
        await fetch(`${process.env.VUE_APP_API_URL}/borrar-apuestas-temporales/${nombreUsuario}`, {
          method: 'DELETE'
        });
        localStorage.removeItem('usuarioNombre'); // Remover el nombre del usuario de localStorage después de borrar las apuestas
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