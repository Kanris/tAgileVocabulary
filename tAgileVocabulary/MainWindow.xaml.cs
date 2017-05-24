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
        private TermDBHandler db = new TermDBHandler();

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

        //=======================================================================================================================
        private void addTermsToDG(List<Term> listOfTerms)
        {
            foreach(Term newRow in listOfTerms)
            {
                dgOverview.Items.Add(newRow);
            }
        }

        private void updatedgOverview(List<Term> listOfTerms)
        {
            try
            {
                if (listOfTerms == null)
                    listOfTerms = db.getTerms();

                dgOverview.Items.Clear(); //Очищаем все строки
                addTermsToDG(listOfTerms); //Заполняем информацией
                actualDataGridSize();
            } 
            catch (Exception e)
            {
                MessageBox.Show($"Exception message: \n {e.Message}", "Can't open db", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }

        private void actualDataGridSize()
        {
            if (dgOverview.Items.Count > 0)
            {
                for (int i = 1; i < dgOverview.Columns.Count; ++i)
                {
                    dgOverview.Columns[i].Width = new DataGridLength(1.0, DataGridLengthUnitType.SizeToCells);
                }
            }

        }

        private void txSearch_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
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

        private void txSearch_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
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
    }
}
