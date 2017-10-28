using System.Collections.Generic;
using System.IO;
using Finisar.SQLite;

namespace Boletos_CASD
{
	class DataManager
	{
		// We use these three SQLite objects:
		SQLiteConnection sqlite_conn;
		SQLiteCommand sqlite_cmd;
		SQLiteDataReader sqlite_datareader;

		string currentDatabaseName = "";

		public DataManager ()
		{
			//sqlite_conn = null;
			//sqlite_cmd = null;
			//sqlite_datareader = null;
		}

		public bool IsThereADatabaseConnection ()
		{
			if (sqlite_cmd == null || sqlite_conn == null)
			{
				return false;
			}
			else return true;
		}

		public bool IsDatabasesMatching (string matcher)
		{
			if (!IsThereADatabaseConnection())
				return false;
			if (matcher.ToLower() == currentDatabaseName.ToLower())
				return true;
			else return false;
		}

		public void CreateDatabase(string databaseName)
		{
			// Mount the path to the database folder
			string path = "Data\\" + databaseName;

			// Verify if it's a valid directory
			if (!Directory.Exists(path))
			{
				System.Windows.MessageBox.Show("O diretório não existe");
				System.Windows.MessageBox.Show("A pasta será criada");
				Directory.CreateDirectory(path);
			}

			// Mount the path to the database file
			path = path + "\\" + databaseName + ".db"

			// Verify if the current database file already exists
			if (File.Exists(path))
			{
				System.Windows.MessageBox.Show("Essa base de dados já existe!");
				return;
			}

			currentDatabaseName = databaseName;

			// if (yes) ask for overwritting <TO IMPLEMENT>
			// AsdForOverwritting() // Create new windows, ask, save answer and go ahead

			// if no
			bool overwrite = false;

			// create a new database connection without overwritting:
			sqlite_conn = new SQLiteConnection("Data Source=" + path + ";Version=3;New=" + overwrite.ToString() + ";Compress=True;");

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

			// Mount the path to the database file
			string path = "Data\\" + databaseName + "\\" + databaseName + ".db"

			// Verify if the current database file already exists
			if (!File.Exists(path))
			{
				System.Windows.MessageBox.Show("Essa base de dados não existe!");
				return;
			}

			currentDatabaseName = databaseName;

			// create a new database connection:
			sqlite_conn = new SQLiteConnection("Data Source=" + path + ";Version=3;New=False;Compress=True;");
			
			// open the connection:
			sqlite_conn.Open();

			// create a new SQL command:
			sqlite_cmd = sqlite_conn.CreateCommand();
		}

		public void InsertIntoCurrentDatabase(Aluno aluno)
		{
			if (sqlite_cmd == null || sqlite_conn == null)
			{
				System.Windows.MessageBox.Show("Não há conexão com a base de dados");
				return;
			}

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

		/// <summary>
		/// Finds and gets data from the database by name
		/// </summary>
		/// <param name="name">Name could be just part of the field name in the database </param>
		/// <returns></returns>
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

		/// <summary>
		/// Finds and gets data from the database by email
		/// </summary>
		/// <param name="email">Could be part of the field email</param>
		/// <returns></returns>
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

		/// <summary>
		/// Finds and gets data from the database by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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
