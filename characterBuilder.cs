using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOTV_FFBE
{
    public partial class characterBuilder : Form
    {
        // I heard people hated global variables, but I'm not a good programmer. Deal with it.
        // There's going to be a nasty amount of these.

        // Volatile
        string previousValue;

        // Player stats
        string element, rarity, mainJob, subjobOne, subjobTwo, weapons;
        string[] armors, passives;
        double move, jump, cost, HP, TP, AP, ATK, MAG, DEX, AGI, LUCK, 
            jHP, jTP, jAP, jATK, jMAG, jDEX, jAGI, jLUCK, 
            bHP, bTP, binitAP, bATK, DEF, bMAG, SPR, bDEX, bAGI, bLUCK, bCRIT, 
            fireRes, iceRes, earthRes, windRes, lightningRes, waterRes, lightRes, darkRes, 
            slashRes, pierceRes, strikeRes, missileRes, magicRes, attackRes, areaRes, 
            HPperMod, TPperMod, APperMod, ATKperMod, MAGperMod, DEXperMod, AGIperMod, LUCKperMod, 
            slashUP, pierceUP, strikeUP, missileUP, magicUP, 
            accuracy, evade, critEvade, 
            fireKiller, iceKiller, earthKiller, windKiller, lightningKiller, waterKiller, lightKiller, darkKiller, humanKiller,
            fireUP, iceUP, earthUP, windUP, lightningUP, waterUP, lightUP, darkUP, 
            slashFlatPen, pierceFlatPen, strikeFlatPen, missileFlatPen, magicFlatPen, 
            fireFlatPen, iceFlatPen, earthFlatPen, windFlatPen, lightningFlatPen, waterFlatPen, lightFlatPen, darkFlatPen, 
            slashPercentPen, piercePercentPen, strikePercentPen, missilePercentPen, magicPercentPen, 
            firePercentPen, icePercentPen, earthPercentPen, windPercentPen, lightningPercentPen, waterPercentPen, lightPercentPen, darkPercentPen;

        /* Enemy stats
        string elementE, rarityE, mainJobE, subjobOneE, subjobTwoE, weaponsE;
        string[] armorsE, passivesE;
        double moveE, jumpE, costE, HPE, TPE, APE, ATKE, MAGE, DEXE, AGIE, LUCKE, 
            jHPE, jTPE, jAPE, jATKE, jMAGE, jDEXE, jAGIE, jLUCKE, 
            bHPE, bTPE, binitAPE, bATKE, DEFE, bMAGE, SPRE, bDEXE, bAGIE, bLUCKE, bCRITE, 
            fireResE, iceResE, earthResE, windResE, lightningResE, waterResE, lightResE, darkResE, 
            slashResE, pierceResE, strikeResE, missileResE, magicResE, attackResE, areaResE, 
            HPperModE, TPperModE, APperModE, ATKperModE, MAGperModE, DEXperModE, AGIperModE, LUCKperModE, 
            slashUPE, pierceUPE, strikeUPE, missileUPE, magicUPE, 
            accuracyE, evadeE, critEvadeE, 
            fireKillerE, iceKillerE, earthKillerE, windKillerE, lightningKillerE, waterKillerE, lightKillerE, darkKillerE, humanKillerE,
            fireUPE, iceUPE, earthUPE, windUPE, lightningUPE, waterUPE, lightUPE, darkUPE,
            slashFlatPenE, pierceFlatPenE, strikeFlatPenE, missileFlatPenE, magicFlatPenE,
            fireFlatPenE, iceFlatPenE, earthFlatPenE, windFlatPenE, lightningFlatPenE, waterFlatPenE, lightFlatPenE, darkFlatPenE,
            slashPercentPenE, piercePercentPenE, strikePercentPenE, missilePercentPenE, magicPercentPenE,
            firePercentPenE, icePercentPenE, earthPercentPenE, windPercentPenE, lightningPercentPenE, waterPercentPenE, lightPercentPenE, darkPercentPenE;*/

        public characterBuilder()
        {
            InitializeComponent();
            string dbLoc = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Internal.sqlite";
            if (File.Exists(dbLoc) == false) { databaseCreator databaseCreator = new databaseCreator(dbLoc); databaseCreator.ShowDialog(); }
            populate(dbLoc);
        }        

        private void button6_Click(object sender, EventArgs e)
        {
            /*send to calculator button

	        Look at character's stat globals
	        Tally up all equipment and grab the max stats
	        Grab vision card passive abilities from Vision Card
						        Party Ability 1~4
	        Grab passive abilites from Passive 1~2
	        Grab buffs active
	        Grab esper passives
	        Sum level 99 stats and job stats
	        Multiply modifiers gained from all passives
	        add vision card, esper, and equipment stats

	        fill to calc
	        literally everything*/

            SQLiteConnection con = new SQLiteConnection("Data Source=" + Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Internal.sqlite" + ";Version=3;");
            con.Open();

            // Determine element advantages
            string strongVS, weakVS;
            switch (element){
                case "fire": strongVS = "ice"; weakVS = "water"; break;
                case "ice": strongVS = "wind"; weakVS = "fire"; break;
                case "wind": strongVS = "earth"; weakVS = "ice"; break;
                case "earth": strongVS = "lightning"; weakVS = "wind"; break;
                case "lightning": strongVS = "water"; weakVS = "earth"; break;
                case "water": strongVS = "fire"; weakVS = "lightning"; break;
                case "light": strongVS = "dark"; weakVS = ""; break;
                case "dark": strongVS = "light"; weakVS = ""; break;
            }

            string sql = "SELECT * FROM passives WHERE name IS '" + comboBox7.SelectedItem.ToString() + "'";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            double outputVal;
            // Applying Passive 1
            Double.TryParse(reader["ATKperMod"].ToString(), out outputVal); ATKperMod += outputVal;
            Double.TryParse(reader["DEXperMod"].ToString(), out outputVal); DEXperMod += outputVal;
            Double.TryParse(reader["AGIperMod"].ToString(), out outputVal); AGIperMod += outputVal;
            Double.TryParse(reader["LUCKperMod"].ToString(), out outputVal); LUCKperMod += outputVal;
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 



            damageCalculator parentForm = (damageCalculator)this.Owner;
            parentForm.returnedValue("a", magicRes.ToString()); // This single line will send everything back to damageCalculator
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dbLoc = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Internal.sqlite";
            databaseCreator databaseCreator = new databaseCreator(dbLoc); databaseCreator.ShowDialog();
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            SQLiteConnection con = new SQLiteConnection("Data Source=" + Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Internal.sqlite" + ";Version=3;");
            con.Open();

            sql = "SELECT * FROM characters WHERE name IS '" + comboBox1.SelectedItem.ToString() + "'";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            // Load all global variables
            element = reader["element"].ToString();
            rarity = reader["rarity"].ToString();
            Double.TryParse(reader["move"].ToString(), out move);
            Double.TryParse(reader["jump"].ToString(), out jump);
            Double.TryParse(reader["cost"].ToString(), out cost);
            mainJob = reader["mainJob"].ToString();
            subjobOne = reader["subjobOne"].ToString();
            subjobTwo = reader["subjobTwo"].ToString();
            weapons = reader["weapons"].ToString();
            armors = reader["armors"].ToString().Split(',');
            Double.TryParse(reader["HP"].ToString(), out HP);
            Double.TryParse(reader["TP"].ToString(), out TP);
            Double.TryParse(reader["AP"].ToString(), out AP);
            Double.TryParse(reader["ATK"].ToString(), out ATK);
            Double.TryParse(reader["MAG"].ToString(), out MAG);
            Double.TryParse(reader["DEX"].ToString(), out DEX);
            Double.TryParse(reader["AGI"].ToString(), out AGI);
            Double.TryParse(reader["LUCK"].ToString(), out LUCK);
            Double.TryParse(reader["jHP"].ToString(), out jHP);
            Double.TryParse(reader["jTP"].ToString(), out jTP);
            Double.TryParse(reader["jAP"].ToString(), out jAP);
            Double.TryParse(reader["jATK"].ToString(), out jATK);
            Double.TryParse(reader["jMAG"].ToString(), out jMAG);
            Double.TryParse(reader["jDEX"].ToString(), out jDEX);
            Double.TryParse(reader["jAGI"].ToString(), out jAGI);
            Double.TryParse(reader["jLUCK"].ToString(), out jLUCK);
            Double.TryParse(reader["bHP"].ToString(), out bHP);
            Double.TryParse(reader["bTP"].ToString(), out bTP);
            Double.TryParse(reader["binitAP"].ToString(), out binitAP);
            Double.TryParse(reader["bATK"].ToString(), out bATK);
            Double.TryParse(reader["DEF"].ToString(), out DEF);
            Double.TryParse(reader["bMAG"].ToString(), out bMAG);
            Double.TryParse(reader["SPR"].ToString(), out SPR);
            Double.TryParse(reader["bDEX"].ToString(), out bDEX);
            Double.TryParse(reader["bAGI"].ToString(), out bAGI);
            Double.TryParse(reader["bLUCK"].ToString(), out bLUCK);
            Double.TryParse(reader["bCRIT"].ToString(), out bCRIT);
            Double.TryParse(reader["fireRes"].ToString(), out fireRes);
            Double.TryParse(reader["iceRes"].ToString(), out iceRes);
            Double.TryParse(reader["earthRes"].ToString(), out earthRes);
            Double.TryParse(reader["windRes"].ToString(), out windRes);
            Double.TryParse(reader["lightningRes"].ToString(), out lightningRes);
            Double.TryParse(reader["waterRes"].ToString(), out waterRes);
            Double.TryParse(reader["lightRes"].ToString(), out lightRes);
            Double.TryParse(reader["darkRes"].ToString(), out darkRes);
            Double.TryParse(reader["slashRes"].ToString(), out slashRes);
            Double.TryParse(reader["pierceRes"].ToString(), out pierceRes);
            Double.TryParse(reader["strikeRes"].ToString(), out strikeRes);
            Double.TryParse(reader["missileRes"].ToString(), out missileRes);
            Double.TryParse(reader["magicRes"].ToString(), out magicRes);
            Double.TryParse(reader["attackRes"].ToString(), out attackRes);
            Double.TryParse(reader["areaRes"].ToString(), out areaRes);
            Double.TryParse(reader["HPperMod"].ToString(), out HPperMod);
            Double.TryParse(reader["TPperMod"].ToString(), out TPperMod);
            Double.TryParse(reader["APperMod"].ToString(), out APperMod);
            Double.TryParse(reader["ATKperMod"].ToString(), out ATKperMod);
            Double.TryParse(reader["MAGperMod"].ToString(), out MAGperMod);
            Double.TryParse(reader["DEXperMod"].ToString(), out DEXperMod);
            Double.TryParse(reader["AGIperMod"].ToString(), out AGIperMod);
            Double.TryParse(reader["LUCKperMod"].ToString(), out LUCKperMod);
            passives = reader["passives"].ToString().Split(',');
            Double.TryParse(reader["slashUP"].ToString(), out slashUP); 
            Double.TryParse(reader["pierceUp"].ToString(), out pierceUP); 
            Double.TryParse(reader["strikeUp"].ToString(), out strikeUP); 
            Double.TryParse(reader["missileUp"].ToString(), out missileUP); 
            Double.TryParse(reader["magicUp"].ToString(), out magicUP);
            Double.TryParse(reader["accuracy"].ToString(), out accuracy);
            Double.TryParse(reader["evade"].ToString(), out evade);
            Double.TryParse(reader["critEvade"].ToString(), out critEvade);
            Double.TryParse(reader["fireKiller"].ToString(), out fireKiller);
            Double.TryParse(reader["iceKiller"].ToString(), out iceKiller);
            Double.TryParse(reader["earthKiller"].ToString(), out earthKiller);
            Double.TryParse(reader["windKiller"].ToString(), out windKiller);
            Double.TryParse(reader["lightningKiller"].ToString(), out lightningKiller);
            Double.TryParse(reader["waterKiller"].ToString(), out waterKiller);
            Double.TryParse(reader["lightKiller"].ToString(), out lightKiller);
            Double.TryParse(reader["darkKiller"].ToString(), out darkKiller);
            Double.TryParse(reader["humanKiller"].ToString(), out humanKiller);
            Double.TryParse(reader["fireUP"].ToString(), out fireUP);
            Double.TryParse(reader["iceUP"].ToString(), out iceUP);
            Double.TryParse(reader["earthUP"].ToString(), out earthUP);
            Double.TryParse(reader["windUP"].ToString(), out windUP);
            Double.TryParse(reader["lightningUP"].ToString(), out lightningUP);
            Double.TryParse(reader["waterUP"].ToString(), out waterUP);
            Double.TryParse(reader["lightUP"].ToString(), out lightUP);
            Double.TryParse(reader["darkUP"].ToString(), out darkUP);
            Double.TryParse(reader["slashFlatPen"].ToString(), out slashFlatPen);
            Double.TryParse(reader["pierceFlatPen"].ToString(), out pierceFlatPen);
            Double.TryParse(reader["strikeFlatPen"].ToString(), out strikeFlatPen);
            Double.TryParse(reader["missileFlatPen"].ToString(), out missileFlatPen);
            Double.TryParse(reader["magicFlatPen"].ToString(), out magicFlatPen);
            Double.TryParse(reader["fireFlatPen"].ToString(), out fireFlatPen);
            Double.TryParse(reader["iceFlatPen"].ToString(), out iceFlatPen);
            Double.TryParse(reader["earthFlatPen"].ToString(), out earthFlatPen);
            Double.TryParse(reader["windFlatPen"].ToString(), out windFlatPen);
            Double.TryParse(reader["lightningFlatPen"].ToString(), out lightningFlatPen);
            Double.TryParse(reader["waterFlatPen"].ToString(), out waterFlatPen);
            Double.TryParse(reader["lightFlatPen"].ToString(), out lightFlatPen);
            Double.TryParse(reader["darkFlatPen"].ToString(), out darkFlatPen);
            Double.TryParse(reader["slashPercentPen"].ToString(), out slashPercentPen);
            Double.TryParse(reader["piercePercentPen"].ToString(), out piercePercentPen);
            Double.TryParse(reader["strikePercentPen"].ToString(), out strikePercentPen);
            Double.TryParse(reader["missilePercentPen"].ToString(), out missilePercentPen);
            Double.TryParse(reader["magicPercentPen"].ToString(), out magicPercentPen);
            Double.TryParse(reader["firePercentPen"].ToString(), out firePercentPen);
            Double.TryParse(reader["icePercentPen"].ToString(), out icePercentPen);
            Double.TryParse(reader["earthPercentPen"].ToString(), out earthPercentPen);
            Double.TryParse(reader["windPercentPen"].ToString(), out windPercentPen);
            Double.TryParse(reader["lightningPercentPen"].ToString(), out lightningPercentPen);
            Double.TryParse(reader["waterPercentPen"].ToString(), out waterPercentPen);
            Double.TryParse(reader["lightPercentPen"].ToString(), out lightPercentPen);
            Double.TryParse(reader["darkPercentPen"].ToString(), out darkPercentPen);
            con.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Replace passives
            comboBox7.Items.Clear(); comboBox8.Items.Clear(); 
            for (int x = 0; x != passives.Count(); x++) { comboBox7.Items.Add(passives[x]); comboBox8.Items.Add(passives[x]); }

            // Populate equipment fields with equipment that the selected character can use
            /*
             * That goes here 
             */
        }

        private void populate(string dbLoc)
        {
            // This function fills the comboboxes with data available for every character including:
            // Characters, Vision Cards, Equipment, select few Abilities that can be cast on turn one and two of a fight
            string sql;
            SQLiteConnection con = new SQLiteConnection("Data Source=" + dbLoc + ";Version=3;");
            con.Open();

            sql = "SELECT * FROM characters ORDER BY name ASC";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["name"]); comboBox9.Items.Add(reader["name"]);
            }

            // new command for esper combobox
            // new command for vision cards and all party ability comboboxes
            // new command for all equipment comboboxes
            // new command for all buff comboboxes

            con.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
