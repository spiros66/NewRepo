//*************************************************************************************************
// Assembly         : Sillogos v2.0
// Author           : ������
// Created          : 01-04-2023
//
// Last Modified By : ������
// Last Modified On : 01-04-2023
// Description      : 
//
// Copyright        : (c) Spiros. All rights reserved.
//*************************************************************************************************
using System.Windows;
using Sillogos.ViewModels;

namespace Sillogos.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainWindowViewModel();
		}
	}
}
