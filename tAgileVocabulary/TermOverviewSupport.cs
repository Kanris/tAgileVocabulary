using System.Windows;
using System.Windows.Controls;
using TermDBHandlerLibrary;
namespace tAgileVocabulary
{
    public class TermOverviewSupport
    {
        private TermDBHandler db;
        private Term editTerm;
        private TermOverviewOperation operation; //Операция

        public TermOverviewSupport(TermOverviewOperation operation, Term editTerm = null)
        {
            db = new TermDBHandler();

            this.operation = operation;
            this.editTerm = editTerm;
        }

        public void ChangeWindowType(Button btnRemoveTask, params TextBox[] textBoxes)
        {
            if (operation == TermOverviewOperation.ADD)
            {
                btnRemoveTask.Visibility = Visibility.Collapsed;

            }
            else
            {
                textBoxes[0].Text = editTerm.term;

                textBoxes[1].Text = editTerm.transcription.Substring(1, editTerm.transcription.Length - 2);
                textBoxes[2].Text = editTerm.interpretation;
            }
        }

        public void SaveTerm(Window termOverview, params TextBox[] textBoxes)
        {
            if (textBoxes[0].Text.Length > 0)
            {
                if (operation == TermOverviewOperation.ADD)
                {

                    Term newTerm = createTerm(textBoxes);
                    db.addTerm(newTerm);

                }
                else
                {
                    Term editedTerm = createTerm(textBoxes);
                    editedTerm.termID = editTerm.termID;

                    db.updateTerm(editedTerm);
                }

                termOverview.DialogResult = true;
                termOverview.Close();

            }
            else
            {
                string message = Properties.Resources.TermFieldEmpty;
                MessageBox.Show(message);
            }
        }

        public void RemoveTerm(Window termOverview)
        {
            db.removeTerm(editTerm.termID);

            termOverview.DialogResult = true;
            termOverview.Close();
        }

        public void TranscriptionButtonPress(TextBox txTranscription, object sender)
        {
            string content = (sender as Button).Content.ToString();
            txTranscription.Text += content;
        }

        private Term createTerm(params TextBox[] textBoxes)
        {
            string term = textBoxes[0].Text;
            string transcription = textBoxes[1].Text;
            string interpretation = textBoxes[2].Text;

            return new Term() { term = term, transcription = transcription, interpretation = interpretation };
        }
    }
}
