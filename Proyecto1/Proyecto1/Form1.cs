using System;
using System.Data;
using System.Data.SqlClient;
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
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            textBox1.Text += " " + op + " ";
            expresion += op;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

       
        private void btn0_Click(object sender, EventArgs e) { AgregarNumero("0"); }
        private void btn1_Click(object sender, EventArgs e) { AgregarNumero("1"); }
        private void btn2_Click(object sender, EventArgs e) { AgregarNumero("2"); }
        private void btn3_Click(object sender, EventArgs e) { AgregarNumero("3"); }
        private void btn4_Click(object sender, EventArgs e) { AgregarNumero("4"); }
        private void btn5_Click(object sender, EventArgs e) { AgregarNumero("5"); }
        private void btn6_Click(object sender, EventArgs e) { AgregarNumero("6"); }
        private void btn7_Click(object sender, EventArgs e) { AgregarNumero("7"); }
        private void btn8_Click(object sender, EventArgs e) { AgregarNumero("8"); }
        private void btn9_Click(object sender, EventArgs e) { AgregarNumero("9"); }
        private void btnPunto_Click(object sender, EventArgs e) { AgregarNumero("."); }

    
        private void btnSuma_Click(object sender, EventArgs e) { AgregarOperacion("+"); }
        private void btnResta_Click(object sender, EventArgs e) { AgregarOperacion("-"); }
        private void btnMult_Click(object sender, EventArgs e) { AgregarOperacion("*"); }
        private void btnDivi_Click(object sender, EventArgs e) { AgregarOperacion("/"); }

       
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            expresion = "";
        }

      
        private void btnNegativo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            if (textBox1.Text.StartsWith("-"))
                textBox1.Text = textBox1.Text.Substring(1);
            else
                textBox1.Text = "-" + textBox1.Text;

            expresion = textBox1.Text;
        }

       
        private void btnCuadrado_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double num))
            {
                MessageBox.Show("Ingrese un número válido.");
                return;
            }

            resultado = num * num;
            textBox1.Text = resultado.ToString();
            GuardarOperacion($"{num}²", resultado, "²");
            nuevaOperacion = true;
        }

       
        private void btnRaiz_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double num))
            {
                MessageBox.Show("Ingrese un número válido.");
                return;
            }

            resultado = Math.Sqrt(num);
            textBox1.Text = resultado.ToString();
            GuardarOperacion($"√{num}", resultado, "√");
            nuevaOperacion = true;
        }

    
        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ingrese una expresión válida.");
                return;
            }

            string expr = textBox1.Text.Replace(" ", ""); 

            try
            {
                var dt = new DataTable();
                resultado = Convert.ToDouble(dt.Compute(expr, ""));
                textBox1.Text = resultado.ToString();

                string operacion = DetectarUltimaOperacion(expr);
                GuardarOperacion(expr, resultado, operacion);

                nuevaOperacion = true;
                expresion = resultado.ToString(); 
            }
            catch
            {
                MessageBox.Show("Error en la expresión. Solo se permiten operaciones básicas (+, -, *, /).");
            }
        }

       
        private int ObtenerTipoOperacion(string operacion)
        {
            switch (operacion)
            {
                case "+": return 1;
                case "-": return 2;
                case "*": return 3;
                case "/": return 4;
                case "√": return 5;
                case "²": return 6;
                default: return 1;
            }
        }

        private string DetectarUltimaOperacion(string expr)
        {
            string[] ops = { "+", "-", "*", "/" };
            foreach (string op in ops)
                if (expr.Contains(op))
                    return op;
            return "+"; 
        }

        
        private void GuardarOperacion(string exp, double res, string operacion)
        {
            int idTipo = ObtenerTipoOperacion(operacion);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Operaciones (Expresion, Resultado, IdTipo) VALUES (@exp, @res, @idTipo)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@exp", exp);
                cmd.Parameters.AddWithValue("@res", res);
                cmd.Parameters.AddWithValue("@idTipo", idTipo);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
