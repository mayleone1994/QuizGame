using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Menu1 : Form
    {
        public static byte modoJogo;

        public Menu1()
        {
            InitializeComponent();
        }

        private void Botoes(object sender, EventArgs e) {

            Button btn = (Button)sender;

            foreach (RadioButton radio in this.Controls.OfType<RadioButton>()) {

                if (radio.Checked) {

                    switch (radio.Name)
                    {

                        case "A": modoJogo = 0; break;
                        case "B": modoJogo = 1; break;
                        case "C": modoJogo = 2; break;

                    }
                
                }
            
            }

            MessageBox.Show("Boa sorte no jogo! Aguarde uns segundos para que ele carregue por completo!");

            if (btn.TabIndex == 1) {

                Form1 form = new Form1();
                form.Show();
                this.Enabled = false;
                this.Visible = false;

            }
            else if (btn.TabIndex == 2)
            {

                Modo2 form = new Modo2();
                form.Show();
                this.Enabled = false;
                this.Visible = false;

            }

            else {

                Modo3 form = new Modo3();
                form.Show();
                this.Enabled = false;
                this.Visible = false;
            }
        
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
    }
}
