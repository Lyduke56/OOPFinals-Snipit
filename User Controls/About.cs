using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnipIt.Managers
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        public Dashboard about
        {
            get => default;
            set
            {
            }
        }

        internal Dashboard Dashboard
        {
            get => default;
            set
            {
            }
        }
    }
}
