using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
SERÁ ADICIONADO: 
     *SOBRE
     *TELA DE CURIOSIDADE QUANDO O JOGADOR GANHA, MOSTRANDO TEXTO E IMAGEM SOBRE
     *A PALAVRA ACERTADA !!! 
*/

namespace JOGO_DA_FORCA
{
    public partial class Form1 : Form
    {
        public static string endereço, palavra_sorteada;
        Int32 total_palavras_cadastradas, número_de_letras, arquivo_sorteado;
        Int32 contador_de_erros = 0, contador_de_letras_certas = 0;
        char[] letras_acertadas = new char[50];
        Int32 sortear_arquivo()
        {
            Random palavra = new Random();
            return palavra.Next(1, total_palavras_cadastradas + 1);
        }

        public Form1()
        {
            InitializeComponent();
        }

        //JANELA PRINCIPAL DO JOGO
        private void Form1_Load(object sender, EventArgs e)
        {
            //NECESSÁRIO PARA TRANSFORMAR EM PALAVRA NO BOTÃO TENTAR
            Int32 c = 0;
            while (c < 50)
            {
                letras_acertadas[c] = Convert.ToChar("-");
                c++;
            }

            label3.Visible = true;
            label4.Visible = true;
            label3.Text = "";
            label4.Text = "";
            label9.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox1.Visible = false;
            
            //PROGRAMA APRENDE O ENDEREÇO
            try
            {
                endereço = System.IO.File.ReadAllText("endereço.txt");
            }
            catch
            {
                endereço = Microsoft.VisualBasic.Interaction.InputBox(
                    "ENDEREÇO DA PASTA COM PALAVRAS:" + 
                    Environment.NewLine + "(PODE DEIXAR EM BRANCO SE QUISER)");

                //SE O USUARIO DIGITAR O ENDEREÇO VAZIO
                if (endereço == "" || endereço == " ")
                    endereço = "palavras";

                endereço = endereço + "\\";

                //CRIA NOVA PASTA PARA RECEBER AS PALAVRAS CADASTRADAS
                System.IO.Directory.CreateDirectory(endereço);
                System.IO.File.WriteAllText("endereço.txt", endereço);
                button1.Enabled = false;
            }

            //PROGRAMA TENTA LER PALAVRAS CADASTRADAS
            try
            {
                total_palavras_cadastradas = Convert.ToInt32(
                System.IO.File.ReadAllText(endereço + "total.txt"));

                arquivo_sorteado = sortear_arquivo();

                //LE A PALAVRA DA FORCA
                palavra_sorteada = System.IO.File.ReadAllText(endereço +
                    Convert.ToString(arquivo_sorteado) + ".txt");

                //LE A DICA DA FORCA
                label1.Text = System.IO.File.ReadAllText(endereço +
                    Convert.ToString(arquivo_sorteado) + "_1.txt");
                label2.Text = "";
                número_de_letras = palavra_sorteada.Length;
                Int32 contador = 1;
                while (contador <= número_de_letras)
                {
                    label2.Text = label2.Text + "- ";
                    contador++;
                }
                label1.Visible = false;
                label2.Visible = false;
            }
            catch
            {
                endereço = @"palavras\";
                System.IO.Directory.CreateDirectory(endereço);
                System.IO.File.WriteAllText(endereço + "total.txt", "0");
            }
            finally
            {
                //MessageBox.Show("sei la");
                total_palavras_cadastradas = Convert.ToInt32(
                    System.IO.File.ReadAllText(@"" + endereço + "total.txt"));
                if (total_palavras_cadastradas == 0)
                {
                    MessageBox.Show("NÃO HÁ PALAVRAS CADASTRADAS");
                    button1.Enabled = false;
                    Form2 rForm2 = new Form2();
                    rForm2.Show();
                    rForm2.TopMost = true;
                }
                else
                {
                    button1.Enabled = true;
                    arquivo_sorteado = sortear_arquivo();

                    //MessageBox.Show(Convert.ToString(arquivo_sorteado));
                    //MessageBox.Show(Convert.ToString(total_palavras_cadastradas));
                    //MessageBox.Show(Convert.ToString(endereço + "total.txt"));
                    // MessageBox.Show(System.IO.File.ReadAllText(@"" + endereço + "total.txt"));

                    palavra_sorteada = System.IO.File.ReadAllText(endereço +
                        Convert.ToString(arquivo_sorteado) + ".txt");
                    label1.Text = System.IO.File.ReadAllText(endereço +
                        Convert.ToString(arquivo_sorteado) + "_1.txt");
                    label2.Text = "";
                    número_de_letras = palavra_sorteada.Length;
                    Int32 contador = 1;
                    while (contador <= número_de_letras)
                    {
                        label2.Text = label2.Text + "- ";
                        contador++;
                    }
                    label1.Visible = false;
                    label2.Visible = false;
                }
                    
            }           
            
        }

        //BOTÃO CADASTRAR
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 chamar = new Form2();
            chamar.ShowDialog();
            chamar = null;
            Show();
            Application.Restart();
        }

        //BOTÃO JOGAR
        private void button1_Click(object sender, EventArgs e)
        {            
            label9.Visible = true;
            button3.Visible = true;
            textBox1.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = true;
            label2.Visible = true;
            textBox1.Focus();
            MessageBox.Show("UTILIZE APENAS LETRAS MAIÚSCULAS");
        }

        //TENTAR
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToUpper();
            if (textBox1.Text != "" & textBox1.Text.Length == 1)
            {
                label2.Text = "";
                char[] letras = new char[50];
                Int32 contador = 0;
                bool estado_do_contador_de_erros = true;


                while (contador < número_de_letras)
                {
                    letras[contador] = palavra_sorteada[contador];
                    //MessageBox.Show(Convert.ToString(letras[contador]));
                    if (Convert.ToChar(textBox1.Text) == letras[contador])
                    {
                        letras_acertadas[contador] = letras[contador];
                        contador_de_letras_certas++;
                        estado_do_contador_de_erros = false;
                    }

                    //label2.Text = label2.Text + letras[contador] + " ";
                    //else
                    // letras_acertadas[contador] = Convert.ToChar("-");
                    if (letras_acertadas[contador] == Convert.ToChar("-"))
                    {
                        label2.Text = label2.Text + "- ";
                    }
                    else
                        label2.Text = label2.Text + Convert.ToString(letras_acertadas[contador]) + " ";
                    //MessageBox.Show(Convert.ToString(letras_acertadas[contador]));


                    contador++;
                }
                if (estado_do_contador_de_erros == true)
                {
                    contador_de_erros++;
                    label3.Visible = true;
                    label4.Visible = true;
                    label3.Text = "ERROS: " + Convert.ToString(contador_de_erros);
                    label4.Text = label4.Text + textBox1.Text + " ";
                }

                if (contador_de_erros == 3)
                {
                    MessageBox.Show("GAME OVER");
                    button3.Enabled = false;
                    button4.Visible = true;
                }

                if (contador_de_letras_certas == número_de_letras)
                {
                    MessageBox.Show("PARABÉNS");
                    button3.Enabled = false;
                    button4.Visible = true;
                }
                //MessageBox.Show(Convert.ToString(contador_de_letras_certas));

                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("DIGITE UMA LETRA");
                textBox1.Focus();
            }
        }

        //JOGAR NOVAMENTE
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
