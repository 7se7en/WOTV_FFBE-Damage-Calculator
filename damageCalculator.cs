using Google.Protobuf.WellKnownTypes;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WOTV_FFBE
{
    public partial class damageCalculator : Form
    {
        //453, 613
        public damageCalculator()
        {
            InitializeComponent();
            this.Text = "Damage Calculator";
            comboBox1.SelectedIndex = 0;
            elementAdvantage.SelectedIndex = 1;
            directoryMake();
            string notesLocation = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Saved Notes.txt";
            if (File.Exists(notesLocation)){
                using (var reader = new StreamReader(notesLocation)){
                    string line;
                    while ((line = reader.ReadLine()) != null){
                        this.Size = new Size(980, 616);
                        notesListBox.Items.Add(line.ToString());
                        notesNumericValues.Items.Add(reader.ReadLine().ToString());
                    }
                    reader.Close();
                }
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(x =>
            {
                if (x.Text == "0")
                {
                    x.Text = "";
                }
            });
            double varMAIN = 1, varDEX = 0, varAGI = 0, varLCK = 0;
            double.TryParse(mainStat.Text, out double mainStatVal);
            double.TryParse(dexStat.Text, out double dexVal);
            double.TryParse(agiStat.Text, out double agiVal);
            double.TryParse(luckStat.Text, out double luckVal);
            double.TryParse(skillMultiplierStat.Text, out double skillMultiplierVal);
            double.TryParse(damageTypeUpStat.Text, out double damageTypeUpVal);
            double.TryParse(elementTypeUpStat.Text, out double elementTypeUpVal);
            double.TryParse(totalKillerStat.Text, out double totalKillerVal);
            double.TryParse(criticalDamageUpStat.Text, out double criticalDamageUpVal);
            criticalDamageUpVal += 25;
            double.TryParse(damageTypeResistanceStat.Text, out double damageTypeResistanceVal);     // 50
            double.TryParse(flatTypePenStat.Text, out double flatTypePenVal);                       // 25
            damageTypeResistanceVal -= flatTypePenVal;                                              // 50 - 25 = 25
            double.TryParse(typePenetrationStat.Text, out double typePenetrationVal);               // 20
            typePenetrationVal = (100 - typePenetrationVal) / 100;                                  // ( 100 - 20 ) / 100 = 0.80 = 80%
            if (damageTypeResistanceVal >= 0)
            {
                damageTypeResistanceVal *= typePenetrationVal;                                      // 25 * 80% = 20
            }
            damageTypeResistanceVal = (100 - damageTypeResistanceVal) / 100;                        // ( 100 - 20 ) / 100 = 0.80 = 80% instead of 50%
            double.TryParse(elementTypeResistanceStat.Text, out double elementTypeResistanceVal);
            double.TryParse(flatElemPenStat.Text, out double flatElemPenVal);
            elementTypeResistanceVal -= flatElemPenVal;
            double.TryParse(elemPenetrationStat.Text, out double elemPenetrationVal);
            elemPenetrationVal = (100 - elemPenetrationVal) / 100;
            if (elementTypeResistanceVal >= 0)
            {
                elementTypeResistanceVal *= elemPenetrationVal;
            }
            elementTypeResistanceVal = (100 - elementTypeResistanceVal) / 100;
            double.TryParse(singleAreaResistanceStat.Text, out double singleAreaResistanceVal);
            singleAreaResistanceVal = (100 - singleAreaResistanceVal) / 100;
            double.TryParse(penetrationStat.Text, out double penetrationVal);
            penetrationVal = (100 - penetrationVal)/100;
            double.TryParse(DEForSPRStat.Text, out double DEForSPRval);
            //Don't know if Protect/Shell goes before or after penetration. Will put before for now.
            double.TryParse(protectShellStat.Text, out double protectShellVal);
            DEForSPRval += protectShellVal;
            //Also don't know if DEF debuffs go before percentage-based debuffs. Will put before for now as well!
            double.TryParse(flatPenStat.Text, out double flatPenVal);
            DEForSPRval -= flatPenVal;
            if(DEForSPRval >= 0)
            {
                DEForSPRval *= penetrationVal;
            }            
            DEForSPRval = (100 - DEForSPRval)/100;
            double.TryParse(BRVStat.Text, out double BRVval);
            BRVval += 50;
            if(BRVval == 50) { BRVval = 0; } // If the user left their BRV blank, do this. You can't do damage below 10 BRV anyway because chicken
            BRVval /= 100;
            double.TryParse(FTHStat.Text, out double FTHval);
            double.TryParse(EFTHStat.Text, out double EFTHval);
            double totalFTHval = (FTHval + EFTHval) / 100;
            double.TryParse(elementAdvantage.SelectedItem.ToString(), out double elementalAdvantageVal);
            elementalAdvantageVal /= 100;

            double.TryParse(nicheBox.Text, out double nicheVal);
            switch(comboBox1.SelectedItem)
            {
                case "Arithmetician": skillMultiplierVal += (nicheVal*5);
                    break;
            }

            switch (comboBox1.SelectedItem)
            {
                case "Assassin":
                case "Ninja":
                    varDEX = 0.2;
                    varAGI = 0.3;
                    break;
                case "Arithmetician":
                case "Black Mage":
                case "Cleric":
                case "Knight of Grandshelt":
                case "Red Mage (MAG)":
                case "Sorceress":
                case "Staff Mage":
                    varDEX = 0.15;
                    varAGI = 0.1;
                    varLCK = 0.05;
                    break;
                case "Dragoon":
                case "Green Mage":
                case "Time Mage":
                    varDEX = 0.05;
                    varLCK = 0.15;
                    break;
                case "Dual Gunner":
                case "Gunner":
                case "Machinist":
                    varDEX = 0.25;
                    varLCK = 0.05;
                    break;
                case "Fell Knight":
                case "Holy Knight":
                case "Lord":
                case "Red Mage (ATK)":
                case "Red Mage (Sword MAG)":
                case "Spellblade":
                case "Squire":
                case "Sword Saint":
                case "Warrior":
                    varDEX = 0.25;
                    varAGI = 0.1;
                    varLCK = 0.1;
                    break;
                case "Gunbreaker":
                case "Paladin":
                case "Warrior of Light":
                    varDEX = 0.1;
                    varAGI = 0.1;
                    varLCK = 0.05;
                    break;
                case "Knight":
                case "Knight of Ruin":
                case "Monk":
                case "Samurai":
                case "Soldier":
                case "Viking":
                case "Winged One":
                    varMAIN = 1.2;
                    break;
                case "Lancer":
                    varDEX = 0.2;
                    varAGI = 0.1;
                    varLCK = 0.15;
                    break;
                case "Ranger":
                case "White Mage":
                case "White Mage of Lapis":
                    varDEX = 0.15;
                    varLCK = 0.05;
                    break;
                case "Thief":
                    varDEX = 0.15;
                    varAGI = 0.3;
                    break;
            }

            Math.Truncate(mainStatVal *= varMAIN);
            Math.Truncate(dexVal *= varDEX);
            Math.Truncate(agiVal *= varAGI);
            Math.Truncate(luckVal *= varLCK);
            double totalMainStat = mainStatVal + dexVal + agiVal + luckVal;
            double totalMainMultiplier = skillMultiplierVal + damageTypeUpVal + elementTypeUpVal + totalKillerVal;

            double finalValue = totalMainStat * (totalMainMultiplier/100) * damageTypeResistanceVal * elementTypeResistanceVal * DEForSPRval * singleAreaResistanceVal * BRVval * elementalAdvantageVal;
            BRVdamage.Text = Math.Truncate(finalValue).ToString();
            finalValue = totalMainStat * (totalMainMultiplier / 100) * damageTypeResistanceVal * elementTypeResistanceVal * DEForSPRval * singleAreaResistanceVal * totalFTHval * elementalAdvantageVal;
            FTHdamage.Text = Math.Truncate(finalValue).ToString();

            //Add Critical Damage Up because now we're calculating damage if the attack were a crit.
            totalMainMultiplier += criticalDamageUpVal;
            //Second verse same as the first
            finalValue = totalMainStat * (totalMainMultiplier / 100) * damageTypeResistanceVal * elementTypeResistanceVal * DEForSPRval * singleAreaResistanceVal * BRVval * elementalAdvantageVal;
            BRVdamageCrit.Text = Math.Truncate(finalValue).ToString();
            finalValue = totalMainStat * (totalMainMultiplier / 100) * damageTypeResistanceVal * elementTypeResistanceVal * DEForSPRval * singleAreaResistanceVal * totalFTHval * elementalAdvantageVal;
            FTHdamageCrit.Text = Math.Truncate(finalValue).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(x => x.Clear());
            this.Controls.OfType<Label>().Where(x => x.Name.Contains("damage")).ToList().ForEach(x => x.Text = "");
            BRVStat.Text = "97";
            comboBox1.SelectedIndex = 0;
            elementAdvantage.SelectedIndex = 1;
            mainStat.Focus();
        }

        private void criticalChanceVsEnemyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            criticalChanceCalculator criticalChanceCalculator = new criticalChanceCalculator(dexStat.Text);
            criticalChanceCalculator.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "A simple (for now) damage calculator made by Yurumates\nAll damage results have a margin of error of around 1%.\nPlease join Rage.";
            string title = "About";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }

        private void accuracyAndEvasionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accuracyAndEvasion accuracyAndEvasion = new accuracyAndEvasion(dexStat.Text, luckStat.Text);
            accuracyAndEvasion.Show();
        }

        private void addToList_Click(object sender, EventArgs e)
        {
            directoryMake();
            string completedLine = "";
            this.Size = new Size(980, 616);
            notesListBox.Items.Add(notesText.Text);
            // Fill all empty textboxes with 0. This prepares the data that will be added to the data listbox to look like 0:0:0:0 instead of ::::
            this.Controls.OfType<TextBox>().ToList().ForEach(x =>
                {
                    if (x.Text == ""){
                        x.Text = "0";
                    }
                });
            completedLine += string.Join(":", this.Controls.OfType<TextBox>().Where(c => c.Name.Contains("Stat")).OrderBy(c => c.TabIndex).Select(c => c.Text.ToString())) + ":";
            completedLine += string.Join(":", this.Controls.OfType<ComboBox>().OrderBy(c => c.TabIndex).Select(c => c.SelectedItem.ToString())) + ":end:";
            notesNumericValues.Items.Add(completedLine);
            // Undoes the previous fill. If the user left the boxes blank, they should see it be kept blank!
            this.Controls.OfType<TextBox>().ToList().ForEach(x =>
            {
                if (x.Text == "0"){
                    x.Text = "";
                }
            });
            if (notesNumericValues.Items.Count != 0)
            {
                using (var writer = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Saved Notes.txt"))
                {
                    for (int x = 0; x < notesNumericValues.Items.Count; x++)
                    {
                        Console.WriteLine("Index is at " + x);
                        notesListBox.SetSelected(x, true);
                        notesNumericValues.SetSelected(x, true);
                        writer.WriteLine(notesListBox.SelectedItem.ToString());
                        Console.WriteLine(notesListBox.SelectedItem.ToString() + " has been output");
                        writer.WriteLine(notesNumericValues.SelectedItem.ToString());
                        Console.WriteLine(notesNumericValues.SelectedItem.ToString() + " has been output");
                    }
                    writer.Close();
                }
            }
        }
        
        private void directoryMake()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\data";
            try
            {
                if (Directory.Exists(appPath))
                {
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(appPath);
            }
            catch (Exception e)
            {

            }
        }

        private void damageCalculator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (notesNumericValues.Items.Count != 0){
                using (var writer = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Saved Notes.txt")){
                    for (int x = 0; x < notesNumericValues.Items.Count; x++){
                        Console.WriteLine("Index is at " + x);
                        notesListBox.SetSelected(x, true);
                        notesNumericValues.SetSelected(x, true);
                        writer.WriteLine(notesListBox.SelectedItem.ToString());
                        Console.WriteLine(notesListBox.SelectedItem.ToString() + " has been output");
                        writer.WriteLine(notesNumericValues.SelectedItem.ToString());
                        Console.WriteLine(notesNumericValues.SelectedItem.ToString() + " has been output");
                    }
                }
            } else {
                File.Delete(Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Saved Notes.txt");
            }
        }

        private void notesListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right){
                notesListBox.SelectedIndex = notesListBox.IndexFromPoint(e.Location);
                if (notesListBox.SelectedIndex != -1){
                    dataListBoxRightMenu.Show(Cursor.Position);
                }
            }
        }

        private void deleteFromList_Click(object sender, EventArgs e)
        {
            int index = this.notesListBox.SelectedIndex;
            if (index != System.Windows.Forms.ListBox.NoMatches){
                notesListBox.Items.RemoveAt(index);
                notesNumericValues.Items.RemoveAt(index);
            }
            if (notesListBox.Items.Count == 0){
                this.Size = new Size(453, 613);
            }
        }

        private void fillFields_Click(object sender, EventArgs e)
        {
            // Click the Calculate button to have empty fields be populated with 0s through its function
            button2_Click(calculateButton, EventArgs.Empty);
            // Generates a string with the location of the file used to store saved data
            string notesLocation = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Saved Notes.txt";
            if (File.Exists(notesLocation))
            {
                using (var reader = new StreamReader(notesLocation))
                {
                    // The temp storage string for each line that will be read
                    string x;
                    // Ensures that both ListBoxes have the same index number
                    notesNumericValues.SetSelected(notesListBox.SelectedIndex, true);
                    notesText.Text = notesListBox.SelectedItem.ToString();
                    // This flag will be used to have the StreamReader go to that exact line in the file
                    // Now that I think about it, isn't flag already going to be what I need? Oh well. Filestreams are fun!
                    string flag = this.notesNumericValues.SelectedItem.ToString();
                    do{
                        x = reader.ReadLine();
                        if(x == null){
                            //Exits the DOWHILE loop if the end of the file is reached
                            return;
                        }
                    } while (x != flag);
                    reader.Close();
                    // Segments the line we went to by the delimiters, colons, into an array of strings that will be siphoned into the form's fields
                    string[] fieldValues = x.Split(':');
                    int count = 0;
                    // You're going to love this
                    int ghettoRig = 0;
                    // Go through every single item in the form
                    foreach (Control item in this.Controls.Cast<Control>().OrderBy(c => c.TabIndex)){
                        if(fieldValues[count] == "end"){
                        // If the string is "end", we've reached the end of the line and don't need to fill any more fields
                            button1_Click(calculateButton, EventArgs.Empty);
                            // With our newly populated fields, let's hit the Calculate button again
                            return;
                        }
                        if (item is TextBox){
                            // As we go through all the forms, check what they are, textbox or combobox
                            ((TextBox)item).Text = fieldValues[count];
                            // Set the currently internally-selected Textbox to whatever is stored in the current fieldValues array
                            count++;
                        }
                        if (item is ComboBox){
                            // Here we go.
                            if(ghettoRig == 0){
                                // I'm stubborn. I put the ComboBox values at the end of the line, but the foreach loop isn't at the end of the
                                // line yet. So, in order to have the string array match the location of the ComboBox values, we'll need to skip
                                // ahead temporarily...
                                count += 19;
                                ((ComboBox)item).SelectedIndex = ((ComboBox)item).FindStringExact(fieldValues[count]);
                                // ...and then return to where we left off.
                                count -= 19;
                                ghettoRig++;
                            } else if(ghettoRig == 1){
                                // You'll also notice that the ComboBoxes are far away from each other. As such, their TabOrder is very different
                                // At this point, the foreach loop is at the second ComboBox, but the string array is at the wrong value. So...
                                count++;
                                // ... now it's where it needs to be.
                                ((ComboBox)item).SelectedIndex = ((ComboBox)item).FindStringExact(fieldValues[count]);
                                // And now it should be at "end"
                                count++;
                            }                            
                        }
                    }
                    
                }
            }            
        }

        private void debuffSuccessChanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debuffSuccessChance debuffSuccessChance = new debuffSuccessChance();
            debuffSuccessChance.Show();
        }

        private void notesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fillFields_Click(null, EventArgs.Empty);
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string message = "A simple (for now) damage calculator made by Yurumates\nAll damage results have a margin of error of around 1%.";
            string title = "About - Version El Grande Sterne";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) //Arithmetician
            {
                nicheLabel.Text = "Enemy Height";
                nicheLabel.Visible = true;
                nicheBox.Visible = true;
                nicheHelp.Visible = true;
            }
            else
            {
                nicheBox.Text = "";
                nicheLabel.Visible = false;
                nicheBox.Visible = false;
                nicheHelp.Visible = false;
            }
        }

        private void nicheHelp_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) //Arithmetician
            {
                string message = "This textbox is mainly for the move 'Height-Based Water'. For Level 3/4 Watera/Waterga and Height 2/3 Holy, see wotv-calc for appropriate skill multipler values. Unlike 'Height-Based Water', they do not get a scaling increase. They get a flat increase. Therefore, you can just input the adjusted number on your own.";
                string title = "Arithmetician Height Help";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
            }
        }

        private void emeraldEchoInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "If the description of the ability says \"Bestow\", then it doesn't work with Emerald Echo.";
            string title = "Emerald Echo Info";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }

        
    }
}
