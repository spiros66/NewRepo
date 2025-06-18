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
    public class ContactDetailsViewModel : ViewModelBase
    {

        #region P R O P E R T I E S 
        //*************************************************************************************************
        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }
        private Contact _contact;

        public List<ContactCategory> ContactCategoriesList
        {
            get => _contactCategoriesList;
            set
            {
                _contactCategoriesList = value;
                OnPropertyChanged();
            }
        }
        private List<ContactCategory> _contactCategoriesList;

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
        public ContactDetailsViewModel()
        {
            GetContactCategories();

            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(Edit);
            GoBackCommand = new RelayCommand(GoBack);
            SaveCommand = new RelayCommand(Save, CanSave);

        }

        #endregion C O N S T R U C T O R

        #region C O M M A N D S   E X E C U T I O N
        //*************************************************************************************************
        private bool CanSave(object p)
        {
            return !Contact.HasErrors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        private void Edit(object p)
        {
            PageMode = Mode.Edit; // Switch to Edit Mode
            WindowHeader = "Επεξεργασία Επαφής";
            RecordHeader = "Επαφή:" + Contact.LastName + " " + Contact.FirstName;
        }

        /// <summary>
        /// DELETE Local Community
        /// </summary>
        /// <param name="p"></param>
        private void Delete(object p)
        {
            try
            {
                var result = MessageService.ShowMessageWithConfirm("Do you really want to delete this Contact?");
                if (result == MessageBoxResult.Yes)
                {
                    //if (p is not Protocol protocol) return;
                    if (ContactsData.Delete(Contact.ContactId, out var errorMessage))
                    {
                        MessageService.ShowMessage($"Η Επαφή {Contact.LastName} διαγράφηκε με επιτυχία!");
                        if (ParentWindowViewModel is MainWindowViewModel parent)
                        {
                            parent.CurrentViewModel = new ContactsListViewModel() { ParentWindowViewModel = parent };
                        }
                    }
                    else
                    {
                        //MessageService.ShowMessage($"Failed to delete Protocol {Protocol.ProtocolId}.");
                        MessageService.ShowMessage(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Error while deleting record.");
            }
        }

        /// <summary>
        /// Navigates back to the Protocols list view by setting the parent window's current view model 
        /// to a new instance of <see cref="ContactsListViewModel"/>.
        /// </summary>
        /// <param name="p">An optional parameter that is not used in this method.</param>
        private void GoBack(object p)
        {
            CloseDialog();
        }


        /// <summary>
        /// SAVE Contact details to the database.
        /// </summary>
        /// <param name="p"></param>
        private void Save(object p)
        {
            try
            {
                if (Contact == null) return;

                Contact.RemDate = DateTime.Now;
                Contact.RemUser = Environment.UserName.Length > 50
                    ? Environment.UserName.Substring(0, 50)
                    : Environment.UserName;

                bool saveResult;

                if (PageMode == Mode.Edit)
                {
                    saveResult = ContactsData.Update(Contact);
                    if (saveResult)
                        MessageBox.Show($"{Contact.LastName} was updated successfully.", "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }

                else
                {
                    saveResult = ContactsData.Add(Contact) > 0;
                    if (saveResult)
                        MessageBox.Show($"{Contact.LastName} was added successfully.", "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }

                if (!saveResult) return;

                if (ParentWindowViewModel is MainWindowViewModel parent)
                {
                    parent.CurrentViewModel = new ContactsListViewModel() { ParentWindowViewModel = parent };
                }
            }
            catch (InvalidOperationException ex)
            {
                // Inform user of duplicate Protocol
                MessageBox.Show(ex.Message, "Duplicate Local Community", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                // Inform user of general error
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion C O M M A N D S   E X E C U T I O N

        #region M E T H O D S 
        //*************************************************************************************************

        private void GetContactCategories()
        {
            ContactCategoriesList = ContactCategoriesData.GetList();
        }

        #endregion M E T H O D S

    }
}
