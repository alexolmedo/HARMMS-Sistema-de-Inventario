﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Cliente
{
    public partial class ModificarCliente : Form
    {
        Conexion conexion = new Conexion();
        SqlDataAdapter da;
        DataTable dt;
        public ModificarCliente()
        {
            InitializeComponent();
            this.CenterToScreen();

            //Llenar los datos para autocompletar la búsqueda por cedula
            string strquery1 = "Select ci_cliente from cliente";
            conexion.command = new SqlCommand(strquery1, conexion.connection);

            da = new SqlDataAdapter();
            //fetching query in the database.
            da.SelectCommand = conexion.command;
            //inicializar nueva datatable
            dt = new DataTable();
            //refresca las filas segun el rango especificado en el datasource. 
            da.Fill(dt);

            txtCedula.AutoCompleteCustomSource.Clear();
            foreach (DataRow r in dt.Rows)
            {
                //obtiene todas las filas de una columna
                var rw = r.Field<string>("ci_cliente");

                //Set the properties of a textbox to make it auto suggest and append.
                txtCedula.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCedula.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //adding all rows into the textbox
                txtCedula.AutoCompleteCustomSource.Add(rw);
            }

            //Llenar los datos para autocompletar la búsqueda por nombre
            string strquery2 = "Select nombrecliente from cliente";
            conexion.command = new SqlCommand(strquery2, conexion.connection);

            da = new SqlDataAdapter();
            //fetching query in the database.
            da.SelectCommand = conexion.command;
            //inicializar nueva datatable
            dt = new DataTable();
            //refresca las filas segun el rango especificado en el datasource. 
            da.Fill(dt);

            txtNombre.AutoCompleteCustomSource.Clear();
            foreach (DataRow r in dt.Rows)
            {
                //obtiene todas las filas de una columna
                var rw = r.Field<string>("nombrecliente");

                //Set the properties of a textbox to make it auto suggest and append.
                txtNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //adding all rows into the textbox
                txtNombre.AutoCompleteCustomSource.Add(rw);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Confirma modificación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                
            if (dr == DialogResult.Yes)
            {
                Console.WriteLine(dirActCliente);
                string sql = "Update cliente set CI_Cliente = '" + cedActCliente.Text + "', NombreCliente ='" + nomActCliente.Text + "', TelefonoCliente='" + telActCliente.Text + 
                    "',DireccionCliente='" + dirActCliente.Text + "',RUCCliente='" + RUCActCliente.Text + "',correoCliente='" + correoActCliente.Text + "',EstadoCliente ='" + comboBoxEstC.SelectedItem + "' where CI_Cliente = '"+ cedActCliente.Text +"'";
                Console.WriteLine(sql);
                   
                conexion.command = new SqlCommand(sql, conexion.connection);
                conexion.command.ExecuteNonQuery();
                conexion.command.Dispose();
                MessageBox.Show("El cliente se modificó correctamente", "Cliente Modificado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cedAgrCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void telAgrCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void RUCAgrCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtNombre.ReadOnly = false;
            txtCedula.Text = "";
            txtCedula.ReadOnly = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtNombre.ReadOnly = true;
            txtNombre.Text = "";
            txtCedula.ReadOnly = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string strquery3 = "";

                if (radioButNombre.Checked)
                {
                    strquery3 = "Select * from cliente where NombreCliente = '" + txtNombre.Text + "'";
                }

                if (radioButCed.Checked)
                {
                    strquery3 = "Select * from cliente where CI_Cliente = " + txtCedula.Text + "";
                }

                conexion.command = new SqlCommand(strquery3, conexion.connection);

                da = new SqlDataAdapter();
                //fetching query in the database.
                da.SelectCommand = conexion.command;
                //inicializar nueva datatable
                dt = new DataTable();
                //refresca las filas segun el rango especificado en el datasource. 
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    //obtiene todas las filas de una columna
                    cedActCliente.Text = r[0].ToString();
                    nomActCliente.Text = r[1].ToString();
                    telActCliente.Text = r[2].ToString();
                    dirActCliente.Text = r[3].ToString();
                    RUCActCliente.Text = r[4].ToString();
                    correoActCliente.Text = r[5].ToString();
                    comboBoxEstC.SelectedItem = r[6];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
 
        }

        private void btnCerrarAgrCliente_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dirActCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar=='-'|| e.KeyChar == '.'|| e.KeyChar == ',')
            {
                 e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void correoActCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '-' || e.KeyChar == '.' || e.KeyChar == '@' || e.KeyChar == '_')
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}