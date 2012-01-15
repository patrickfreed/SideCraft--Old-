using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SideCraft.UI {
    public partial class SideCraftMainMenu : Form {
        public SideCraftMainMenu() {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("background.png");
        }

        private void btnStartGame_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
