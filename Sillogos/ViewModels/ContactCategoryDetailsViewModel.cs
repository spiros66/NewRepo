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
using System.Windows;
using System.Windows.Input;
using Sillogos.Data;
using Sillogos.Helpers;
using Sillogos.Models;

namespace Sillogos.ViewModels
{
    public class ContactCategoryDetailsViewModel : ViewModelBase
    {

        #region P R O P E R T I E S
        //*************************************************************************************************

        private readonly ContactCategoriesData _contactCategoriesData;

        private ContactCategory _contactCategory;
        public ContactCategory ContactCategory
        {
            get => _contactCategory;
            set
            {
                _contactCategory = value;
                OnPropertyChanged();
            }
        }

        public List<ContactCategory> ContactCategoriesList { get; set; }
        
        public Mode PageMode
        {
            get => _pageMode;
            set
            {
                _pageMode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsSaveVisible));
                OnPropertyChanged(nameof(IsEditDeleteVisible));
            }
        }
        private Mode _pageMode;

        public bool IsReadOnly => PageMode == Mode.View;
        public bool IsSaveVisible => PageMode != Mode.View;
        public bool IsEditDeleteVisible => PageMode == Mode.View;

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }

        #endregion P R O P E R T I E S

        #region C O M M A N D S   D E F I N I T I O N
        //*************************************************************************************************

        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        #endregion C O M M A N D S   D E F I N I T I O N

        #region C O N S T R U C T O R
        //*************************************************************************************************

        public ContactCategoryDetailsViewModel()
        {

            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(Edit);
            GoBackCommand = new RelayCommand(GoBack);
            SaveCommand = new RelayCommand(Save, CanSave);


        }
        
        #endregion C O N S T R U C T O R

        #region C O M M A N D S   E X E C U T I O N
        //*************************************************************************************************

        private bool CanSave(object arg)
        {
            return !ContactCategory.HasErrors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        private void Edit(object p)
        {
            PageMode = Mode.Edit;
        }

        /// <summary>
        /// Delete ContactCategory
        /// </summary>
        /// <param name="p"></param>
        private void Delete(object p)
        {
            try
            {
                var result = MessageService.ShowMessageWithConfirm("Do you really want to delete this ContactCategorys?");
                if (result == MessageBoxResult.Yes)
                {
                    if (p is ContactCategory objContactCategorys)
                    {
                        //if (ContactCategoriesData.Delete(objContactCategorys.ContactCategoryId))
                        //{
                        //    MessageService.ShowMessage(
                        //        $"delete ContactCategorys {objContactCategorys.ContactCategoryId}  successfully!");
                        //    if (ParentWindowViewModel is MainWindowViewModel parent)
                        //    {
                        //        parent.CurrentViewModel = new ContactCategorysListViewModel() { ParentWindowViewModel = parent };
                        //    }
                        //}
                        //else
                        //{
                        //    MessageService.ShowMessage($"Delete ContactCategorys {objContactCategorys.ContactCategoryId}  fails!");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Error while saving record.\n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                // throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        private void GoBack(object p)
        {
            CloseDialog();
        }

        /// <summary>
        /// Save ContactCategory
        /// </summary>
        /// <param name="p"></param>
        private void Save(object p)
        {
            try
            {
                if (ContactCategory != null)
                {
                    bool saveResult;

                    ContactCategory.RemDate = DateTime.Now;
                    ContactCategory.RemUser = Environment.UserName.Length > 16
                        ? Environment.UserName.Substring(0, 16)
                        : Environment.UserName;

                    if (PageMode == Mode.Edit)
                        saveResult = ContactCategoriesData.Update(ContactCategory);
                    else
                        saveResult = ContactCategoriesData.Add(ContactCategory) > 0;

                    if (saveResult) //todo add messageBox?
                        if (ParentWindowViewModel is MainWindowViewModel parent)
                            parent.CurrentViewModel = new ContactCategoriesListViewModel() { ParentWindowViewModel = parent };
                }
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Error while saving record.\n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        #endregion C O M M A N D S   E X E C U T I O N

        #region M E T H O D S
        //*************************************************************************************************

        #endregion M E T H O D S

    }
}
