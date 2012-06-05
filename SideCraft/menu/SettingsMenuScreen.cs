using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SideCraft.menu {
    public partial class SettingsMenuScreen : UserControl {
        public SettingsMenuScreen() {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            Program.menu.showScreen(Screens.MAIN_MENU);
        }
    }
}
