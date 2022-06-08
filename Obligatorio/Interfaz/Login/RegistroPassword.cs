﻿using Interfaz.Alertas;
using Negocio.Excepciones;
using Negocio.InterfacesGUI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Interfaz.Login
{
    public partial class RegistroPassword : Form
    {
        private IUsuario sesion = new UsuarioGUI();
        public RegistroPassword()
        {
            InitializeComponent();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtRepetirPassword.PasswordChar == "•".ToCharArray()[0])
            {
                GuardarPassword();
            }
            else
            {
                Alerta("Debe rellenar los campos primero.", AlertaToast.enmTipo.Error);
            }
        }
        private void GuardarPassword()
        {
            string passwordInicial = this.txtPassword.Text;
            string passwordRepetido = this.txtRepetirPassword.Text;
            try
            {
                if (passwordInicial != passwordRepetido)
                {
                    Alerta("Los passwords deben coincidir.", AlertaToast.enmTipo.Error);
                    return;
                }
                sesion.GuardarPrimerPassword(passwordRepetido);
                NavegarALogin();
            }
            catch (ExcepcionLargoTexto errorLargoTexto)
            {
                Alerta(errorLargoTexto.Message, AlertaToast.enmTipo.Error);
            }
        }
        private void lblVolver_Click(object sender, EventArgs e)
        {
            NavegarALogin();
        }
        private void NavegarALogin()
        {
            InicioSesion inicioSesion = new InicioSesion();
            this.Hide();
            inicioSesion.Show();
        }
        private void Alerta(string mensaje, AlertaToast.enmTipo tipo)
        {
            AlertaToast alerta = new AlertaToast();
            alerta.MostrarAlerta(mensaje, tipo);
        }

        #region Comportamiento Visual
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRepetirPassword.Focus();
            }
        }
        private void txtRepetirPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegistrar.Focus();
            }else if (e.KeyCode == Keys.Tab)
            {
                NavegarALogin();
            }
        }
        private void txtPassword_MouseHover(object sender, EventArgs e)
        {
            ResaltarColorPassword();
        }
        private void txtRepetirPassword_MouseHover(object sender, EventArgs e)
        {
            ResaltarColorRepetir();
        }
        private void txtPassword_MouseLeave(object sender, EventArgs e)
        {
            VolverColorPassword();
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            VolverColorPassword();
        }
        private void txtRepetirPassword_Leave(object sender, EventArgs e)
        {
            VolverColorRepetir();
        }
        private void txtRepetirPassword_MouseLeave(object sender, EventArgs e)
        {
            VolverColorRepetir();
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            ResaltarColorPassword();
        }
        private void txtRepetirPassword_TextChanged(object sender, EventArgs e)
        {
            ResaltarColorRepetir();
        }
        private void ResaltarColorPassword()
        {
            VolverColorRepetir();
            txtPassword.ForeColor = Color.WhiteSmoke;
            icoPassword.IconColor = Color.WhiteSmoke;
            pnlLinea.BackColor = Color.WhiteSmoke;
        }
        private void ResaltarColorRepetir()
        {
            VolverColorPassword();
            txtRepetirPassword.ForeColor = Color.WhiteSmoke;
            icoRepetirPassword.IconColor = Color.WhiteSmoke;
            pnlRepetirPassword.BackColor = Color.WhiteSmoke;
        }
        private void VolverColorPassword()
        {
            if (txtPassword.Text == "" && !txtPassword.Focused)
            {
                txtPassword.ForeColor = Color.DarkGray;
                icoPassword.IconColor = Color.DarkGray;
                pnlLinea.BackColor = Color.DarkGray;
                txtPassword.PasswordChar = default;
                txtPassword.Text = "Contraseña";
            }
            else if (!txtPassword.Focused)
            {
                txtPassword.ForeColor = Color.DarkGray;
                icoPassword.IconColor = Color.DarkGray;
                pnlLinea.BackColor = Color.DarkGray;
            }
        }
        private void VolverColorRepetir()
        {
            if (txtRepetirPassword.Text == "" && !txtRepetirPassword.Focused)
            {
                txtRepetirPassword.ForeColor = Color.DarkGray;
                icoRepetirPassword.IconColor = Color.DarkGray;
                pnlRepetirPassword.BackColor = Color.DarkGray;
                txtRepetirPassword.PasswordChar = default;
                txtRepetirPassword.Text = "Repetir Contraseña";
            }
            else if (!txtPassword.Focused)
            {
                txtRepetirPassword.ForeColor = Color.DarkGray;
                icoRepetirPassword.IconColor = Color.DarkGray;
                pnlRepetirPassword.BackColor = Color.DarkGray;
            }
        }
        private void icoPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        private void icoRepetirPassword_Click(object sender, EventArgs e)
        {
            txtRepetirPassword.Focus();
        }
        private void icoPassword_MouseHover(object sender, EventArgs e)
        {
            ResaltarColorPassword();
        }
        private void icoPassword_MouseLeave(object sender, EventArgs e)
        {
            VolverColorPassword();
        }
        private void icoRepetirPassword_MouseHover(object sender, EventArgs e)
        {
            ResaltarColorRepetir();
        }
        private void icoRepetirPassword_MouseLeave(object sender, EventArgs e)
        {
            VolverColorRepetir();
        }
        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar != "•".ToCharArray()[0])
            {
                txtPassword.Text = "";
                txtPassword.PasswordChar = "•".ToCharArray()[0];
            }
        }
        private void txtRepetirPassword_Click(object sender, EventArgs e)
        {
            if (txtRepetirPassword.PasswordChar != "•".ToCharArray()[0])
            {
                txtRepetirPassword.Text = "";
                txtRepetirPassword.PasswordChar = "•".ToCharArray()[0];
            }
        }
        private void txtRepetirPassword_Enter(object sender, EventArgs e)
        {
            VolverColorPassword();
            if (txtRepetirPassword.PasswordChar != "•".ToCharArray()[0])
            {
                txtRepetirPassword.Text = "";
                txtRepetirPassword.PasswordChar = "•".ToCharArray()[0];
            }
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            VolverColorRepetir();
            if (txtPassword.PasswordChar != "•".ToCharArray()[0])
            {
                txtPassword.Text = "";
                txtPassword.PasswordChar = "•".ToCharArray()[0];
            }
        }
        #endregion

    }
}
