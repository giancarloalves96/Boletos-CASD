using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Boletos_CASD
{
	/// <summary>
	/// Interação lógica para MainWindow.xam
	/// </summary>
	public partial class MainWindow : Window
	{
		DataManager dataManager = new DataManager();
		string monthOnMessage = "";

		public MainWindow()
		{
			InitializeComponent();

			HideAllGrids();
		}

		private void HideAllGrids()
		{
			emailGrid.Visibility = Visibility.Hidden;
			databaseGrid.Visibility = Visibility.Hidden;
		}

		private void Show(Grid thisTimeGrid)
		{
			HideAllGrids();
			thisTimeGrid.Visibility = Visibility.Visible;
		}

		private void Retitle(string newName)
		{
			mainLabel.Content = newName;
		}

		private string BrowseFile(string defaultExt, string filter)
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			// Set filter for file extension and default file extension 
			dlg.DefaultExt = defaultExt;
			dlg.Filter = filter;

			// Display OpenFileDialog by calling ShowDialog method 
			bool? result = dlg.ShowDialog();

			// Get the selected file name and display in a TextBox 
			if (result == true)//.HasValue && result.Value)
			{
				// Open document 
				return dlg.FileName;
			}

			return null;
		}

		// Button's functions
		private void GoHome (object sender, RoutedEventArgs e)
		{
			HideAllGrids();
			Retitle("Boletos CASD");
		}

		private void CloseApplication(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void ShowDatabaseGrid (object sender, RoutedEventArgs e)
		{
			Show(databaseGrid);
			Retitle("Nova database");
		}

		private void ShowEmailGrid (object sender, RoutedEventArgs e)
		{
			Show(emailGrid);
			Retitle("Enviar Emails");
		}

		private void ShowAnalisisGrid(object sender, RoutedEventArgs e)
		{
			//Show(emailGrid);
			Retitle("Enviar Emails");
		}

		private void ShowResearchGrid(object sender, RoutedEventArgs e)
		{
			//Show(researchGrid);
		}

		private void ReplaceMonthOnMessage(object sender, SelectionChangedEventArgs e)
		{
			if (CB_month_emailGrid != null)
			{
				if ( ((sender as ComboBox).SelectedItem as ComboBoxItem).Content == null )
					return;

				monthOnMessage = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();

				if (txt_message.Text.Contains("#mes"))
					txt_message.Text = txt_message.Text.Replace("#mes", monthOnMessage.ToUpper());
				if (txt_message.Text.Contains("#MES"))
					txt_message.Text = txt_message.Text.Replace("#MES", monthOnMessage.ToUpper());
			}
		}

		private void BrowseButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender == browseButton1)
			{
				browseTextBox1.Text = BrowseFile(".pdf", "Pdf Files|*.pdf");
			}
			else if (sender == browseButton2)
			{
				browseTextBox2.Text = BrowseFile(".xls|.*", "Excel Worksheets | *.xls|All Files|*.*");
			}
		}

		private void SendEmail(object sender, RoutedEventArgs e)
		{
			string subject = txt_subject.Text;
			string message = txt_message.Text;
			if (txt_subject == null)
			{
				MessageBox.Show("Você deve preencher o assunto");
				return;
			} else if (txt_subject.Text.Length == 0)
			{
				MessageBox.Show("Você deve preencher o assunto");
				return;
			}

			if (!dataManager.IsThereADatabaseConnection() || !dataManager.IsDatabasesMatching(monthOnMessage))
			{
				MessageBox.Show("Sem conexão com uma base de dados");
				return;
			}

			List<string> names = dataManager.GetData("nome");
			List<string> emails = dataManager.GetData("email");
			List<string> boletos = dataManager.GetData("boleto");

			foreach (string name in names)
			{
				// Replace the personalization expression by the first name
				message = message.Replace(txt_person.Text, name.Substring(0, name.IndexOf(" ")));

				// Enviar e-mail com:
				// - assunto subject, 
				// - texto message 
				// - para o e-mail emails.ElementAt(names.FindIndex(n => n == name) - 1)
				// - anexado o arquivo achado em boletos.ElementAt(names.FindIndex(n => n == name) - 1)
			}
		}

		private void CreateDatabase(object sender, RoutedEventArgs e)
		{
			// Mount database's name
			string databaseName = CB_month_databaseGrid.Text + CB_year_databaseGrid.Text;
			
			// Create database file
			dataManager.CreateDatabase(databaseName);

			// Separar os PDFs e colocar em "Data\\" + databaseName

			string[] PDFpaths = System.IO.Directory.GetFiles("Data\\" + databaseName);

			// Cria um dataset para as informações antes de passar pra database
			List<Aluno> alunos = new List<Aluno>();

			// Se o nome dos PDFs separados já forem o nome dos alunos, dá pra fazer direto pelo filePath
			foreach (string s in PDFpaths)
			{
				alunos.Add(new Aluno(
								alunos.Count + 1,
								s.Substring(s.LastIndexOf('\\'), s.LastIndexOf('.')),
								"Sem e-mail registrado",
								s)
							);
			}

			// Pegar os nomes e e-mails da planilha e colocar em
			List<string> names = new List<string>();
			List<string> emails = new List<string>();

			// Percorre a lista de alunos montada com a lista dos PDFs e vai incrementando
			foreach (Aluno a in alunos)
			{
				if (names.Contains(a.nome))
				{
					int index = names.FindIndex(n => n == a.nome);
					a.email = emails[index];
					names.RemoveAt(index);
					emails.RemoveAt(index);
				}
			}


			// 
		}


		// Prototypes

		private void DataTest(object sender, RoutedEventArgs e)
		{
			DataManager.DataTest();
		}

		private void GetNamesAndPagesFromPDF()
		{
			//string filename = BrowseFile();
			//txtEditor2.Text = PDFManager.LerArquivo(filename);
		}

		private void SplitPages(object sender, RoutedEventArgs e)
		{
			// Pedir pra procurar arquivo
			//string filename = BrowseFile();

			// Pedir pra procurar destino
			// Separar por nomes
			// Colocar nome da pessoa no PDF ou guardar o file path associado à pessoa
			
				//PDFManager.SplitPages(filename);
				MessageBox.Show("Páginas separadas!");
		}

		// Command functions
		private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			txt_message.Text = "";
		}
	}
}