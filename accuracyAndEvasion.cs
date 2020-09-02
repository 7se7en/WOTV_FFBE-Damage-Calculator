using System;
using System.Windows.Forms;

namespace WOTV_FFBE
{
    public partial class accuracyAndEvasion : Form
    {
        public accuracyAndEvasion(string dexInput, string luckInput)
        {
            InitializeComponent();
            textBox1.Text = dexInput;
            textBox2.Text = luckInput;
        }

        private void accuracyAndEvasion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox1.Text, out double yourDEX);
            double.TryParse(textBox2.Text, out double yourLUCK);
            double.TryParse(textBox4.Text, out double yourAccuracy);

            double.TryParse(textBox5.Text, out double theirLUCK);
            double.TryParse(textBox6.Text, out double theirAGI);
            double.TryParse(textBox7.Text, out double theirEvasion);

            double finalAccuracy = Math.Truncate(yourDEX / 4) + Math.Truncate(yourLUCK / 2.5) + yourAccuracy;
            double finalEvasion = Math.Truncate(theirLUCK / 2.5) + Math.Truncate(theirAGI / 2) + theirEvasion;

            if(finalAccuracy - finalEvasion <= 0)
            {
                finalResult.Text = "0%";
            } else
            {
                finalResult.Text = (finalAccuracy - finalEvasion).ToString() + "%";
            }            
        }
    }
}
