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
    public partial class effectiveHP : Form
    {
        public effectiveHP()
        {
            InitializeComponent();
        }

        private void effectiveHP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
        }

        double turnIntoPercent(double input)
        {
            // Why am I doing it like this? So that you can look at this and ask that.
            double returnValue = (100 - input) / 100;
            return returnValue;
        }

        private void effectiveHP_KeyUp(object sender, KeyEventArgs e)
        {
            double.TryParse(textBox1.Text, out double DEForSPR);
            DEForSPR = turnIntoPercent(DEForSPR);
            double.TryParse(textBox2.Text, out double typeResistance);
            typeResistance = turnIntoPercent(typeResistance);
            double.TryParse(textBox3.Text, out double elementResistance);
            elementResistance = turnIntoPercent(elementResistance);
            double.TryParse(textBox4.Text, out double singleAreaResistance);
            singleAreaResistance = turnIntoPercent(singleAreaResistance);
            double.TryParse(textBox5.Text, out double protectShell);
            protectShell = turnIntoPercent(protectShell);
            double.TryParse(textBox6.Text, out double HP);

            double value = 1;
            value *= DEForSPR;
            label7.Text = ((1 - value) * 100).ToString();
            value *= typeResistance;
            label8.Text = ((1 - value) * 100).ToString();
            value *= elementResistance;
            label9.Text = ((1 - value) * 100).ToString();
            value *= singleAreaResistance;
            label10.Text = ((1 - value) * 100).ToString();
            value *= protectShell;
            label11.Text = ((1 - value) * 100).ToString();

            value = Math.Round(HP / value);

            label13.Text = String.Format("{0:n0}", value);

            if (textBox1.Text == "") { label7.Visible = false; } else { label7.Visible = true; }
            if (textBox2.Text == "") { label8.Visible = false; } else { label8.Visible = true; }
            if (textBox3.Text == "") { label9.Visible = false; } else { label9.Visible = true; }
            if (textBox4.Text == "") { label10.Visible = false; } else { label10.Visible = true; }
            if (textBox5.Text == "") { label11.Visible = false; } else { label11.Visible = true; }

            foreach(Control c in this.Controls){
                if( c is TextBox ){
                    TextBox textBox = c as TextBox;
                    if (c.Name != "textBox6")
                    {
                        double.TryParse(textBox.Text, out double output);
                        if (output >= 100)
                        {
                            label13.Text = "1 x " + String.Format("{0:n0}", HP) + " times";
                        }
                    }
                    
                }
            }
        }
    }
}
