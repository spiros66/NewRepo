//*************************************************************************************************
// Assembly         : Sillogos v2.0
// Author           : Σπύρος
// Created          : 01-04-2023
//
// Last Modified By : Σπύρος
// Last Modified On : 01-04-2023
// Description      : 
//
// Copyright        : (c) Spiros. All rights reserved.
//*************************************************************************************************

using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using Sillogos.Helpers;

namespace Sillogos.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExitCommand { get; set; }

        public ICommand NavigateCommand { get; set; }

        public MainWindowViewModel()
        {
            Dictionary<string, Func<ViewModelBase>> viewModelList = new Dictionary<string, Func<ViewModelBase>>();
            
            viewModelList.Add("Home", () => new HomePageViewModel());
            viewModelList.Add("ContactCategories", () => new ContactCategoriesListViewModel());
            viewModelList.Add("Contacts", () => new ContactsListViewModel());
            
            // Invoke HomePageViewModel
            CurrentViewModel = new HomePageViewModel();
            CurrentViewModel.ParentWindowViewModel = this;

            NavigateCommand = new RelayCommand((e) =>
            {
                if (e != null && viewModelList.ContainsKey(e.ToString()))
                {
                    CurrentViewModel = viewModelList[e.ToString()]();
                    CurrentViewModel.ParentWindowViewModel = this;
                }
            });

            ExitCommand = new RelayCommand((e) => { Application.Current.Shutdown(); });
        }
    }
}