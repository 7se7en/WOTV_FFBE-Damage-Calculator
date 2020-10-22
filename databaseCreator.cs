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
    public partial class databaseCreator : Form
    {
        public databaseCreator(string input)
        {
            InitializeComponent();
        }

        private void databaseCreator_Shown(object sender, EventArgs e)
        {
            string dbLoc = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\Internal.sqlite";
            try{
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(dbLoc);
                SQLiteConnection.CreateFile(dbLoc);
                SQLiteConnection con = new SQLiteConnection("Data Source=" + dbLoc + ";Version=3;");
                con.Open();
                string sql;
                // Generates a table containing character information
                sql = "CREATE TABLE characters (name TEXT, " +
                    "element TEXT, " +
                    "rarity VARCHAR(3), " +
                    "move INT, " +
                    "jump INT, " +
                    "cost INT, " +
                    "mainJob TEXT, " +
                    "subjobOne TEXT, " +
                    "subjobTwo TEXT, " +
                    "weapons TEXT, " +
                    "armors TEXT, " +
                    "HP DOUBLE, " +
                    "TP DOUBLE, " +
                    "AP DOUBLE, " +
                    "ATK DOUBLE, " +
                    "MAG DOUBLE, " +
                    "DEX DOUBLE, " +
                    "AGI DOUBLE, " +
                    "LUCK DOUBLE, " +
                    "jHP DOUBLE, " +
                    "jTP DOUBLE, " +
                    "jAP DOUBLE, " +
                    "jATK DOUBLE, " +
                    "jMAG DOUBLE, " +
                    "jDEX DOUBLE, " +
                    "jAGI DOUBLE, " +
                    "jLUCK DOUBLE, " +
                    "bHP DOUBLE, " +
                    "bTP DOUBLE, " +
                    "binitAP DOUBLE, " +
                    "bATK DOUBLE, " +
                    "DEF DOUBLE, " +
                    "bMAG DOUBLE, " +
                    "SPR DOUBLE, " +
                    "bDEX DOUBLE, " +
                    "bAGI DOUBLE, " +
                    "bLUCK DOUBLE, " +
                    "bCRIT DOUBLE, " +
                    "fireRes DOUBLE, " +
                    "iceRes DOUBLE, " +
                    "earthRes DOUBLE, " +
                    "windRes DOUBLE, " +
                    "lightningRes DOUBLE, " +
                    "waterRes DOUBLE, " +
                    "lightRes DOUBLE, " +
                    "darkRes DOUBLE, " +
                    "slashRes DOUBLE, " +
                    "pierceRes DOUBLE, " +
                    "strikeRes DOUBLE, " +
                    "missileRes DOUBLE, " +
                    "magicRes DOUBLE, " +
                    "attackRes DOUBLE, " +
                    "areaRes DOUBLE, " +
                    "HPperMod DOUBLE, " +
                    "TPperMod DOUBLE, " +
                    "APperMod DOUBLE, " +
                    "ATKperMod DOUBLE, " +
                    "MAGperMod DOUBLE, " +
                    "DEXperMod DOUBLE, " +
                    "AGIperMod DOUBLE, " +
                    "LUCKperMod DOUBLE, " +
                    "passives TEXT, " +
                    "slashUP DOUBLE, " +
                    "pierceUP DOUBLE, " +
                    "strikeUP DOUBLE, " +
                    "missileUP DOUBLE, " +
                    "magicUP DOUBLE," +
                    "accuracy DOUBLE," +
                    "evade DOUBLE, " +
                    "critEvade DOUBLE, " +
                    "fireKiller DOUBLE, " +
                    "iceKiller DOUBLE, " +
                    "earthKiller DOUBLE, " +
                    "windKiller DOUBLE, " +
                    "lightningKiller DOUBLE, " +
                    "waterKiller DOUBLE, " +
                    "lightKiller DOUBLE, " +
                    "darkKiller DOUBLE, " +
                    "humanKiller DOUBLE, " +
                    "fireUP DOUBLE, " +
                    "iceUP DOUBLE, " +
                    "earthUP DOUBLE, " +
                    "windUP DOUBLE, " +
                    "lightningUP DOUBLE, " +
                    "waterUP DOUBLE, " +
                    "lightUP DOUBLE, " +
                    "darkUP DOUBLE, " +
                    "slashFlatPen DOUBLE, " +
                    "pierceFlatPen DOUBLE, " +
                    "strikeFlatPen DOUBLE, " +
                    "missileFlatPen DOUBLE, " +
                    "magicFlatPen DOUBLE, " +
                    "fireFlatPen DOUBLE, " +
                    "iceFlatPen DOUBLE, " +
                    "earthFlatPen DOUBLE, " +
                    "windFlatPen DOUBLE, " +
                    "lightningFlatPen DOUBLE, " +
                    "waterFlatPen DOUBLE, " +
                    "lightFlatPen DOUBLE, " +
                    "darkFlatPen DOUBLE, " +
                    "slashPercentPen DOUBLE, " +
                    "piercePercentPen DOUBLE, " +
                    "strikePercentPen DOUBLE, " +
                    "missilePercentPen DOUBLE, " +
                    "magicPercentPen DOUBLE, " +
                    "firePercentPen DOUBLE, " +
                    "icePercentPen DOUBLE, " +
                    "earthPercentPen DOUBLE, " +
                    "windPercentPen DOUBLE, " +
                    "lightningPercentPen DOUBLE, " +
                    "waterPercentPen DOUBLE, " +
                    "lightPercentPen DOUBLE, " +
                    "darkPercentPen DOUBLE)";
                scrollToBottom(new SQLiteCommand(sql, con), sql);
                // And here be all the characters
                string chunkOne = "INSERT INTO characters (name, element, rarity, move, jump, cost, mainJob, subjobOne, subjobTwo, weapons, armors, HP, TP, AP, ATK, MAG, DEX, AGI, LUCK, jHP, jTP, jAP, jATK, jMAG, jDEX, jAGI, jLUCK, bHP, bTP, binitAP, bATK, DEF, bMAG, SPR, bDEX, bAGI, bLUCK, bCRIT, fireRes, iceRes, earthRes, windRes, lightningRes, waterRes, lightRes, darkRes, slashRes, pierceRes, strikeRes, missileRes, magicRes, attackRes, areaRes, HPperMod, TPperMod, APperMod, ATKperMod, MAGperMod, DEXperMod, AGIperMod, LUCKperMod, passives, slashUP, pierceUp, strikeUp, missileUp, magicUp, accuracy, evade, critEvade, fireKiller, iceKiller, earthKiller, windKiller, lightningKiller, waterKiller, lightKiller, darkKiller, humanKiller, fireUP, iceUP, earthUP, windUP, lightningUP, waterUP, lightUP, darkUP, slashFlatPen, pierceFlatPen, strikeFlatPen, missileFlatPen, magicFlatPen, fireFlatPen, iceFlatPen, earthFlatPen, windFlatPen, lightningFlatPen, waterFlatPen, lightFlatPen, darkFlatPen, slashPercentPen, piercePercentPen, strikePercentPen, missilePercentPen, magicPercentPen, firePercentPen, icePercentPen, earthPercentPen, windPercentPen, lightningPercentPen, waterPercentPen, lightPercentPen, darkPercentPen) values (";
                sql = chunkOne + "'Whisper', 'Dark', 'UR', 3, 1, 70, 'Knight', 'Samurai', 'Spellblade', 'Sword', 'Armor,Helm,Accessory',759, 92, 87, 126, 75, 98, 34, 105, 910, 38, 33, 87, 21, 51, 19, 65, 30, 9, 0, 28, 6, 14, 8, 36, 4, 30, 12, 0, 0, 0, 0, 0, 0, -10, 0, 30, 30, 30, 30, 30, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 'Divine Protection,Blade Soul,HP Up Lv. 1,Providence of Light', 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"; scrollToBottom(new SQLiteCommand(sql, con), sql);
                sql = chunkOne + "'Agrias', 'Ice', 'UR', 3, 1, 80, 'Holy Knight', 'Paladin', 'Cleric', 'Sword', 'Armor,Helm,Accessory',1334, 94, 89, 135, 55, 113, 34, 112, 1600, 48, 45, 99, 15, 63, 19, 53, 200, 0, 0, 45, 6, 8, 6, 16, 8, 35, 0, -10, 0, 20, 0, 0, 0, 0, 0, 5, -10, 5, 0, 20, 15, 0, 0, 0, 0, 20, 0, 0, 0, 0, 'Chivalry,Holy Knights Protection,Knights Honor,SPR Up Lv. 1', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"; scrollToBottom(new SQLiteCommand(sql, con), sql);
                sql = chunkOne + "'Aileen', 'Earth', 'UR', 3, 1, 70, 'Lancer', 'Warrior', 'Soldier', 'Spear', 'Hat,Helm,Cloth,Armor,Accessory',889, 93, 87, 156, 55, 101, 31, 90, 1066, 42, 40, 131, 14, 61, 18, 46, 0, 0, 0, 77, 0, 0, 0, 60, 0, 48, 16, 0, 0, 0, -10, 20, 0, 0, 0, 20, 0, -10, 0, -10, 0, 0, 0, 10, 0, 20, 0, 0, 0, 0, 'Pierce Mastery,ATK Up Lv. 1,Deadly Mastery, Self-Sacrifice', 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"; scrollToBottom(new SQLiteCommand(sql, con), sql);
                sql = chunkOne + "'Ayaka', 'Wind', 'UR', 3, 1, 80, 'White Mage', 'Time Mage', 'Green Mage', 'Rod', 'Hat,Cloth,Accessory',855, 110, 96, 56, 126, 91, 35, 91, 1026, 97, 80, 16, 105, 27, 25, 51, 60, 9, 10, 0, 0, 42, 0, 24, 4, 30, 0, 0, -10, 20, 0, 0, 0, 0, 0, -5, -5, -5, -5, 10, 0, 0, 10, 0, 0, 0, 20, 0, 0, 0, 'March of the Saints,Null CT Changes,Saviors Protection,Speed Cast,Emerald Echo', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"; scrollToBottom(new SQLiteCommand(sql, con), sql);
                sql = chunkOne + "'Delita', 'Fire', 'UR', 3, 1, 80, 'Squire', 'Soldier', 'Paladin', 'Sword', 'Armor,Hat,Helm,Cloth,Accessory',982, 92, 87, 136, 122, 102, 37, 106, 1178, 38, 36, 114, 60, 50, 20, 67, 207, 0, 0, 47, 4, 0, 0, 28, 6, 46, 3, 0, 20, 0, 0, 0, -10, 0, 0, 10, 15, -5, 10, -20, 0, 0, 0, 0, 0, 40, 0, 0, 0, 0, 'Move +1,Acquired AP Up,Holy Knights Protection,Self-Sacrifice', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"; scrollToBottom(new SQLiteCommand(sql, con), sql);

                // Generates a table containing passives information
                sql = "CREATE TABLE passives (name TEXT, " +                    
                    "fireRes DOUBLE, " +
                    "iceRes DOUBLE, " +
                    "earthRes DOUBLE, " +
                    "windRes DOUBLE, " +
                    "lightningRes DOUBLE, " +
                    "waterRes DOUBLE, " +
                    "lightRes DOUBLE, " +
                    "darkRes DOUBLE, " +
                    "slashRes DOUBLE, " +
                    "pierceRes DOUBLE, " +
                    "strikeRes DOUBLE, " +
                    "missileRes DOUBLE, " +
                    "magicRes DOUBLE, " +
                    "attackRes DOUBLE, " +
                    "areaRes DOUBLE, " +
                    "HPperMod DOUBLE, " +
                    "TPperMod DOUBLE, " +
                    "APperMod DOUBLE, " +
                    "ATKperMod DOUBLE, " +
                    "MAGperMod DOUBLE, " +
                    "DEXperMod DOUBLE, " +
                    "AGIperMod DOUBLE, " +
                    "LUCKperMod DOUBLE, " +
                    "DEF DOUBLE, " +
                    "SPR DOUBLE, " +
                    "slashUP DOUBLE, " +
                    "pierceUP DOUBLE, " +
                    "strikeUP DOUBLE, " +
                    "missileUP DOUBLE, " +
                    "magicUP DOUBLE," +
                    "accuracy DOUBLE," +
                    "evade DOUBLE, " +
                    "critEvade DOUBLE, " +
                    "fireKiller DOUBLE, " +
                    "iceKiller DOUBLE, " +
                    "earthKiller DOUBLE, " +
                    "windKiller DOUBLE, " +
                    "lightningKiller DOUBLE, " +
                    "waterKiller DOUBLE, " +
                    "lightKiller DOUBLE, " +
                    "darkKiller DOUBLE, " +
                    "humanKiller DOUBLE, " +
                    "fireUP DOUBLE, " +
                    "iceUP DOUBLE, " +
                    "earthUP DOUBLE, " +
                    "windUP DOUBLE, " +
                    "lightningUP DOUBLE, " +
                    "waterUP DOUBLE, " +
                    "lightUP DOUBLE, " +
                    "darkUP DOUBLE, " +
                    "slashFlatPen DOUBLE, " +
                    "pierceFlatPen DOUBLE, " +
                    "strikeFlatPen DOUBLE, " +
                    "missileFlatPen DOUBLE, " +
                    "magicFlatPen DOUBLE, " +
                    "fireFlatPen DOUBLE, " +
                    "iceFlatPen DOUBLE, " +
                    "earthFlatPen DOUBLE, " +
                    "windFlatPen DOUBLE, " +
                    "lightningFlatPen DOUBLE, " +
                    "waterFlatPen DOUBLE, " +
                    "lightFlatPen DOUBLE, " +
                    "darkFlatPen DOUBLE, " +
                    "slashPercentPen DOUBLE, " +
                    "piercePercentPen DOUBLE, " +
                    "strikePercentPen DOUBLE, " +
                    "missilePercentPen DOUBLE, " +
                    "magicPercentPen DOUBLE, " +
                    "firePercentPen DOUBLE, " +
                    "icePercentPen DOUBLE, " +
                    "earthPercentPen DOUBLE, " +
                    "windPercentPen DOUBLE, " +
                    "lightningPercentPen DOUBLE, " +
                    "waterPercentPen DOUBLE, " +
                    "lightPercentPen DOUBLE, " +
                    "darkPercentPen DOUBLE)";
                scrollToBottom(new SQLiteCommand(sql, con), sql);
                // And here be all the passives
                chunkOne = "INSERT INTO passives (name, fireRes, iceRes, earthRes, windRes, lightningRes, waterRes, lightRes, darkRes, slashRes, pierceRes, strikeRes, missileRes, magicRes, attackRes, areaRes, HPperMod, TPperMod, APperMod, ATKperMod, MAGperMod, DEXperMod, AGIperMod, LUCKperMod, DEF, SPR, slashUP, pierceUp, strikeUp, missileUp, magicUp, accuracy, evade, critEvade, fireKiller, iceKiller, earthKiller, windKiller, lightningKiller, waterKiller, lightKiller, darkKiller, humanKiller, fireUP, iceUP, earthUP, windUP, lightningUP, waterUP, lightUP, darkUP, slashFlatPen, pierceFlatPen, strikeFlatPen, missileFlatPen, magicFlatPen, fireFlatPen, iceFlatPen, earthFlatPen, windFlatPen, lightningFlatPen, waterFlatPen, lightFlatPen, darkFlatPen, slashPercentPen, piercePercentPen, strikePercentPen, missilePercentPen, magicPercentPen, firePercentPen, icePercentPen, earthPercentPen, windPercentPen, lightningPercentPen, waterPercentPen, lightPercentPen, darkPercentPen) values (";
                sql = chunkOne + "'Self-Sacrifice', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 60, 0, 0, 0, 0, 0, 0, -8, -8, -8, -8, -8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"; scrollToBottom(new SQLiteCommand(sql, con), sql);

                con.Close();
            } catch ( Exception error ) {
                string message = "There was an error in creating the core database file. Restart the program and try again.\n\n"+error.ToString();
                string title = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                _ = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Close();
        }

        private void scrollToBottom(SQLiteCommand input, string sql)
        {
            listBox1.Items.Add(sql);
            input.ExecuteNonQuery();
            Update();
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
    }
}
