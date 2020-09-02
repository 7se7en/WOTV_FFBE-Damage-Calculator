using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOTV_FFBE
{
    public partial class criticalChanceCalculator : Form
    {
        public criticalChanceCalculator(string dex)
        {
            InitializeComponent();
            textBox1.Text = dex;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox1.Text, out double criticalHitVal);
            double.TryParse(textBox2.Text, out double criticalAvoidanceVal);
            double.TryParse(textBox3.Text, out double criticalBonusVal);
            double.TryParse(textBox4.Text, out double criticalEvadeVal);

            double criticalHit = Math.Pow(criticalHitVal, 0.35) / 4 - 1;
            criticalHit *= 100;
            double criticalAvoidance = Math.Pow(criticalAvoidanceVal, 0.37) / 5 - 1;
            criticalAvoidance *= 100;
            result.Text = Math.Truncate((criticalHit+criticalBonusVal) - (criticalAvoidance+criticalEvadeVal)).ToString() + "%";
        }

        private void criticalChanceCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
