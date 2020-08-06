using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JOGO_DA_FORCA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //BOTÃO CADASTRAR
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToUpper();
            textBox2.Text = textBox2.Text.ToUpper();
            Int32 qt_atual_palavras;
            qt_atual_palavras = Convert.ToInt32(
                System.IO.File.ReadAllText(@"" + Form1.endereço + "total.txt"));
            qt_atual_palavras++;
            System.IO.File.WriteAllText(@"" + Form1.endereço + Convert.ToString(
                qt_atual_palavras) + ".txt", textBox1.Text);
            System.IO.File.WriteAllText(@"" + Form1.endereço + Convert.ToString(
                qt_atual_palavras) + "_1.txt", textBox2.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            System.IO.File.WriteAllText(@"" + Form1.endereço + "total.txt", 
                Convert.ToString(qt_atual_palavras));
        }

        //BOTÃO VOLTAR
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show("UTILIZE APENAS LETRAS MAIÚSCULAS");
        }
    }
}
