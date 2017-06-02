using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TermDBHandlerLibrary;
using tAgileVocabulary.DGHelper;

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

        public static void AddNewTerm(DataGridHelper dghelper, DataGrid dgOverview)
        {
            TermOverviewWindow termWindow = new TermOverviewWindow(TermOverviewOperation.ADD, null); //Вывозов окна добавления задачи

            if (termWindow.ShowDialog() == true)
            {
                dghelper.updatedgOverview(dgOverview); //обновляем dgOverview
            }
        }

        public static void EditTerm(DataGridHelper dghelper, DataGrid dgOverview)
        {
            if (dgOverview.SelectedItem != null)
            {
                Term selectedTerm = dgOverview.SelectedItem as Term;

                int termID = Convert.ToInt32(selectedTerm.termID);

                if (termID > -1)
                {
                    TermOverviewWindow termWindow = new TermOverviewWindow(TermOverviewOperation.EDIT, selectedTerm); //Вывозов окна добавления задачи

                    if (termWindow.ShowDialog() == true)
                    {
                        dghelper.updatedgOverview(dgOverview); //обновляем dgOverview
                    }
                }
            }
        }

    }
}
