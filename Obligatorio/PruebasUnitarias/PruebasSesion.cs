﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negocio;
using Negocio.Categorias;
using Negocio.Contrasenias;
using Negocio.Excepciones;
using Negocio.TarjetaCreditos;
using System;
using System.Collections.Generic;
using System.Linq;
using Negocio.DataBreaches;

namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasSesion
    {
        private Sesion sesionPrueba;
        private TarjetaCredito nuevoTarjeta;
        private Contrasenia pruebaContrasenia;


        [TestInitialize]
        public void InicializarPruebas()
        {
            
            sesionPrueba = Sesion.ObtenerInstancia();
            
            sesionPrueba.GuardarPrimerPassword("secreto");
            sesionPrueba.Login("secreto");
            int id = sesionPrueba.AltaCategoria("Cosas");
            Categoria nuevaCategoriaPrueba = sesionPrueba.BuscarCategoriaPorId(id);

            this.nuevoTarjeta = new TarjetaCredito()
            {
                Categoria = nuevaCategoriaPrueba,
                Nombre = "PruebaNombre",
                Tipo = "PruebaTipo",
                Numero = "1234123412341234",
                Codigo = "123",
                Vencimiento = DateTime.Now,
                Nota = "Nota Opcional",
                
            };
           
            this.pruebaContrasenia = new Contrasenia()
            {
                Sitio = "deremate.com",
                Usuario = "fedex",
                Password = new Password("dalevo111!!!"),
                Categoria = nuevaCategoriaPrueba,
                Notas = "Sin"
            };

            sesionPrueba.AltaTarjetaCredito(nuevoTarjeta);
            sesionPrueba.AltaContrasenia(pruebaContrasenia);
            FuenteLocal fuente = new FuenteLocal();
            sesionPrueba.CargarDataBreach(fuente, "dalevo111!!!\n1234123412341234");
        }


        [TestCleanup]
        public void LimpiarPruebas()
        {
            sesionPrueba.VaciarDatosPrueba();            
        }


        


        [TestMethod]
        public void LaSesionMeDevuelveSiElPasswordSeFiltro()
        {
          
            int filtrada = 0;
            string password = "dalevo111!!!";
            filtrada = sesionPrueba.VerificarPasswordFiltrado(password);
            Assert.IsTrue(filtrada > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeCambiarElPasswordSinEstarLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.CambiarPassword("cambio password");
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeVerificarContraseniasVulnerablesSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.ContraseniasVulnerables();
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeVerificarTarjetasVulnerablesSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.TarjetasCreditoVulnerables();
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeCambiarContextoBDSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.CambiarContextoDeBaseDeDatos("");
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeCargarDataBreachSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            FuenteLocal fuente = new FuenteLocal();
            sesionPrueba.CargarDataBreach(fuente, "");
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarBuscarCategoriaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.BuscarCategoriaPorId(1);
        }
        [TestMethod]
        public void SePuedeEjecutarBuscarCategoriaPorIdSiSeEstaLogueado()
        {
            int nuevaCategoria = sesionPrueba.AltaCategoria("viejaCategoria");
            Categoria categoriaBuscada = sesionPrueba.BuscarCategoriaPorId(nuevaCategoria);
            Assert.IsNotNull(categoriaBuscada);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarAltaTarjetaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.AltaTarjetaCredito(new TarjetaCredito());
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarBajaTarjetaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.BajaTarjetaCredito(new TarjetaCredito().Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarModificarTarjetaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.ModificarTarjeta(new TarjetaCredito());
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarBuscarTarjetaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.BuscarTarjeta(new TarjetaCredito().Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarObtenerTodasTarjetaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.ObtenerTodasLasTarjetas();
        }


        
        [TestMethod]
        public void SePuedeEjecutarBuscarTarjetaEstandoLogueado()
        {
            int id = sesionPrueba.AltaCategoria("fake");
            Categoria nuevaCat = sesionPrueba.BuscarCategoriaPorId(id);

            TarjetaCredito buscada = new TarjetaCredito()
            {
                Categoria = nuevaCat,
                Nombre = "tarjeta a Buscar",
                Tipo = "PruebaTipo2",
                Numero = "5555123412344444",
                Codigo = "123",
                Vencimiento = DateTime.Now,
                Nota = "Nota Opcional",

            };

            int idTarjeta = sesionPrueba.AltaTarjetaCredito(buscada);
            TarjetaCredito encontrada = sesionPrueba.BuscarTarjeta(idTarjeta);
            Assert.AreEqual(idTarjeta, encontrada.Id);

        }



        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarAltaContraseniaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.AltaContrasenia(new Contrasenia());
        }

        

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarModificarContraseniaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.ModificarContrasenia(new Contrasenia());
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarBuscarContraseniaSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.BuscarContrasenia(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSePuedeEjecutarListarContraseniasSiNoSeEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.ObtenerTodasLasContrasenias();
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoPuedeEjecutarMostrarPasswordNoEstandoLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.MostrarPassword(new Contrasenia());

        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionLargoTexto))]
        public void NoSePuedeCambiarElPasswordMaestroMayorA25Caracteres()
        {
            sesionPrueba.CambiarPassword("12345123451234512345123451");
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionLargoTexto))]
        public void NoSePuedeCambiarElPasswordMaestroMenorA5Caracteres()
        {
            sesionPrueba.CambiarPassword("aaaa");
        }
        [TestMethod]
        public void SePuedeModificarElPassword()
        {
            Contrasenia aModificar = sesionPrueba.ObtenerTodasLasContrasenias().First();
            int idPass = aModificar.ContraseniaId;
            string passAnterior = sesionPrueba.MostrarPassword(aModificar);
            aModificar.Password = new Password("secretoNuevo");
            sesionPrueba.ModificarContrasenia(aModificar);

            Contrasenia modificada = sesionPrueba.BuscarContrasenia(idPass);
            string passActual = sesionPrueba.MostrarPassword(aModificar);

            Assert.AreNotEqual(passAnterior, passActual);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionElementoNoExiste))]
        public void NoSePuedeAgregarUnaContraseniaSiNoExisteLaCategoria()
        {
            Contrasenia nueva = new Contrasenia()
            {
                Categoria = new Categoria("Nombre X")
                {
                    Id = 50
                },
                Password = new Password("passs"),
                Sitio = "un sitio X",
                Usuario= "un usuario X"
            };
            sesionPrueba.AltaContrasenia(nueva);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionElementoNoExiste))]
        public void NoSePuedeAgregarUnaTarjetaSiNoExisteLaCategoria()
        {
            TarjetaCredito nueva = new TarjetaCredito()
            {
                Categoria = new Categoria("Nombre X")
                {
                    Id = 50
                },
                Codigo="123",
                Numero="1231231231231231",
                Nombre="tarjeta x",
                Tipo="Tarjeta",
                Vencimiento = DateTime.Now
            };
            sesionPrueba.AltaTarjetaCredito(nueva);
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void LaSesionNoDevuelveElColorDeUnPasswordSiNoEstaLogueado()
        {
            sesionPrueba.LogOut();
            string password = "HolaSecretoPassword";
            string fortaleza = sesionPrueba.VerificarFortalezaPassword(password);
        }
        

        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void NoSeGeneranGruposSinLoguearse()
        {
            sesionPrueba.LogOut();
            List<Grupo> grupos = sesionPrueba.GenerarGrupos().ToList();

        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void LaSesionNoDevuelveSiElPasswordEsRepetidoSiNoEstaLogueado()
        {
            sesionPrueba.LogOut();
            string password = "HolaSecretoPassword";
            sesionPrueba.VerificarCatidadVecesPasswordRepetido(password);
        }

    
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void LaSesionNoDevuelveSiElPasswordSeFiltroSiNoEstaLogueado()
        {
            sesionPrueba.LogOut();
            string password = "dalevo111!!!";
            sesionPrueba.VerificarPasswordFiltrado(password);
        }

        
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void LaSesionNoPermiteEliminarLosArchivosDataBreachSiNoEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.LimpiarFuentes();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ExcepcionAccesoDenegado))]
        public void LaSesionNoPermiteEliminarLaFuenteLocalSiNoEstaLogueado()
        {
            sesionPrueba.LogOut();
            sesionPrueba.LimpiarFuentes();
        }

        [TestMethod]
        public void SePuedeVerificarSiExisteUsuario()
        {
            Assert.IsTrue(sesionPrueba.VerificarUsuarioExiste());
        }
    }
}
