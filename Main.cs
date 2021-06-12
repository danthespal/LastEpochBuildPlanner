using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LastEpochBuildPlanner
{
    public partial class Main : Form
    {
        // declare global variables
        private string name;
        private int lvl, str, dex, intel, att, vit, mana, manaReg, movSpeed, dodge, health;
        private double healthReg, armour, wardRet, healthBLvl, manaBLevel, healthRegBLvl;

        public Main()
        {
            InitializeComponent();
            UpdateClass();
        }

        private void LoadXML()
        {
            // selected item is trimed of '-' and ' ' (space)
            string selected = classList.Text.Trim(new char[] { '-', ' ' });

            // load and
            // store default values based on class
            XElement selectedClass = XElement.Load(@"Data\classes.xml");
            IEnumerable<XElement> Class =
                from cls in selectedClass.Elements("class")
                where (string)cls.Element("Name") == selected
                select cls;
            foreach (XElement cls in Class)
            {
                name = cls.Element("Name").Value;
                lvl = Convert.ToInt32(cls.Element("Level").Value);
                str = Convert.ToInt32(cls.Element("Strength").Value);
                dex = Convert.ToInt32(cls.Element("Dexterity").Value);
                intel = Convert.ToInt32(cls.Element("Intelligence").Value);
                att = Convert.ToInt32(cls.Element("Attunement").Value);
                vit = Convert.ToInt32(cls.Element("Vitality").Value);
                health = Convert.ToInt32(cls.Element("Health").Value);
                mana = Convert.ToInt32(cls.Element("Mana").Value);
                healthReg = Convert.ToInt32(cls.Element("HealthReg").Value);
                manaReg = Convert.ToInt32(cls.Element("ManaReg").Value);
                movSpeed = Convert.ToInt32(cls.Element("MovSpd").Value);

                classStr.Text = name;
                lvlValue.Value = lvl;
                strValue.Text = Convert.ToString(str);
                dexValue.Text = Convert.ToString(dex);
                intValue.Text = Convert.ToString(intel);
                attValue.Text = Convert.ToString(att);
                vitValue.Text = Convert.ToString(vit);
                healthValue.Text = Convert.ToString(health);
                manaValue.Text = Convert.ToString(mana);
                healthRegenValue.Text = Convert.ToString(healthReg);
                manaRegenValue.Text = Convert.ToString(manaReg);
                movspeedValue.Text = Convert.ToString(movSpeed);
            }
        }

        private void UpdateClass()
        {
            // this will disable passiveBtn while the value is null or selected item is empty
            // else enable
            if (classList.SelectedItem == null || string.IsNullOrEmpty(classList.SelectedItem.ToString()))
            {
                passivesBtn.Enabled = false;

                name = "";
                lvl = 1;
                str = 0;
                dex = 0;
                intel = 0;
                att = 0;
                vit = 0;
                health = 0;
                mana = 0;
                healthReg = 0;
                manaReg = 0;
                movSpeed = 0;

                classPicBox.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("default_class");
                classStr.Text = name;
                lvlValue.Value = lvl;
                strValue.Text = Convert.ToString(str);
                dexValue.Text = Convert.ToString(dex);
                intValue.Text = Convert.ToString(intel);
                attValue.Text = Convert.ToString(att);
                vitValue.Text = Convert.ToString(vit);
                healthValue.Text = Convert.ToString(health);
                manaValue.Text = Convert.ToString(mana);
                healthRegenValue.Text = Convert.ToString(healthReg);
                manaRegenValue.Text = Convert.ToString(manaReg);
                movspeedValue.Text = Convert.ToString(movSpeed);
            }
            else
            {
                passivesBtn.Enabled = true;

                classPicBox.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(name);
                classStr.Text = name;
                strValue.Text = Convert.ToString(str);
                dexValue.Text = Convert.ToString(dex);
                intValue.Text = Convert.ToString(intel);
                attValue.Text = Convert.ToString(att);
                vitValue.Text = Convert.ToString(vit);
                healthValue.Text = Convert.ToString(health);
                manaValue.Text = Convert.ToString(mana);
                healthRegenValue.Text = Convert.ToString(healthReg);
                manaRegenValue.Text = Convert.ToString(manaReg);
                movspeedValue.Text = Convert.ToString(movSpeed);
            }

            // update default values based on points
            if (att > 0)
            {
                // attunement is one of the Character Stats which grants 2 mana per point and improves skills
                // that rely on innate magic inside the character and its surroundings.
                mana += att * 2;
                manaValue.Text = Convert.ToString(mana);
            }
            else
            {
                manaValue.Text = Convert.ToString(mana);
            }

            if (str > 0)
            {
                // strength is one of the Character Stats which increases Armor by 5% per point
                armour += Math.Floor(str * 0.4f);
                armourValue.Text = Convert.ToString(armour);
            }
            else
            {
                armourValue.Text = "0";
            }

            if (dex > 0)
            {
                // each point of Dexterity that a character has grants
                // an additional 4 points to the maximum Dodge Rating
                dodge += dex * 4;
                dodgeValue.Text = Convert.ToString(dodge);
            }
            else
            {
                dodgeValue.Text = "0";
            }

            if (intel > 0)
            {
                // intelligence is one of the Character Stats which increases Ward Retention by 4% per point
                wardRet += Math.Floor(intel * 0.4f);
                wardRetValue.Text = Convert.ToString(wardRet);
            }
            else
            {
                wardRetValue.Text = "0";
            }

            if (vit > 0)
            {
                // vitality is one of the Character Stats which grants 10 health and 2% increased health regen.
                health += 10 * Convert.ToInt32(lvlValue.Value);
                healthReg += Math.Floor(healthReg * 0.2f);
                healthValue.Text = Convert.ToString(health);
                healthRegenValue.Text = Convert.ToString(healthReg);
            }
            else
            {
                healthValue.Text = Convert.ToString(health);
                healthRegenValue.Text = Convert.ToString(healthReg);
            }
        }

        private void passivesBtn_Click(object sender, EventArgs e)
        {
            // new form, passive points panel
            var passiveForm = new Passives();
            passiveForm.Show();
        }

        // update values based on selected class
        private void classList_TextChanged(object sender, EventArgs e)
        {
            LoadXML();
            UpdateClass();
        }

        private void lvlValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateClass();

            // strength

            // dexterity

            // intelligence

            // attunement

            // vitality

            // health
            if (classList.Text == "Druid")
            {
                healthBLvl = Math.Floor((9.6 * (int)lvlValue.Value) - 9.6 + health);
                healthValue.Text = Convert.ToString(healthBLvl);
            } else
            {
                healthBLvl = (8 * (int)lvlValue.Value) - 8 + health;
                healthValue.Text = Convert.ToString(healthBLvl);
            }

            // mana
            if (classList.Text == "Druid")
            {
                manaBLevel = Math.Floor(0.6f * (int)lvlValue.Value) + mana;
                manaValue.Text = Convert.ToString(manaBLevel);
            } else
            {
                manaBLevel = Math.Floor(0.5f * (int)lvlValue.Value) + mana;
                manaValue.Text = Convert.ToString(manaBLevel);
            }

            // health regen
            healthRegBLvl = Math.Floor(healthReg + (0.14 * (int)lvlValue.Value));
            healthRegenValue.Text = Convert.ToString(healthRegBLvl);

            // mana regen

            // mov speed
        }
    }
}
