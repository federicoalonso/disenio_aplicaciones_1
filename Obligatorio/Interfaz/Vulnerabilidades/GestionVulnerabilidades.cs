﻿using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Interfaz.Vulnerabilidades
{
    public partial class GestionVulnerabilidades : UserControl
    {
        private IconButton BotonSeleccionado;

        public GestionVulnerabilidades()
        {
            InitializeComponent();
        }

        private void btnResumenVulnerabilidades_Click(object sender, EventArgs e)
        {
            BotonActivo(sender, Color.FromArgb(95, 77, 221));
            pnlGestor.Controls.Clear();
            UserControl resumenVulnerabilidades = new ResumenVulnerabilidades();
            pnlGestor.Controls.Add(resumenVulnerabilidades);
        }

        private void btnModificarFuenteLocal_Click(object sender, EventArgs e)
        {
            BotonActivo(sender, Color.FromArgb(95, 77, 221));
            pnlGestor.Controls.Clear();
            UserControl fuenteLocalVulnerabilidades = new FuenteLocalVulnerabilidades();
            pnlGestor.Controls.Add(fuenteLocalVulnerabilidades);
        }
        private void btnCargarFuente_Click(object sender, EventArgs e)
        {
            BotonActivo(sender, Color.FromArgb(95, 77, 221));
            pnlGestor.Controls.Clear();
            UserControl cargarFuenteVulnerabilidades = new CargarFuenteVulnerabilidades();
            pnlGestor.Controls.Add(cargarFuenteVulnerabilidades);
        }
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            BotonActivo(sender, Color.FromArgb(95, 77, 221));
            pnlGestor.Controls.Clear();
            UserControl historialVulnerabilidades = new HistorialVulnerabilidades();
            pnlGestor.Controls.Add(historialVulnerabilidades);
        }
        private void BotonActivo(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                BotonInactivo();
                BotonSeleccionado = (IconButton)senderBtn;
                BotonSeleccionado.BackColor = Color.FromArgb(37, 36, 81);
                BotonSeleccionado.ForeColor = color;
                BotonSeleccionado.IconColor = color;
            }
        }
        private void BotonInactivo()
        {
            if (BotonSeleccionado != null)
            {
                BotonSeleccionado.BackColor = Color.FromArgb(31, 30, 68);
                BotonSeleccionado.ForeColor = Color.Gainsboro;
                BotonSeleccionado.IconColor = Color.Gainsboro;
            }
        }
    }
}
