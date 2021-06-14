using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LastEpochBuildPlanner
{
    public partial class Passive : Form
    {
        public string name;

        public string className
        {

            get { return name; }
            set { name = value; }
        }

        public Passive()
        {
            InitializeComponent();
        }

        private void Passive_Load(object sender, EventArgs e)
        {
            string classBkg = name + "_passive";
            primalistPassivePanel.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(classBkg);
        }
    }
}
