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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private DataGridHelper dghelper;

        public MainWindow()
        {
            InitializeComponent();
            dghelper = new DataGridHelper();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dghelper.updatedgOverview(dgOverview); //обновляем dgOverview
        }


        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            MainWindowSupport.AddNewTerm(dghelper, dgOverview);
        }

        private void dgOverview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var searchWord = txSearch.Text;
                dghelper.SearchTerm(searchWord, dgOverview);
            }
        }

        private void txSearch_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MainWindowSupport.GotFocus(sender);
        }

        private void txSearch_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MainWindowSupport.LostFocus(sender);
        }
    }
}
