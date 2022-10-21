using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ServiceCheckList
{
    class ContrastColor
    {
        public static Color ButtonActiveColor
        {
            get { return SystemInformation.HighContrast ? Color.White : Color.White; }
        }

        public static Color LabelActiveColor
        {
            get { return SystemInformation.HighContrast ? Color.Black : Color.Black; }
        }

        public static Color UserControlLabelActiveColor
        {
            get { return SystemInformation.HighContrast ? Color.Black : Color.Black; }
        }

        public static Bitmap TickBackGroundImage
        {
            get { return SystemInformation.HighContrast ? ServiceCheckList.Properties.Resources.ckeck_white : ServiceCheckList.Properties.Resources.ckeck_black; }
        }


    }
}
