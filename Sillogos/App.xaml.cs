using System.Windows;
using Sillogos.Helpers;
using Sillogos.ViewModels;
using Sillogos.Views;

namespace Sillogos
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			RegisterMessageService();

            //ServiceLocatorCopilot.Register<IDialogServiceCopilot>(new DialogServiceCopilot());

            var mainWindowViewModel = new MainWindowViewModel();

            var mainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            mainWindow.Show();
        }

        /// <summary>
        /// Register the MessageBox show
        /// </summary>
        public void RegisterMessageService()
		{
			MessageService.ShowMessage = (msg) =>
			{
				MessageBox.Show(msg, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
			};
            MessageService.ShowErrorMessage = (msg) =>
            {
                MessageBox.Show(msg, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            };
			MessageService.ShowMessageWithConfirm = msg =>
			{
				return MessageBox.Show(msg, "delete", MessageBoxButton.YesNo);
			};
		}
	}
}
