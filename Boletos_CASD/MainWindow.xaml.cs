using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Boletos_CASD
{
	/// <summary>
	/// Interação lógica para MainWindow.xam
	/// </summary>
	public partial class MainWindow : Window
	{
		DataManager dataManager = new DataManager();
		Dictionary<string, KeyValuePair<string, string>> alunosData;
		List<Aluno> alunosInfo = new List<Aluno>();
		public static string replacerWord;
		public static string emailUser;
		public static string password;
		string monthOnMessage = "";
		string currentFolderPath = "";

		public MainWindow()
		{
			InitializeComponent();

			GoHome();

			emailUser = emailToUse.Text = ConfigurationManager.AppSettings["emailUser"];
			password = passwordBox.Password = ConfigurationManager.AppSettings["emailPassword"];
		}

		private void HideAllGrids()
		{
			emailGrid.Visibility = Visibility.Hidden;
			databaseGrid.Visibility = Visibility.Hidden;
			researchGrid.Visibility = Visibility.Hidden;
			propertiesGrid.Visibility = Visibility.Hidden;
			logoCasd.Visibility = Visibility.Hidden;
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
			logoCasd.Visibility = Visibility.Visible;
		}

		private void GoHome()
		{
			HideAllGrids();
			Retitle("Boletos CASD");
			logoCasd.Visibility = Visibility.Visible;
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
			if (currentFolderPath != null)
			{
				if (currentFolderPath.Length > 0)
				{
					Show(emailGrid);
					Retitle("Enviar Emails");
					return;
				}
			}

			MessageBox.Show("Por favor, carregue os dados");
		}

		private void ShowEmailGrid()
		{
			if (currentFolderPath != null)
			{
				if (currentFolderPath.Length > 0)
				{
					Show(emailGrid);
					Retitle("Enviar Emails");
					return;
				}
			}

			MessageBox.Show("Por favor, carregue os dados");
		}

		private void ShowPropertiesGrid(object sender, RoutedEventArgs e)
		{
			Show(propertiesGrid);
			Retitle("Configurações");
		}

		private void ShowResearchGrid(object sender, RoutedEventArgs e)
		{
			if (currentFolderPath != null)
			{
				if (currentFolderPath.Length > 0)
				{
					Show(researchGrid);
					Retitle("Pesquisa de dados");
					//System.Diagnostics.Process.Start(currentFolderPath);
					return;
				}
			}

			MessageBox.Show("Por favor, carregue os dados");
		}

		private void ReplaceMonthOnMessage(object sender, SelectionChangedEventArgs e)
		{
			if (CB_month_emailGrid != null)
			{
				if (((sender as ComboBox).SelectedItem as ComboBoxItem).Content == null)
					return;

				monthOnMessage = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();

				if (txt_message.Text.Contains("#mes"))
					txt_message.Text = txt_message.Text.Replace("#mes", monthOnMessage.ToUpper());
				if (txt_message.Text.Contains("#MES"))
					txt_message.Text = txt_message.Text.Replace("#MES", monthOnMessage.ToUpper());
			}
		}

		private void ReplaceMonthOnMessage()
		{
			if (CB_month_emailGrid != null)
			{
				if (CB_month_emailGrid.Text == null)
					return;

				monthOnMessage = CB_month_databaseGrid.Text;

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
				//MessageBox.Show(browseTextBox1.Text);
			}
			else if (sender == browseButton2)
			{
				browseTextBox2.Text = BrowseFile(".xlsx", "Excel 2010|*.xlsx| Excel|*.xls| All Files|*.*");
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

			if (CB_month_emailGrid.Text == "Mês")
			{
				MessageBox.Show("Informe um mês!");
				return;
			}

			if (CB_year_emailGrid.Text == "Ano")
			{
				MessageBox.Show("Informe um ano!");
				return;
			}

			if (alunosData == null)
			{
				MessageBox.Show("Por favor, carregue os dados dos alunos referente ao mês desejado");
				return;
			}

			if (alunosData.Count == 0)
			{
				MessageBox.Show("Não há informações na base de dados carregada!");
				return;
			}

			if (currentFolderPath == null || currentFolderPath == "")
			{
				MessageBox.Show("Por favor, carregue os dados dos alunos referente ao mês desejado");
				return;
			}

			replacerWord = txt_person.Text;

			EmailSender.SendMailList(alunosData, txt_subject.Text, txt_message.Text);

			MessageBox.Show("Emails enviados!");
		}

		private void CreateDatabase(object sender, RoutedEventArgs e)
		{

			string PDFpath = browseTextBox1.Text;
			string sheetPath = browseTextBox2.Text;

			if (CB_month_databaseGrid.Text == "Mês")
			{
				MessageBox.Show("Informe um mês!");
				return;
			}

			if (CB_year_databaseGrid.Text == "Ano")
			{
				MessageBox.Show("Informe um ano!");
				return;
			}

			if (PDFpath == "" || PDFpath == null)
			{
				MessageBox.Show("O diretório dos boletos deve ser informado");
				return;
			}

			if (sheetPath == "" || sheetPath == null)
			{
				MessageBox.Show("O diretório da planilha com os e-mails deve ser informado");
				return;
			}

			// Mount database's name
			string databaseName = CB_month_databaseGrid.Text + CB_year_databaseGrid.Text;

			// Get the actual path to this application
			string appPath = AppDomain.CurrentDomain.BaseDirectory;  // Type 1
			//string appPath = Directory.GetCurrentDirectory();  // Type 2

			// Mount the path to the database folder
			string folderPath = appPath + "Data\\" + databaseName;
			currentFolderPath = folderPath;

			// Create folder if it does not exist
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

            // Separar os PDFs e colocar em "Data\\" + databaseName <<
            Dictionary<string, string> pdfOutput = PDFManager.GeneratePages(PDFpath, folderPath);

            Dictionary<string, string> excelOutput = ExcelParser.ParseMails(sheetPath);

            alunosData = Linker.linkPdfAndExcel(pdfOutput, excelOutput, out List<string> notFound,
                out List<string> notPresent);

            var names = alunosData.Keys.ToList();
            var bol_emails = alunosData.Values.ToList();

            for (int i = 0; i < names.Count; i++)
            {
                alunosInfo.Add(new Aluno() { Id = i, Nome = names[i], Email = bol_emails[i].Value });
            }

            dataGrid.ItemsSource = alunosInfo;

            //dataManager.InsertIntoCurrentDatabase(alunosData);

            // Cria um txt com os nomes e emails que sobraram
            string txtPath = folderPath + "\\Emails nao encontrados.txt";

            //System.IO.File.Create(txtPath);
            File.WriteAllLines(txtPath, notFound);

            // Cria um txt com os nomes que nao possuem boletos
            txtPath = folderPath + "\\Boletos nao encontrados.txt";
            File.WriteAllLines(txtPath, notPresent);

            CB_month_emailGrid.Text = CB_month_databaseGrid.Text;
            ReplaceMonthOnMessage();

            ShowEmailGrid();
        }

		// Prototypes

		//private void DataTest(object sender, RoutedEventArgs e)
		//{
		//	DataManager.DataTest();
		//}


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
