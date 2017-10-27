using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;

namespace Boletos_CASD
{
	class DataManager
	{
		// We use these three SQLite objects:
		SQLiteConnection sqlite_conn;
		SQLiteCommand sqlite_cmd;
		SQLiteDataReader sqlite_datareader;

		public void CreateDatabase(string databaseName)
		{
			// Verify if it's a valid filepath

			// Verify if it already exists

			// if (yes) ask for overswritting
			// if no
			bool overwrite = false;

			// create a new database connection without overwritting:
			sqlite_conn = new SQLiteConnection("Data Source=Data\\" + databaseName + ".db;Version=3;New=" + overwrite.ToString() + ";Compress=True;");

			// open the connection:
			sqlite_conn.Open();

			// create a new SQL command:
			sqlite_cmd = sqlite_conn.CreateCommand();
			
			// Let the SQLiteCommand object know our SQL-Query:
			sqlite_cmd.CommandText = "CREATE TABLE " + "ALUNOS" + " (id integer primary key, nome string, email string, boleto string);";

			// Now lets execute the SQL ;D
			sqlite_cmd.ExecuteNonQuery();
		}

		public void OpenDatabase(string databaseName)
		{
			// verify if the database exists

			// if not, return

			// create a new database connection:
			sqlite_conn = new SQLiteConnection("Data Source=Data\\" + databaseName + ".db;Version=3;New=False;Compress=True;");
			
			// open the connection:
			sqlite_conn.Open();

			// create a new SQL command:
			sqlite_cmd = sqlite_conn.CreateCommand();
		}

		public void InsertIntoCurrentDatabase(Aluno aluno)
		{
			// Lets insert something into our new table:
			sqlite_cmd.CommandText = "INSERT INTO " + "ALUNOS" + " (id, nome, email, boleto) VALUES (" + aluno.id.ToString() + ", '" + aluno.nome.ToString() + "', '" + aluno.email.ToString() + "', '" + aluno.boleto.ToString() + "');";

			// And execute this again ;D
			sqlite_cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Get all the data from the field info
		/// </summary>
		/// <param name="info">
		/// The name of the field to get data from</param>
		/// <returns>
		/// Returns a list of string with the info</returns>
		public List<string> GetData (string info)
		{
			List<string> output = new List<string>();

			// First lets build a SQL-Query again:
			sqlite_cmd.CommandText = "SELECT * FROM " + "ALUNOS";

			// Now the SQLiteCommand object can give us a DataReader-Object:
			sqlite_datareader = sqlite_cmd.ExecuteReader();

			// The SQLiteDataReader allows us to run through the result lines:
			while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
			{
				// Print out the content of the 'info' field:
				output.Add(sqlite_datareader[info].ToString());
			}

			return output;
		}

		public List<Aluno> GetDataByName (string name)
		{
			List<Aluno> output = new List<Aluno>();

			// First lets build a SQL-Query again:
			sqlite_cmd.CommandText = "SELECT * FROM " + "ALUNOS";

			// Now the SQLiteCommand object can give us a DataReader-Object:
			sqlite_datareader = sqlite_cmd.ExecuteReader();

			// The SQLiteDataReader allows us to run through the result lines:
			while(sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
			{
				if (sqlite_datareader["nome"].ToString().Contains(name))
				{
					Aluno aluno = new Aluno(
								sqlite_datareader["id"].ToString(),
								sqlite_datareader["nome"].ToString(),
								sqlite_datareader["email"].ToString(),
								sqlite_datareader["boleto"].ToString()
						);
					output.Add(aluno);
				}
			}

			return output;
		}

		public List<Aluno> GetDataByEmail (string email)
		{
			List<Aluno> output = new List<Aluno>();

			// First lets build a SQL-Query again:
			sqlite_cmd.CommandText = "SELECT * FROM " + "ALUNOS";

			// Now the SQLiteCommand object can give us a DataReader-Object:
			sqlite_datareader = sqlite_cmd.ExecuteReader();

			// The SQLiteDataReader allows us to run through the result lines:
			while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
			{
				if (sqlite_datareader["email"].ToString().Contains(email))
				{
					Aluno aluno = new Aluno(
								sqlite_datareader["id"].ToString(),
								sqlite_datareader["nome"].ToString(),
								sqlite_datareader["email"].ToString(),
								sqlite_datareader["boleto"].ToString()
						);
					output.Add(aluno);
				}
			}

			return output;
		}

		public List<Aluno> GetDataById (string id)
		{
			List<Aluno> output = new List<Aluno>();

			// First lets build a SQL-Query again:
			sqlite_cmd.CommandText = "SELECT * FROM " + "ALUNOS";

			// Now the SQLiteCommand object can give us a DataReader-Object:
			sqlite_datareader = sqlite_cmd.ExecuteReader();

			// The SQLiteDataReader allows us to run through the result lines:
			while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
			{
				if (sqlite_datareader["id"].ToString().Equals(id))
				{
					Aluno aluno = new Aluno(
								sqlite_datareader["id"].ToString(),
								sqlite_datareader["nome"].ToString(),
								sqlite_datareader["email"].ToString(),
								sqlite_datareader["boleto"].ToString()
						);
					output.Add(aluno);
				}
			}

			return output;
		}

		public static void DataTest()
		{
			// We use these three SQLite objects:
			SQLiteConnection sqlite_conn;
			SQLiteCommand sqlite_cmd;
			SQLiteDataReader sqlite_datareader;

			string databaseName = "outubro";
			
			// create a new database connection:
			sqlite_conn = new SQLiteConnection("Data Source=Data\\" + databaseName + ".db;Version=3;New=True;Compress=True;");
			
			// open the connection:
			sqlite_conn.Open();

			// create a new SQL command:
			sqlite_cmd = sqlite_conn.CreateCommand();
			
			// Let the SQLiteCommand object know our SQL-Query:
			sqlite_cmd.CommandText = "CREATE TABLE " + "ALUNOS" + " (id integer primary key, text string);";

			// Now lets execute the SQL ;D
			sqlite_cmd.ExecuteNonQuery();

			// Lets insert something into our new table:
			sqlite_cmd.CommandText = "INSERT INTO " + "ALUNOS" + " (id, text) VALUES (599, \"Adriano\");";

			// And execute this again ;D
			sqlite_cmd.ExecuteNonQuery();

			// ...and inserting another line:
			sqlite_cmd.CommandText = "INSERT INTO " + "ALUNOS" + " (id, text) VALUES (234, \"Coisinha\");";

			// And execute this again ;D
			sqlite_cmd.ExecuteNonQuery();

			// But how do we read something out of our table ?
			// First lets build a SQL-Query again:
			sqlite_cmd.CommandText = "SELECT * FROM " + "ALUNOS";

			// Now the SQLiteCommand object can give us a DataReader-Object:
			sqlite_datareader = sqlite_cmd.ExecuteReader();

			// The SQLiteDataReader allows us to run through the result lines:
			while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
			{
				// Print out the content of the text field:
				System.Windows.MessageBox.Show(sqlite_datareader["id"].ToString() + " | " + sqlite_datareader.GetString(1));
			}
			
			// We are ready, now lets cleanup and close our connection:
			sqlite_conn.Close();
		}
	}
}
