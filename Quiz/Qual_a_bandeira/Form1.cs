using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string resposta, nda = "Nenhuma das alternativas";
        int pontos = 0, erradas = 0, bandeirasJogadas;
        bool podeJogar = true;
        List<Bitmap> imagens = new List<Bitmap>();
        List<Bitmap> Copiaimagens = new List<Bitmap>();
        List<string> nomes = new List<string>();
        List<string> alternativas = new List<string>();
        List<string> CopiaAlternativas = new List<string>();
        List<RadioButton> marcadores = new List<RadioButton>();
        ResourceSet rec = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        Random rdn = new Random();
        public static SystemSound[] sons = { SystemSounds.Asterisk, SystemSounds.Hand };

        public Form1()
        {
            InitializeComponent();
            Modo3.CalcularTotal();
            GuardarReferencias();
            SortearImagem();
        }

        private void GuardarReferencias() {

            foreach (DictionaryEntry entry in rec) {

                String nome = entry.Key.ToString();
                object tipo = entry.Value;

                if (tipo is Bitmap) {

                    
                        imagens.Add((Bitmap)tipo);
                        RetirarChar(ref nome);
                        nomes.Add(nome);
                    }
            
            }

            CopiaAlternativas = nomes.ToList();
            Copiaimagens = imagens.ToList();
            RefazerAlternativas();
        
        }

        private void RefazerAlternativas() {

            alternativas.Clear();

            alternativas = CopiaAlternativas.ToList();

            alternativas.Add(nda);

            Radios();
        
        }

        private void Radios() {

            marcadores.Clear();

            foreach (var radio in this.Controls.OfType<RadioButton>()) {

                marcadores.Add(radio);
            
            }
        
        }

        private void SortearImagem() {

            var sortearIndice = rdn.Next(0, imagens.Count);
            var bandeira = imagens[sortearIndice];
            resposta = nomes[sortearIndice];
            pictureBox1.Image = bandeira;

            imagens.Remove(bandeira);
            nomes.Remove(resposta);
            alternativas.Remove(resposta);
            SortearAlternativas();

        }

        private void SortearAlternativas() {

            bandeirasJogadas++;
            lblContador.Text = bandeirasJogadas.ToString() + "/" + Modo3.aoTodo.ToString();
            var Sorteio = rdn.Next(0, 5);
            marcadores[Sorteio].Text = resposta;
            marcadores.Remove(marcadores[Sorteio]);

            foreach (var rb in marcadores) {

                var Sorteios = rdn.Next(0, alternativas.Count);

                    rb.Text = alternativas[Sorteios];
                    alternativas.Remove(rb.Text);
            
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (podeJogar)
            {
                foreach (var rb in this.Controls.OfType<RadioButton>())
                {

                    if (rb.Checked)
                    {

                        if (rb.Text == resposta)
                        {
                            sons[0].Play();
                            pontos++;
                            MessageBox.Show("Correto!");
                            break;

                        }
                        else
                        {
                            sons[1].Play();
                            erradas++;
                            MessageBox.Show(" Errado! \n A resposta correta é:  " + resposta.ToUpper());
                            break;

                        }
                    }
                }
            }

                Acabou();
        }

        private void Acabou() {

            
            int contagem = 0;

            switch (Menu1.modoJogo) {

                case 0: contagem = 146; break;
                case 1: contagem = 106; break;
                case 2: contagem = 0; break;
            
            }
            

            if (imagens.Count == contagem)
            {
                podeJogar = false;

                MessageBox.Show(" Fim de jogo! \n Você acertou " + pontos.ToString() + " bandeiras e errou " + erradas.ToString());
                

                Perguntar();
            }

            else {

                RefazerAlternativas();
                SortearImagem();
            
            }
        
        }

        private void Perguntar() {

            DialogResult msg = MessageBox.Show("Deseja jogar de novo?", "Pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes) {

                    bandeirasJogadas = 0;
                    lblContador.Text = bandeirasJogadas.ToString() + "/" + Modo3.aoTodo.ToString();
                    nomes.Clear(); imagens.Clear();
                    erradas = 0; pontos = 0; lblContador.Text = "Pontos:";
                    imagens = Copiaimagens.ToList();
                    nomes = CopiaAlternativas.ToList();
                    RefazerAlternativas();
                    SortearImagem();
                    podeJogar = true;

                } else {
                
                    Application.Exit();
                
                }
        
        }

        private void RetirarChar(ref string text) {


            if (text.Contains('_')){
            text = text.Replace('_', ' ');
            
            }
            
    }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
        }
    }

