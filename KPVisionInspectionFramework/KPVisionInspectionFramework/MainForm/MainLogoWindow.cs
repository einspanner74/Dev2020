using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPVisionInspectionFramework
{
    public partial class MainLogoWindow : Form
    {
        public MainLogoWindow()
        {
            InitializeComponent();
        }

        public void Initialize(object _Owner)
        {
            this.Owner = (Form)_Owner;
        }

        public void SetWindowSize(Size _Size)
        {
            this.Size = _Size;
        }

        public void SetLogoImage(Image _BackgroundImage)
        {
            picLogoImage.BackgroundImage = _BackgroundImage;
        }
    }
}
