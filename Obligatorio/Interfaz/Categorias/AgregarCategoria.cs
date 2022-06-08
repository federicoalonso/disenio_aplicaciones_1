﻿using Interfaz.Alertas;
using Negocio.Excepciones;
using Negocio.InterfacesGUI;
using System;
using System.Windows.Forms;

namespace Interfaz.Categorias
{
    public partial class AgregarCategoria : UserControl
    {
        private ICategoria Sesion = new CategoriaGUI();

        public AgregarCategoria()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = this.txtNombre.Text;
            try
            {
                this.Sesion.AltaCategoria(nombre);
                this.txtNombre.Clear();
                Alerta("Categoría creada con éxito!!", AlertaToast.enmTipo.Exito);
            }
            catch (ExcepcionElementoYaExiste unaExcepcion)
            {
                Alerta(unaExcepcion.Message, AlertaToast.enmTipo.Error);
                this.txtNombre.Focus();
            }
            catch (ExcepcionLargoTexto unaExcepcion)
            {
                Alerta(unaExcepcion.Message, AlertaToast.enmTipo.Error);
                this.txtNombre.Focus();
            }
        }

        private void Alerta(string mensaje, AlertaToast.enmTipo tipo)
        {
            AlertaToast alerta = new AlertaToast();
            alerta.MostrarAlerta(mensaje, tipo);
        }
    }
}
