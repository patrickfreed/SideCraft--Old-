using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SideCraft.menu;
using System.Windows.Forms.VisualStyles;

namespace SideCraft.menu {
    public enum Screens {
        MAIN_MENU,
        SETTINGS
    }
    
    public partial class SideCraftMainMenu : Form {
        private Dictionary<Screens, Control> controls;
        
        public SideCraftMainMenu() {
            InitializeComponent();
            controls = new Dictionary<Screens, Control>();
            controls.Add(Screens.MAIN_MENU, new MainMenuScreen());
            controls.Add(Screens.SETTINGS, new SettingsMenuScreen());
            this.BackgroundImage = Image.FromFile("background.png");
        }

        public void showScreen(Screens s) {
            Control c = controls[s];
            
            c.Parent = this;
            c.Dock = DockStyle.Fill;
            c.BringToFront();
            c.Show();
        }

        public Control getScreen(Screens s) {
            return controls[s];
        }

        public void startGame() {
            SettingsMenuScreen set = (SettingsMenuScreen)getScreen(Screens.SETTINGS);
            Settings.BLOCK_SIZE = int.Parse(set.txtBlockSize.Text);
            
            
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SideCraftMainMenu_Load(object sender, EventArgs e) {
           showScreen(Screens.MAIN_MENU);
        }
    }
}