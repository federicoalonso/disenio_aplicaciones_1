﻿using System.Collections.Generic;
using Negocio.Categorias;
using Negocio.Contrasenias;
using Negocio.TarjetaCreditos;
using Negocio.DataBreaches;
using Negocio.Excepciones;
using System.Configuration;
using Negocio.Usuarios;

namespace Negocio
{
    public class Sesion
    {
        private const string MENSAJE_ERROR_NO_LOGUEADO = "Debe estar logueado para realizar esta acción.";
        
        private static Sesion instancia;
        private GestorCategorias gestorCategoria;
        private GestorContrasenias gestorContrasenia;
        private GestorTarjetaCredito gestorTarjetaCredito;
        private GestorDataBreaches gestorDataBreaches;
        private Usuario usuario;
        private bool logueado;

        public static Sesion ObtenerInstancia()
        {
            if (instancia == null) instancia = new Sesion();
            return instancia;
        }

        private Sesion()
        {
            gestorCategoria = new GestorCategorias();
            gestorContrasenia = new GestorContrasenias();
            gestorTarjetaCredito = new GestorTarjetaCredito();
            gestorDataBreaches = new GestorDataBreaches();
            this.usuario = new Usuario();
            this.logueado = false;
        }
        public void Login(string password)
        {
            this.logueado = usuario.Login(password);
        }

        public IEnumerable<Contrasenia> ContraseniasVulnerables()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            List<IFuente> fuentes = gestorDataBreaches.ObtenerFuentes();
            return this.gestorContrasenia.ObtenerContraseniasVulnerables(fuentes);
        }
 
        public IEnumerable<TarjetaCredito> TarjetasCreditoVulnerables()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            List<IFuente> fuentes = gestorDataBreaches.ObtenerFuentes();
            return this.gestorTarjetaCredito.ObtenerTarjetasVulnerables(fuentes);
        }

        public void CambiarContextoDeBaseDeDatos(string contexto)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            ConfigurationManager.AppSettings["DATABASE_CONTEXT"] = contexto;
        }

        public void CargarDataBreach(IFuente fuente, string texto)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorDataBreaches.CargarDataBreach(fuente,texto);
        }

        public void GuardarPrimerPassword(string primerPassword)
        {
            usuario.GuardarPrimerPassword(primerPassword);
        }

        public void CambiarPassword(string nuevoPassword)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado();
            usuario.CambiarPassword(nuevoPassword);
        }

        public int AltaCategoria(string nombreCategoria)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return this.gestorCategoria.Alta(nombreCategoria);
        }

        public void BajaCategoria(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            this.gestorCategoria.Baja(id);
        }

        public void ModificarCategoria(int id, string nombreNuevo)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            this.gestorCategoria.ModificarCategoria(id, nombreNuevo);
        }

        public Categoria BuscarCategoriaPorId(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorCategoria.BuscarCategoriaPorId(id);
        }

        public IEnumerable<Categoria> ObtenerTodasLasCategorias()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return this.gestorCategoria.ObtenerTodas();
        }

        public int AltaTarjetaCredito(TarjetaCredito nuevaTarjeta)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            BuscarCategoriaPorId(nuevaTarjeta.Categoria.Id);
            return this.gestorTarjetaCredito.Alta(nuevaTarjeta);
        }

        public void BajaTarjetaCredito(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorTarjetaCredito.Baja(id);
        }

        public void BajaHistorial(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorDataBreaches.Baja(id);
        }

        public void ModificarTarjeta(TarjetaCredito modificada)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorTarjetaCredito.ModificarTarjeta(modificada);
        }

        public TarjetaCredito BuscarTarjeta(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorTarjetaCredito.Buscar(id);
        }

        public IEnumerable<TarjetaCredito> ObtenerTodasLasTarjetas()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorTarjetaCredito.ObtenerTodas();
        }

        public int AltaContrasenia(Contrasenia unaContrasena)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorCategoria.BuscarCategoriaPorId(unaContrasena.Categoria.Id);
            return gestorContrasenia.Alta(unaContrasena);
        }

        public void BajaContrasenia(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorContrasenia.Baja(id);
        }

        public void ModificarContrasenia(Contrasenia modificada)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorContrasenia.ModificarContrasenia(modificada);
        }

        public Contrasenia BuscarContrasenia(int id)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorContrasenia.Buscar(id);
        }

        public IEnumerable<Contrasenia> ObtenerTodasLasContrasenias()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorContrasenia.ObtenerTodas();
        }
       
        public string MostrarPassword(Contrasenia contrasenia)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorContrasenia.MostrarPassword(contrasenia);
        }

        public void LogOut()
        {
            this.logueado = false;
        }
        public int ConsultarVulnerabilidades()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorDataBreaches.ConsultarVulnerabilidades(ContraseniasVulnerables(), TarjetasCreditoVulnerables());
        }
        public IEnumerable<HistorialContrasenia> DevolverContraseniasVulnerables(int historial)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorDataBreaches.DevolverContraseniasVulnerables(historial);
        }
        public IEnumerable<HistorialTarjetas> DevolverTarjetasVulnerables(int historial)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorDataBreaches.DevolverTarjetasVulnerables(historial);
        }
        public IEnumerable<Historial> ObtenerTodasLosHistoriales()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorDataBreaches.ObtenerTodas();
        }
        public int AltaHistorial(Historial historial)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorDataBreaches.Alta(historial);
        }
        public Historial BuscarHistorial(int historial)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorDataBreaches.Buscar(historial);
        }
        public IEnumerable<Grupo> GenerarGrupos()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorContrasenia.GenerarGrupos();
        }
        public string VerificarFortalezaPassword(string password)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorContrasenia.VerificarFortalezaPassword(password);
        }
        public int VerificarCatidadVecesPasswordRepetido(string password)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            return gestorContrasenia.VerificarCantidadVecesPasswordRepetido(password);
        }
        public int VerificarPasswordFiltrado(string password)
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            List<IFuente> fuentes = gestorDataBreaches.ObtenerFuentes();
            return gestorContrasenia.VerificarPasswordFiltrado(password, fuentes);
        }
        public bool VerificarUsuarioExiste()
        {
            return usuario.VerificarUsuarioExiste();
        }
        /* 
         * Método que se realiza para limpiar los datos de las pruebas unitarias
         * de sesión, debido a que la sesión es Singleton, no se limpian los datos
         * vacindo las listas.
         */
        public void VaciarDatosPrueba()
        {
            this.gestorContrasenia.LimpiarBD();
            this.gestorCategoria.LimpiarBD();
            this.gestorTarjetaCredito.LimpiarBD();
            this.gestorDataBreaches.LimpiarBD();
            this.gestorDataBreaches.LimpiarFuentes();
            this.usuario.LimpiarBD();
        }
        public void LimpiarFuentes()
        {
            if (!this.logueado) throw new ExcepcionAccesoDenegado(MENSAJE_ERROR_NO_LOGUEADO);
            gestorDataBreaches.LimpiarFuentes();
        }
    }
}
