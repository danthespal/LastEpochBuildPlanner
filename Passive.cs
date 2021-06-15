using System;
using System.Drawing;
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
