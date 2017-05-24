using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace TermDBHandlerLibrary
{
    public class TermDBHandler
    {
        static SqlConnection dbConnection; //Подключение к БД
        static SqlCommand sqlCommand;

        private Term convertFromReaderToTerm(SqlDataReader dr)
        {
            Term newTerm = new Term() { termID = dr[0].ToString(), term = dr[1].ToString(), transcription = dr[2].ToString(), interpretation = dr[3].ToString() };

            newTerm.transcription = "[" + newTerm.transcription + "]";

            if (newTerm.interpretation.Length > 40) newTerm.interpretation = newTerm.interpretation.Substring(0, 40) + "...";


            return newTerm;
        }

        public TermDBHandler()
        {
            if (dbConnection == null)
            {
                string pathToDB = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\termsDB.mdf";

                string ConnectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30", pathToDB);
                dbConnection = new SqlConnection(ConnectionString);
                
                sqlCommand = new SqlCommand(); //Переменная для запросов к БД
                sqlCommand.Connection = dbConnection; //Подключение переменной к БД
            }

        }

        public List<Term> getTerms()
        {
            dbConnection.Open();

            sqlCommand.CommandText = "SELECT termID, term, transcription, interpretation FROM Terms ORDER BY term ASC";

            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<Term> listOfTerms = new List<Term>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listOfTerms.Add(convertFromReaderToTerm(dr));
                }
            }

            dbConnection.Close();

            return listOfTerms;
        }

        public List<Term> searchTerm(string term)
        {
            
            dbConnection.Open();

            sqlCommand.CommandText = "SELECT * FROM Terms WHERE LOWER(term) LIKE @term";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@term", "%" + term + "%");

            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<Term> listOfTerms = new List<Term>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listOfTerms.Add(convertFromReaderToTerm(dr));
                }
            }

            dbConnection.Close();

            return listOfTerms;
        }

        public Term getTerm(string termID)
        {
            dbConnection.Open();

            sqlCommand.CommandText = "SELECT * FROM Terms WHERE termID = @termID";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@termID", termID);

            SqlDataReader dr = sqlCommand.ExecuteReader();
            Term searchedTerm = null;

            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    List<string> termInfo = new List<string>();
                    for (int i = 0; i < dr.FieldCount; ++i)
                    {
                        dr[i].ToString();
                        termInfo.Add(dr[i].ToString());
                    }

                    searchedTerm = new Term() { termID = termInfo[0], term = termInfo[1], transcription = termInfo[2], interpretation = termInfo[3] };
                }
            }

            dbConnection.Close();

            return searchedTerm;
        }

        public void addTerm(Term newTerm)
        {
            dbConnection.Open();

            sqlCommand.CommandText = "INSERT INTO Terms(term, transcription, interpretation) VALUES(@term, @transcription, @interpretation)";

            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@term", newTerm.term);
            sqlCommand.Parameters.AddWithValue("@transcription", newTerm.transcription);
            sqlCommand.Parameters.AddWithValue("@interpretation", newTerm.interpretation);

            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }

        public void removeTerm(string termID)
        {
            dbConnection.Open();

            sqlCommand.CommandText = "DELETE FROM Terms WHERE termID=@id";

            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", termID);

            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();

        }

        public void updateTerm(Term updatedTerm)
        {
            dbConnection.Open();

            string termID = updatedTerm.termID;

            sqlCommand.CommandText = "UPDATE Terms SET term=@term, transcription = @transcription, interpretation = @interpretation WHERE termID=@termID";

            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@termID", updatedTerm.termID);
            sqlCommand.Parameters.AddWithValue("@term", updatedTerm.term);
            sqlCommand.Parameters.AddWithValue("@transcription", updatedTerm.transcription);
            sqlCommand.Parameters.AddWithValue("@interpretation", updatedTerm.interpretation);

            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }
    }
}
