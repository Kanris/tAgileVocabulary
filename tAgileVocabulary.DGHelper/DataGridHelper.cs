using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TermDBHandlerLibrary;

namespace tAgileVocabulary.DGHelper
{
    public class DataGridHelper
    {
        private TermDBHandler db;

        public DataGridHelper()
        {
            db = new TermDBHandler();
        }

        public bool updatedgOverview(DataGrid dgOverview, List<Term> listOfTerms = null)
        {
            try
            {
                if (listOfTerms == null)
                    listOfTerms = db.getTerms();

                dgOverview.Items.Clear(); //Очищаем все строки
                
                foreach(var item in addTermsToDG(listOfTerms))
                {
                    dgOverview.Items.Add(item);
                }

                actualDataGridSize(dgOverview);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception message: \n {e.Message}", "Can't open db", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

        }

        public void SearchTerm(string txSearch, DataGrid dgOverview)
        {
            List<Term> searchedTerms = db.searchTerm(txSearch);

            if (searchedTerms.Count > 0)
            {
                updatedgOverview(dgOverview, searchedTerms); //обновляем dgOverview

            }
            else
            {
                dgOverview.Focus();
                
                MessageBox.Show("The search has not given any results");
            }
        }

        private IEnumerable<Term> addTermsToDG(List<Term> listOfTerms)
        {
            foreach (Term newRow in listOfTerms)
            {
                yield return newRow;
            }
        }

        private void actualDataGridSize(DataGrid dgOverview)
        {
            if (dgOverview.Items.Count > 0)
            {
                for (int i = 1; i < dgOverview.Columns.Count; ++i)
                {
                    dgOverview.Columns[i].Width = new DataGridLength(1.0, DataGridLengthUnitType.SizeToCells);
                }
            }

        }
    }
}
