using System.Windows;
using System.Windows.Controls;
using TermDBHandlerLibrary;

namespace tAgileVocabulary
{
    /// <summary>
    /// Interaction logic for TermOverviewWindow.xaml
    /// </summary>
    public partial class TermOverviewWindow : Window
    {
        TermDBHandler db = new TermDBHandler();

        TermOverviewOperation operation; //Операция
        Term editTerm;

        public TermOverviewWindow(TermOverviewOperation operation, Term editTerm)
        {
            InitializeComponent();

            this.operation = operation;
            this.editTerm = editTerm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (operation == TermOverviewOperation.ADD)
            {
                btnRemoveTask.Visibility = Visibility.Collapsed;

            } else
            {
                txTerm.Text = editTerm.term;

                txTranscription.Text = editTerm.transcription.Substring(1, editTerm.transcription.Length - 2);
                txInterpretation.Text = editTerm.interpretation;
            }
        }

        private void btnRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            db.removeTerm(editTerm.termID);

            this.DialogResult = true;
            this.Close();
        }

        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            if (txTerm.Text.Length > 0)
            {
                if (operation == TermOverviewOperation.ADD)
                {

                    Term newTerm = createTerm();
                    db.addTerm(newTerm);

                }
                else
                {
                    Term editedTerm = createTerm();
                    editedTerm.termID = editTerm.termID;

                    db.updateTerm(editedTerm);
                }

                this.DialogResult = true;
                this.Close();

            } else
            {
                string message = Properties.Resources.TermFieldEmpty;
                MessageBox.Show(message);
            }
            
        }

        private Term createTerm()
        {
            string term = txTerm.Text;
            string transcription = txTranscription.Text;
            string interpretation = txInterpretation.Text;

            return new Term() { term = txTerm.Text, transcription = txTranscription.Text, interpretation = txInterpretation.Text };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            txTranscription.Text += content;
        }
    }
}
