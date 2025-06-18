using System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sillogos.Helpers;
using Sillogos.Services;

namespace Sillogos.ViewModels
{
    public abstract class ViewModelBase : INotifyDataErrorInfo, INotifyPropertyChanged, ICloseable
    {

        public ViewModelBase ParentWindowViewModel { get; set; }

        public string RecordHeader { get; set; }
        public string WindowHeader { get; set; }

        protected static void HandleException(Exception ex, string userMessage)
        {
            // Log the error (implement logging as needed)
            // LogError(ex);

            // Show user-friendly message
            MessageService.ShowErrorMessage($"{userMessage} \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
        }
        
        #region INotifyDataErrorInfo
        //=========================================================================================

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool HasErrors { get; }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion INotifyDataErrorInfo

        #region INotifyPropertyChanged
        //=========================================================================================

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion INotifyPropertyChanged

        #region ICloseable
        //=========================================================================================

        public event EventHandler RequestClose;

        protected void CloseDialog()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        #endregion ICloseable
    }
}
