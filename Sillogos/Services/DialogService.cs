using System;
using System.Collections.Generic;
using System.Windows;
using Sillogos.ViewModels;
using Sillogos.Views;

namespace Sillogos.Services
    {
        public class DialogService : IDialogService
        {
            private readonly Dictionary<Type, Type> _viewModelToViewMapping;

            public DialogService()
            {
                _viewModelToViewMapping = new Dictionary<Type, Type>
                {
                    { typeof(ContactDetailsViewModel), typeof(ContactDetails) },
                    { typeof(ContactCategoryDetailsViewModel), typeof(ContactCategoryDetails) },
                    // Add other mappings as needed
                };
            }

            public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class
            {
                var viewModelType = typeof(TViewModel);
                if (_viewModelToViewMapping.TryGetValue(viewModelType, out var viewType))
                {
                    var view = (Window)Activator.CreateInstance(viewType);
                    view.DataContext = viewModel;

                    if (viewModel is ICloseable closeable) closeable.RequestClose += (sender, e) => view.Close();

                    return view.ShowDialog();
                }
                else
                {
                    throw new InvalidOperationException($"No view found for {viewModelType.FullName}");
                }
            }
        }
    }