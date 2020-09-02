using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOTV_FFBE
{
    public partial class debuffSuccessChance : Form
    {
        public debuffSuccessChance()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox1.Text, out double successChance);
            double.TryParse(textBox2.Text, out double enemyResistance);
            double.TryParse(textBox3.Text, out double yourFTH);
            double.TryParse(textBox4.Text, out double enemyFTH);

            successChance = (successChance - enemyResistance) / 100;
            if(successChance <= 0) { finalResult.Text = "0%"; return; }
            successChance *= yourFTH + enemyFTH;
            finalResult.Text = Math.Truncate(successChance) + "%";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "30";
            textBox4.Text = "30";
            button1_Click(button2, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "30";
            textBox4.Text = "97";
            button1_Click(button3, EventArgs.Empty);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "97";
            textBox4.Text = "97";
            button1_Click(button4, EventArgs.Empty);
        }

        private void debuffSuccessChance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
