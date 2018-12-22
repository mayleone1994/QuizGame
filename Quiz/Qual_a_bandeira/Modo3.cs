using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Modo3 : Form
    {
        public static int aoTodo, contagem;
        string resposta;
        int acertos = 0, erros = 0, bandeirasJogadas;
        List<Bitmap> bandeiras = new List<Bitmap>();
        List<Bitmap> bandeirasCopia = new List<Bitmap>();
        List<string> nomes = new List<string>();
        List<string> nomesCopia = new List<string>();
        Random rdn = new Random();

        public Modo3()
        {
            InitializeComponent();
            CalcularTotal();
            CarregarRecursos();
            Sortear();
        }

        public static void CalcularTotal(){

            contagem = 0;

            switch (Menu1.modoJogo)
            {

                case 0: contagem = 146; break;
                case 1: contagem = 106; break;
                case 2: contagem = 0; break;

            }

            aoTodo = 192 - contagem;
        
        }

        private void CarregarRecursos()
        {

            ResourceSet res = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry entrar in res)
            {

                string nome = entrar.Key.ToString();
                object tipo = entrar.Value;

                if (tipo is Bitmap && (nome != "Chade" || nome != "Romênia"))
                {

                    bandeiras.Add((Bitmap)tipo);
                    Retirar(ref nome);
                    nomes.Add(nome.ToLower());

                }

            }

            bandeirasCopia = bandeiras.ToList();
            nomesCopia = nomes.ToList();
            aoTodo -= 2;

        }

        private void Sortear() {

            bandeirasJogadas++;
            lblContador.Text = bandeirasJogadas.ToString() + "/" + aoTodo.ToString();
            var sorteio = rdn.Next(0, bandeiras.Count);
            pictureBox1.Image = bandeiras[sorteio];
            bandeiras.Remove(bandeiras[sorteio]);
            resposta = nomes[sorteio];
            nomes.Remove(resposta);
        }

        private void Checar(object sender, EventArgs e) {

            if (txtResposta.Text.ToLower() == resposta)
            {

                Form1.sons[0].Play();
                acertos++;
                txtResposta.BackColor = Color.DarkGreen;
                MessageBox.Show("Correto!");
                
            }
            else {
                Form1.sons[1].Play();
                erros++;
                txtResposta.BackColor = Color.Red;
                MessageBox.Show(" Errado! \n A resposta certa é: " + resposta.ToUpper());
             

            }

            Terminar();

        
        }

        private void Terminar()
        {

            if (bandeiras.Count == contagem)
            {
                txtResposta.Text = "";
                MessageBox.Show("Fim de jogo! Você acertou " + acertos.ToString() + " bandeiras e errou: " + erros.ToString());
                DialogResult msg = MessageBox.Show("Deseja jogar de novo?", "Pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    bandeirasJogadas = 0;
                    lblContador.Text = bandeirasJogadas.ToString() + "/" + aoTodo.ToString();
                    txtResposta.BackColor = Color.White;
                    bandeiras.Clear(); nomes.Clear();
                    acertos = 0; erros = 0;
                    bandeiras = bandeirasCopia.ToList();
                    nomes = nomesCopia.ToList();
                     Sortear();

                }
                else
                {

                    Application.Exit();

                }

            }
            else
            {
                txtResposta.BackColor = Color.White;
                txtResposta.Text = "";
                Sortear();
                

            }

        }

        private void Retirar(ref string texto) { 
        
            if (texto.Contains('_')){

                texto = texto.Replace('_', ' ');
            }
        
        }

        private void Modo3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
