using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Calculadora;Trusted_Connection=True;";

        string expresion = "";
        double resultado = 0;
        bool nuevaOperacion = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void AgregarNumero(string numero)
        {
            if (nuevaOperacion)
            {
                textBox1.Text = "";
                nuevaOperacion = false;
            }
            textBox1.Text += numero;
            expresion += numero;
        }

        private void AgregarOperacion(string op)
        {
            textBox1.Text += " " + op + " ";
            expresion += op;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCuadrado_Click(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(textBox1.Text);
                resultado = num * num;
                textBox1.Text = resultado.ToString();
                GuardarOperacion($"{num}²", resultado);
                nuevaOperacion = true;
            }
            catch
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }

        private void btnRaiz_Click(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(textBox1.Text);
                resultado = Math.Sqrt(num);
                textBox1.Text = resultado.ToString();
                GuardarOperacion($"√{num}", resultado);
                nuevaOperacion = true;
            }
            catch
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }
        private void btnNegativo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (textBox1.Text.StartsWith("-"))
                    textBox1.Text = textBox1.Text.Substring(1);
                else
                    textBox1.Text = "-" + textBox1.Text;

                expresion = textBox1.Text;
            }
        }
        private void btnDivi_Click(object sender, EventArgs e)
        {
            AgregarOperacion("/");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            AgregarNumero("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            AgregarNumero("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            AgregarNumero("9");
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            AgregarOperacion("*");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            AgregarNumero("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            AgregarNumero("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            AgregarNumero("6");
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            AgregarOperacion("-");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            AgregarNumero("1");

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            AgregarNumero("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            AgregarNumero("3");
        }

        private void btnSuma_Click(object sender, EventArgs e)
        {
            AgregarOperacion("+");
        }


        private void btnBorrar_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            expresion = "";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            AgregarNumero("0");

        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            AgregarNumero(".");
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = new DataTable();
                resultado = Convert.ToDouble(dt.Compute(expresion, ""));
                textBox1.Text = resultado.ToString();
                GuardarOperacion(expresion, resultado);
                nuevaOperacion = true;
                expresion = "";
            }
            catch
            {
                MessageBox.Show("Error en la expresión.");
            }
        }

        private void GuardarOperacion(string exp, double res)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Operaciones (Expresion, Resultado) VALUES (@exp, @res)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@exp", exp);
                cmd.Parameters.AddWithValue("@res", res);
                cmd.ExecuteNonQuery();
            }
        }
    }
}