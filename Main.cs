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
        private string name, selected;
        private int lvl, str, dex, intel, att, vit, manaReg, movSpeed, dodge, armour;
        private double baseHealthReg, wardRet, baseHealth, baseMana, fireValue, lightValue, coldValue, physValue, poisonValue, necroValue, voidValue;

        public Main()
        {
            InitializeComponent();
            LoadClass();
            UpdateClass();
        }

        private void LoadClass()
        {
            // selected item from comboBox is trimed of '-' and ' ' (space)
            selected = classList.Text.Trim(new char[] { '-' }).ToLower().Replace(" ", "");

            // load baseValues from xml file
            IEnumerable<XElement> enumerable()
            {
                foreach (var cls in XElement.Load(@"Data\classes.xml").Elements("class"))
                {
                    if ((string)cls.Element("name") == selected)
                    {
                        yield return cls;
                    }
                }
            }

            // store default values based on class
            foreach (XElement cls in enumerable())
            {
                name = classList.Text.Trim(new char[] { '-' }).ToUpper();
                lvl = Convert.ToInt32(cls.Element("level").Value);
                str = Convert.ToInt32(cls.Element("strength").Value);
                dex = Convert.ToInt32(cls.Element("dexterity").Value);
                intel = Convert.ToInt32(cls.Element("intelligence").Value);
                att = Convert.ToInt32(cls.Element("attunement").Value);
                vit = Convert.ToInt32(cls.Element("vitality").Value);
                baseHealth = Convert.ToInt32(cls.Element("health").Value);
                baseMana = Convert.ToInt32(cls.Element("mana").Value);
                baseHealthReg = Convert.ToInt32(cls.Element("healthreg").Value);
                manaReg = Convert.ToInt32(cls.Element("manareg").Value);
                movSpeed = Convert.ToInt32(cls.Element("movspd").Value);
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
                baseHealth = 0;
                baseMana = 0;
                baseHealthReg = 0;
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
                healthValue.Text = Convert.ToString(baseHealth);
                manaValue.Text = Convert.ToString(baseMana);
                healthRegenValue.Text = Convert.ToString(baseHealthReg);
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
                healthValue.Text = Convert.ToString(baseHealth);
                manaValue.Text = Convert.ToString(baseMana);
                healthRegenValue.Text = Convert.ToString(baseHealthReg);
                manaRegenValue.Text = Convert.ToString(manaReg);
                movspeedValue.Text = Convert.ToString(movSpeed + "%");
            }

            // update status based on class mastery bonus

            // beastmaster
            // +1 companion summon limit
            // you and your minion deal 50% increased melee damage

            // shaman
            // -5 totem mana cost
            // +10 attunement
            // 50% elemental resistance while you control a totem.
            if (selected == "shaman")
            {
                att += 10;
                attValue.Text = Convert.ToString(att);
            }
            else
            {
                attValue.Text = Convert.ToString(att);
            }

            // druid
            // when leaving a transformed state, you gain 70% damage reduction for 2 seconds
            // 20% increased health and mana
            if (selected == "druid")
            {
                baseHealth += Math.Floor(baseHealth * 0.2f);
                baseMana += Math.Floor(baseMana * 0.2f);

                healthValue.Text = Convert.ToString(baseHealth);
                manaValue.Text = Convert.ToString(baseMana);

            }
            else
            {
                manaValue.Text = Convert.ToString(baseMana);
                healthValue.Text = Convert.ToString(baseHealth);
            }

            // sorcerer
            // +50 mana
            // spells deal increased damage equal to their mana cost
            if (selected == "sorcerer")
            {
                baseMana += 50;
                manaValue.Text = Convert.ToString(baseMana);
            }
            else
            {
                manaValue.Text = Convert.ToString(baseMana);
            }

            // spellblade
            // 4 ward gained on melee hit
            // mana spent on melee attacks is converted to ward

            // void knight
            // 75% increased melee void damage
            // your melee attacks, throwing attacks and void spells have a 10% 
            // chance to be repeated by an echo 0.5s later
            // excludes movement abilities and anomaly

            //  forge guard
            // 35% physical and fire resitance
            // 3% increased armor for each hit you have taken in the last 10 seconds
            if (selected == "forgeguard")
            {
                physValue = 35;
                fireValue = 35;

                physRes.Text = $"{Convert.ToString(physValue)}%";
                fireRes.Text = $"{Convert.ToString(fireValue)}%";
            }

            // paladin
            // you deal increased fire and physical damage equal to your recent health remaining
            // 1% increased healing effectiveness per point of attunement

            // necromancer
            // +1 max skeleton
            // +1 max skeleton mage
            // your minions deal 50% increased damage

            // lich
            // 1% of damage dealt is leeched as health spells and melee attacks deal 
            // increased damage equal to your missing health

            // bladedancer
            // +1 max shadows
            // +15 melee physical damage
            // 15% more dodge rating (multiplicative with other modifiers)

            // marksman
            // using a bow attack grants 5% increased attack speed. can stack 5 times.
            // all stacks fall of after if you have not used a bow attack recently.
            // 50% increased damage while using a bow

            // update default values based on points
            if (att > 0)
            {
                // attunement is one of the Character Stats which grants 2 mana per point and improves skills
                // that rely on innate magic inside the character and its surroundings.
                baseMana += att * 2;
                manaValue.Text = Convert.ToString(baseMana);
            }
            else
            {
                manaValue.Text = Convert.ToString(baseMana);
            }

            if (str > 0)
            {
                // strength is one of the Character Stats which increases Armor by 5% per point
                armour += armour * (str * 5 / 100);
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
                wardRet += intel * 4;
                wardRetValue.Text = Convert.ToString($"{wardRet}%");
            }
            else
            {
                wardRetValue.Text = Convert.ToString($"{wardRet}%");
            }

            if (vit > 0)
            {
                // vitality is one of the Character Stats which grants 10 health and 2% increased health regen.
                baseHealth += vit * 10;

                baseHealthReg += Math.Floor(baseHealthReg * ((double)(vit * 2) / 100));

                healthValue.Text = Convert.ToString(baseHealth);
                healthRegenValue.Text = Convert.ToString(baseHealthReg);
            }
            else
            {
                healthValue.Text = Convert.ToString(baseHealth);
                healthRegenValue.Text = Convert.ToString(baseHealthReg);
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
            double healthBLvl, manaBLvl, healthRegBLvl, totalHealth, totalMana;

            if (classList.SelectedItem == null || string.IsNullOrEmpty(classList.SelectedItem.ToString()))
            {
                lvl = 1;
            }

            // health
            // the character gaining 8 points of Health for each level they have.
            if (selected == "druid")
            {
                healthBLvl = (8 * lvl) - 8;
                totalHealth = Math.Floor(healthBLvl + baseHealth + (healthBLvl * 0.2f));

                healthValue.Text = Convert.ToString(totalHealth);
            }
            else
            {
                totalHealth = (8 * lvl) - 8 + baseHealth;
                healthValue.Text = Convert.ToString(totalHealth);
            }

            // mana
            if (selected == "druid")
            {
                manaBLvl = (0.5f * lvl) - 0.5f;
                totalMana = Math.Floor(manaBLvl + baseMana + (manaBLvl * 0.2f));

                manaValue.Text = Convert.ToString(totalMana);
            }
            else
            {
                manaBLvl = Math.Floor((0.5f * lvl) + baseMana);
                manaValue.Text = Convert.ToString(manaBLvl);
            }

            // health regen
            healthRegBLvl = Math.Floor((0.14f * lvl) + baseHealthReg);
            healthRegenValue.Text = Convert.ToString(healthRegBLvl);
        }
    }
}
