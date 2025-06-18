//*************************************************************************************************
// Assembly         : Sillogos v2.0
// Author           : Óðýñïò
// Created          : 01-04-2023
//
// Last Modified By : Óðýñïò
// Last Modified On : 01-04-2023
// Description      : 
//
// Copyright        : (c) Spiros. All rights reserved.
//*************************************************************************************************

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sillogos.Helpers
{
	public static class Commands
	{
		public static readonly DependencyProperty DataGridDoubleClickProperty =
		  DependencyProperty.RegisterAttached("DataGridDoubleClickCommand", typeof(ICommand), typeof(Commands),
							new PropertyMetadata(AttachOrRemoveDataGridDoubleClickEvent));

		public static ICommand GetDataGridDoubleClickCommand(DependencyObject obj)
		{
			return (ICommand)obj.GetValue(DataGridDoubleClickProperty);
		}

		public static void SetDataGridDoubleClickCommand(DependencyObject obj, ICommand value)
		{
			obj.SetValue(DataGridDoubleClickProperty, value);
		}

		public static void AttachOrRemoveDataGridDoubleClickEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			DataGrid dataGrid = obj as DataGrid;
			if (dataGrid != null)
			{
				if (args.OldValue == null && args.NewValue != null)
				{
					dataGrid.MouseDoubleClick += ExecuteDataGridDoubleClick;
				}
				else if (args.OldValue != null && args.NewValue == null)
				{
					dataGrid.MouseDoubleClick -= ExecuteDataGridDoubleClick;
				}
			}
		}

		private static void ExecuteDataGridDoubleClick(object sender, MouseButtonEventArgs args)
		{
            if (sender is DependencyObject obj)
            {
                ICommand command = (ICommand)obj.GetValue(DataGridDoubleClickProperty);
                if (command != null)
                {
                    if (command.CanExecute(obj))
                    {
                        command.Execute(obj);
                    }
                }
            }
        }

	}
}
