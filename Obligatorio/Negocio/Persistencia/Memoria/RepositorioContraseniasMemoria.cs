﻿using Negocio.Contrasenias;
using Negocio.Excepciones;
using System;
using System.Collections.Generic;

namespace Negocio.Persistencia.Memoria
{
    public class RepositorioContraseniasMemoria : IRepositorio<Contrasenia>
    {
        private static int autonumerado = 1;
        private List<Contrasenia> contrasenias;
                
        public RepositorioContraseniasMemoria()
        {
            this.contrasenias = new List<Contrasenia>();
        }

        public int Alta(Contrasenia unaContrasenia)
        {
            Existe(unaContrasenia);
            unaContrasenia.ContraseniaId = autonumerado;
            unaContrasenia.FechaUltimaModificacion = DateTime.Now;
            this.contrasenias.Add(unaContrasenia);
            autonumerado++;
            return unaContrasenia.ContraseniaId;
        }

        public void Baja(Contrasenia eliminar)
        {
            contrasenias.Remove(Buscar(eliminar));
        }

        public void Modificar(Contrasenia modificarContrasenia)
        {
            Contrasenia anterior = Buscar(modificarContrasenia);
            anterior.Sitio = modificarContrasenia.Sitio;
            anterior.Usuario = modificarContrasenia.Usuario;
                        
            if (!anterior.Password.Equals(modificarContrasenia.Password))
            {
                anterior.Password = modificarContrasenia.Password;
                anterior.FechaUltimaModificacion = DateTime.Now;
            }
            
            anterior.Categoria = modificarContrasenia.Categoria;
            anterior.Notas = modificarContrasenia.Notas;
                        
        }

        public Contrasenia Buscar(Contrasenia buscada)
        {
            foreach (Contrasenia item in contrasenias)
                if (item.ContraseniaId == buscada.ContraseniaId) return item;
            throw new ExcepcionElementoNoExiste("La contraseña buscada no existe.");
        }

        public IEnumerable<Contrasenia> ObtenerTodas()
        {
            this.contrasenias.Sort();
            return this.contrasenias;
        }
   
        public void Existe(Contrasenia unaContrasenia)
        {
            foreach (var contrasenia in this.contrasenias)
            {
                if (contrasenia.Sitio.ToUpper().Equals(unaContrasenia.Sitio.ToUpper()) &&
                    contrasenia.Usuario.ToUpper().Equals(unaContrasenia.Usuario.ToUpper()))
                    throw new ExcepcionElementoYaExiste("La contraseña buscada ya existe.");
            }
        }

        public void TestClear()
        {
            this.contrasenias = new List<Contrasenia>();
        }
    }
}