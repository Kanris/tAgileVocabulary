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
        private TermOverviewSupport tOverviewSupport;

        public TermOverviewWindow(TermOverviewOperation operation, Term editTerm)
        {
            InitializeComponent();

            tOverviewSupport = new TermOverviewSupport(operation, editTerm);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tOverviewSupport.ChangeWindowType(btnRemoveTask, txTerm, txTranscription, txInterpretation);
        }

        private void btnRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            tOverviewSupport.RemoveTerm(this);
        }

        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            tOverviewSupport.SaveTerm(this, txTerm, txTranscription, txInterpretation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tOverviewSupport.TranscriptionButtonPress(txTranscription, sender);
        }
    }
}
