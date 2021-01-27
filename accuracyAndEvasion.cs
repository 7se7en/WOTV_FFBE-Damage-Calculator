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

            yourDEX = Math.Pow(yourDEX, 0.2);
            Console.WriteLine(yourDEX);
            yourDEX = (yourDEX * 11) / 20;
            Console.WriteLine(yourDEX);
            yourLUCK = Math.Pow(yourLUCK, 0.96);
            Console.WriteLine(yourLUCK);
            yourLUCK /= 200;
            Console.WriteLine(yourLUCK);
            double finalAccuracy = yourDEX + yourLUCK + yourAccuracy/100;
            Console.WriteLine(finalAccuracy);
            finalAccuracy -= 1;

            theirAGI = Math.Pow(theirAGI, 0.9);
            theirAGI = (theirAGI * 11) / 1000;
            theirLUCK = Math.Pow(theirLUCK, 0.96);
            theirLUCK /= 200;
            double finalEvasion = theirAGI + theirLUCK + theirEvasion/100;
            finalEvasion -= 1;

            double result = (finalAccuracy - finalEvasion) * 100;

            finalResult.Text = String.Format("{0:n0}%", result);
        }
    }
}
