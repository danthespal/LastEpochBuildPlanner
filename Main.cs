using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LastEpochBuildPlanner
{
    public partial class Main : Form
    {
        // declare global variables
        private string name, selected;
        private int lvl, str, dex, intel, att, vit, manaReg, movSpeed, dodge, armour, armourBPoint;
        private double healthReg, wardRet, health, healthRegBPoint, mana;

        public Main()
        {
            InitializeComponent();
            LoadClass();
            UpdateClass();
        }

        private void LoadClass()
        {
            // selected item is trimed of '-' and ' ' (space)
            char[] charsToTrim = { '-' };
            selected = classList.Text.Trim(charsToTrim).ToLower().Replace(" ", "");

            // load and
            // store default values based on class
            XElement selectedClass = XElement.Load(@"Data\classes.xml");
            IEnumerable<XElement> Class =
                from cls in selectedClass.Elements("class")
                where (string)cls.Element("Name") == selected
                select cls;
            foreach (XElement cls in Class)
            {
                name = classList.Text.Trim(charsToTrim).ToUpper();
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
                movspeedValue.Text = Convert.ToString(movSpeed + "%");
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
                movspeedValue.Text = Convert.ToString(movSpeed + "%");
            }
            else
            {
                passivesBtn.Enabled = true;

                classPicBox.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(selected);
                classStr.Text = name.ToUpper();
                strValue.Text = Convert.ToString(str);
                dexValue.Text = Convert.ToString(dex);
                intValue.Text = Convert.ToString(intel);
                attValue.Text = Convert.ToString(att);
                vitValue.Text = Convert.ToString(vit);
                healthValue.Text = Convert.ToString(health);
                manaValue.Text = Convert.ToString(mana);
                healthRegenValue.Text = Convert.ToString(healthReg);
                manaRegenValue.Text = Convert.ToString(manaReg);
                movspeedValue.Text = Convert.ToString(movSpeed + "%");
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
                armourBPoint = str * 5;
                armour += armour * (armourBPoint / 100);
                armourValue.Text = Convert.ToString(armour);
            }
            else
            {
                armourValue.Text = Convert.ToString(armour);
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
                dodgeValue.Text = Convert.ToString(dodge);
            }

            if (intel > 0)
            {
                // intelligence is one of the Character Stats which increases Ward Retention by 4% per point
                int wardRetBpoint = intel * 4;
                wardRet += wardRet * (wardRetBpoint / 100);
                wardRetValue.Text = Convert.ToString(wardRet + "%");
            }
            else
            {
                wardRetValue.Text = Convert.ToString(wardRet + "%");
            }

            if (vit > 0)
            {
                // vitality is one of the Character Stats which grants 10 health and 2% increased health regen.
                health += vit * 10;

                healthRegBPoint = vit * 2;
                healthReg += Math.Floor(healthReg * (healthRegBPoint / 100));

                healthValue.Text = Convert.ToString(health);
                healthRegenValue.Text = Convert.ToString(healthReg);
            }
            else
            {
                healthValue.Text = Convert.ToString(health);
                healthRegenValue.Text = Convert.ToString(healthReg);
            }

            if (classList.Text == "Druid")
            {
                double classBonusH = health * 0.2f;
                double totalHealth = Math.Floor(health + classBonusH);

                double classBonusM = mana * 0.2f;
                double totalMana = Math.Floor(mana + classBonusM);

                healthValue.Text = Convert.ToString(totalHealth);
                manaValue.Text = Convert.ToString(totalMana);
            }
            else
            {
                manaValue.Text = Convert.ToString(mana);
                healthValue.Text = Convert.ToString(health);
            }
        }

        private void passivesBtn_Click(object sender, EventArgs e)
        {
            // new form, passive points panel
            Passive passiveForm = new Passive
            {
                className = classList.Text.Trim(new char[] { '-', ' ' }).ToLower().Replace(" ", "")
            };
            passiveForm.Show();
        }

        // update values based on selected class
        private void classList_TextChanged(object sender, EventArgs e)
        {
            LoadClass();
            UpdateClass();
        }

        private void lvlValue_ValueChanged(object sender, EventArgs e)
        {
            lvl = (int)lvlValue.Value;
            double healthBLvl, druidHBonus, healthBonus, manaBLvl, druidMBonus, manaBonus, healthRegBLvl;

            if (classList.SelectedItem == null || string.IsNullOrEmpty(classList.SelectedItem.ToString()))
            {
                lvl = 1;
            }

            // health
            // the character gaining 8 points of Health for each level they have.
            if (selected == "druid")
            {
                healthBLvl = (8 * lvl) - 8 + health;

                druidHBonus = healthBLvl * 0.2f;
                healthBonus = Math.Floor(healthBLvl + druidHBonus);

                healthValue.Text = Convert.ToString(healthBonus);
            }
            else
            {
                healthBLvl = (8 * lvl) - 8 + health;
                healthValue.Text = Convert.ToString(healthBLvl);
            }

            // mana
            if (selected == "druid")
            {
                manaBLvl = (0.5f * lvl) + mana;

                druidMBonus = manaBLvl * 0.2f;
                manaBonus = Math.Floor(manaBLvl + druidMBonus);

                manaValue.Text = Convert.ToString(manaBonus);
            }
            else
            {
                manaBLvl = Math.Floor((0.5f * lvl) + mana);
                manaValue.Text = Convert.ToString(manaBLvl);
            }

            // health regen
            healthRegBLvl = Math.Floor((0.14f * lvl) + healthReg);
            healthRegenValue.Text = Convert.ToString(healthRegBLvl);
        }
    }
}
