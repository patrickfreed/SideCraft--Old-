using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SideCraft.menu {
    public partial class MainMenuScreen : UserControl {
        public MainMenuScreen() {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("background.png");
        }

        private void btnStartGame_Click(object sender, EventArgs e) {
            Program.menu.startGame();
        }

        private void button1_Click(object sender, EventArgs e) {
            Program.menu.showScreen(Screens.SETTINGS);
        }
    }
}
