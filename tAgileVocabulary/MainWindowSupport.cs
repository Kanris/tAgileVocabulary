using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace tAgileVocabulary
{
    public static class MainWindowSupport
    {
        public static void LostFocus(object sender)
        {
            if (sender is TextBox)
            {
                //If nothing was entered, reset default text.
                if ((sender as TextBox).Text.Trim().Equals(""))
                {
                    (sender as TextBox).Foreground = Brushes.LightGray;
                    (sender as TextBox).Text = Properties.Resources.SearchText;
                }
            }
        }

        public static void GotFocus(object sender)
        {
            if (sender is TextBox)
            {
                //If nothing has been entered yet.
                if ((sender as TextBox).Foreground == Brushes.LightGray)
                {
                    (sender as TextBox).Text = "";
                    (sender as TextBox).Foreground = Brushes.WhiteSmoke;
                }
            }
        }



    }
}
