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
		string inputPath = "";
		string outputPath = "";

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

		private string BrowseFile()
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".pdf";
			dlg.Filter = "Pdf Files|*.pdf";

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

		private void SendEmail(object sender, RoutedEventArgs e)
		{
			Retitle("Enviar Emails");
		}

		private void ShowSearchGrid(object sender, RoutedEventArgs e)
		{
			//Show(emailGrid);
			Retitle("Enviar Emails");
		}

		private void BrowseButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender == browseButton1)
			{
				inputPath = BrowseFile();
				BrowseTextBox1.Text = inputPath;
			}
			else if (sender == browseButton2)
			{
				outputPath = BrowseFile();
				BrowseTextBox1.Text = outputPath;
			}
		}

		private void CreateDatabase(object sender, RoutedEventArgs e)
		{

		}


		// Prototypes


		private void DataTest(object sender, RoutedEventArgs e)
		{
			DataManager.DataTest();
		}

		private void GetNamesAndPagesFromPDF()
		{
			string filename = BrowseFile();
			//txtEditor2.Text = PDFManager.LerArquivo(filename);
		}

		private void SplitPages(object sender, RoutedEventArgs e)
		{
			// Pedir pra procurar arquivo
			string filename = BrowseFile();

			// Pedir pra procurar destino
			// Separar por nomes
			// Colocar nome da pessoa no PDF ou guardar o file path associado à pessoa

			if (filename != "")
			{
				//PDFManager.SplitPages(filename);
				MessageBox.Show("Páginas separadas!");
			}
		}

		// Command functions
		private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			txt_email.Text = "";
		}

	}
}