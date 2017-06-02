using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TermDBHandlerLibrary;

namespace tAgileVocabulary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updatedgOverview(null); //обновляем dgOverview
        }


        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            TermOverviewWindow termWindow = new TermOverviewWindow(TermOverviewWindow.OPERATION_ADD, null); //Вывозов окна добавления задачи

            if (termWindow.ShowDialog() == true)
            {
                updatedgOverview(null); //обновляем dgOverview
            }
        }

        private void dgOverview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgOverview.SelectedItem != null)
            {
                Term selectedTerm = dgOverview.SelectedItem as Term;

                int termID = Convert.ToInt32(selectedTerm.termID);

                if (termID > -1)
                {
                    TermOverviewWindow termWindow = new TermOverviewWindow(TermOverviewWindow.OPERATION_EDIT, selectedTerm); //Вывозов окна добавления задачи

                    if (termWindow.ShowDialog() == true)
                    {
                        updatedgOverview(null); //обновляем dgOverview
                    }
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<Term> searchedTerms = db.searchTerm(txSearch.Text);

                if (searchedTerms.Count > 0)
                {
                    updatedgOverview(searchedTerms); //обновляем dgOverview

                } else
                {
                    dgOverview.Focus();

                    MessageBox.Show(Properties.Resources.SearchFailed);
                }
                
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
