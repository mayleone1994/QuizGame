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
    public partial class Modo2 : Form
    {
        string resposta;
        int acertos = 0, erradas = 0, bandeirasJogadas;
        Bitmap bandeiraCerta;
        List<Bitmap> Imagem = new List<Bitmap>();
        List<Bitmap> bandeiras = new List<Bitmap>();
        List<Bitmap> bandeirasCopia = new List<Bitmap>();
        List<string> perguntas = new List<string>();
        List<string> perguntasCopia = new List<string>();
        List<PictureBox> pbs = new List<PictureBox>();
        Random rdn = new Random();

        public Modo2()
        {
            InitializeComponent();
            Modo3.CalcularTotal();
            CarregarRecursos();
            IniciarPbs();
        }

        private void CarregarRecursos()
        {

            ResourceSet res = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry entrar in res)
            {

                string nome = entrar.Key.ToString();
                object tipo = entrar.Value;

                if (tipo is Bitmap)
                {

                    bandeiras.Add((Bitmap)tipo);
                    Imagem.Add((Bitmap)tipo);
                    RetirarUnder(ref nome);
                    perguntas.Add(nome);

                }

            }

            bandeirasCopia = bandeiras.ToList();
            perguntasCopia = perguntas.ToList();

        }

        private void RetirarUnder(ref string texto)
        {

            if (texto.Contains('_'))
            {

                texto = texto.Replace('_', ' ');
            }

        }

        private void IniciarPbs()
        {

            pbs.Clear();

            foreach (var pic in this.Controls.OfType<PictureBox>())
            {

                pic.Visible = true;
                pbs.Add(pic);
                pic.Tag = "NADA";

            }

            IniciarBitmaps();
        }

        private void IniciarBitmaps()
        {

            bandeiras.Clear();

            bandeiras = bandeirasCopia.ToList();

            SortearPergunta();
        }

        private void SortearPergunta()
        {
            bandeirasJogadas++;
            lblContador.Text = bandeirasJogadas.ToString() + "/" + Modo3.aoTodo.ToString();
            var sortear = rdn.Next(0, perguntas.Count);
            resposta = perguntas[sortear];
            perguntas.Remove(resposta);
            lblPergunta.Text = resposta;
            bandeiraCerta = Imagem[sortear];
            Imagem.Remove(bandeiraCerta);
            SortearAlternativas();

        }

        private void SortearAlternativas()
        {

            var sorteio = rdn.Next(0, 4);
            pbs[sorteio].Image = bandeiraCerta;
            pbs[sorteio].Tag = resposta;
            bandeiras.Remove(bandeiraCerta);

            foreach (PictureBox picture in this.Controls.OfType<PictureBox>())
            {

                if (picture.Tag.ToString() == "NADA")
                {
                    var sorteando = rdn.Next(0, bandeiras.Count);
                    picture.Image = bandeiras[sorteando];
                    bandeiras.Remove(bandeiras[sorteando]);
                }
            }

        }

        private void ConferirResposta(object sender, EventArgs e)
        {

            PictureBox pic = (PictureBox)sender;

            if (pic.Tag.ToString() == resposta)
            {

                Form1.sons[0].Play();
                acertos++;
                MessageBox.Show("Correto!");

            }
            else
            {
                foreach (var pb in this.Controls.OfType<PictureBox>()) {

                    if (pb.Tag.ToString() != resposta) {

                        pb.Visible = false;
                    }

                
                }
                Form1.sons[1].Play();
                erradas++;
                MessageBox.Show("Errado!!");

            }

            Terminar();

        }

        private void Terminar()
        {
            int contagem = 0;

            switch (Menu1.modoJogo)
            {

                case 0: contagem = 146; break;
                case 1: contagem = 106; break;
                case 2: contagem = 0; break;

            }

            if (perguntas.Count == contagem)
            {
                MessageBox.Show("Fim de jogo! Você acertou " + acertos.ToString() + " bandeiras e errou: " + erradas.ToString());
                DialogResult msg = MessageBox.Show("Deseja jogar de novo?", "Pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    bandeirasJogadas = 0;
                    lblContador.Text = bandeirasJogadas.ToString() + "/" + Modo3.aoTodo.ToString();
                    perguntas.Clear(); pbs.Clear(); Imagem.Clear();
                    Imagem = bandeirasCopia.ToList();
                    perguntas = perguntasCopia.ToList();
                    acertos = 0; erradas = 0;
                    IniciarPbs();

                }
                else
                {

                    Application.Exit();

                }

            }
            else
            {

                IniciarPbs();

            }

        }

        private void Modo2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

