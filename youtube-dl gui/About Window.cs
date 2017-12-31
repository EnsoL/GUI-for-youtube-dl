using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtube_dl_gui
{
    public partial class About : Form
    {
        public About(String version)
        {
            InitializeComponent();
            // Set the window text
            aboutText.Text =
"  This is a GUI for youtube-dl. The current version of youtube-dl is " + version + @" You can find get more information about youtube-dl here: https://rg3.github.io/youtube-dl/.

    The UI was made by Luka Colić. It is released into the public domain through the following license: http://unlicense.org/.";
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
