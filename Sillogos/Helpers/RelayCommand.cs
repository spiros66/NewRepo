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

using System;
using System.Windows.Input;

namespace Sillogos.Helpers
{
    public class RelayCommand : ICommand
    {
        #region Fields 

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors 

        /// <summary> 
        /// Creates a new command that can always execute. 
        /// </summary> 
        /// <param name="execute">The execution logic.</param> 
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary> 
        /// Creates a new command. 
        /// </summary> 
        /// <param name="execute">The execution logic.</param> 
        /// <param name="canExecute">The execution status logic.</param> 
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand MembersData 

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

    }
}
