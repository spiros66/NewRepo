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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Sillogos.Data;
using Sillogos.Models;
using Sillogos.Helpers;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;
using Sillogos.Services;

namespace Sillogos.ViewModels
{
    public class ContactsListViewModel : ViewModelBase
    {

        #region P R O P E R T I E S
        //===========================================================================================================

        private string _sortColumn = "LastName";
        private readonly DialogService _dialogService = new DialogService();

        private ObservableCollection<Contact> _contactsList;
        public ObservableCollection<Contact> ContactsList
        {
            get => _contactsList;
            set
            {
                _contactsList = value;
                OnPropertyChanged();
            }
        }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        public Contact SelectedContacts;

        private ContactsSearchViewModel _contactsSearchViewModel;
        public ContactsSearchViewModel ContactsSearchViewModel
        {
            get => _contactsSearchViewModel;
            set
            {
                _contactsSearchViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(FilterExpression);
            }
        }

        private string _filterExpression;
        public string FilterExpression
        {
            get => _filterExpression;
            set
            {
                _filterExpression = value;
                OnPropertyChanged();
                CurrentPage = 1; //Δική μου προσθήκη
                GetData();
            }
        }

        ///<summary>
        /// Κείμενο για αναζήτηση
        /// </summary>
        /// <returns></returns>
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                //CurrentPage = 1;
                //GetData();
            }
        }

        public bool SelectAllNone
        {
            get => _selectAllNone;
            set
            {
                _selectAllNone = value;
                ContactsList.ToList().ForEach(x => x.IsSelected = value);
                OnPropertyChanged();
            }
        }
        private bool _selectAllNone;

        #endregion P R O P E R T I E S

        #region P R O P E R T I E S   f o r   P A G I N G

        //public List<int> PageSizesList
        //{
        //    get => _pageSizesList;
        //    set => _pageSizesList = value;
        //}
        //private List<int> _pageSizesList;
        public List<int> PageSizesList { get; set; }

        public int CurrentPageSize
        {
            get => _currentPageSize;
            set
            {
                _currentPageSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPage));
                OnPropertyChanged(nameof(CurrentStart));
                OnPropertyChanged(nameof(CurrentEnd));
                CurrentPage = 1;
                GetData();
                //todo redo search and display
            }
        }
        private int _currentPageSize;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        private int _currentPage = 1;

        public int TotalPage
        {
            get
            {
                if (CurrentPageSize > 0)
                {
                    _totalPage = (TotalCount + CurrentPageSize - 1) / CurrentPageSize;
                }
                else
                {
                    _totalPage = 0;
                }
                return _totalPage;
            }
        }
        private int _totalPage = 1;

        public int CurrentStart
        {
            get
            {
                if (CurrentPage > 0)
                {
                    _currentStart = CurrentPageSize * (CurrentPage - 1) + 1;
                }
                return _currentStart;
            }
        }
        private int _currentStart;

        public int CurrentEnd
        {
            get
            {
                _currentEnd = CurrentPage * CurrentPageSize;
                if (_currentEnd > TotalCount)
                {
                    _currentEnd = TotalCount;
                }
                return _currentEnd;
            }
        }
        private int _currentEnd;

        public int TotalCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPage));
                OnPropertyChanged(nameof(CurrentStart));
                OnPropertyChanged(nameof(CurrentEnd));
            }
        }
        private int _totalCount;

        #endregion P R O P E R T I E S   f o r   P A G I N G

        #region C O M M A N D S   D E F I N I T I O N
        //===========================================================================================================

        // Search Commands
        public ICommand QuickSearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }

        // Paging Commands
        public ICommand GoToFirstPageCommand { get; set; }
        public ICommand GoToPreviousPageCommand { get; set; }
        public ICommand GoToNextPageCommand { get; set; }
        public ICommand GoToLastPageCommand { get; set; }
        public ICommand GoToPageCommand { get; set; }

        // Action Commands
        public ICommand AddNewCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteListCommand { get; set; }
        public ICommand CloneCommand { get; set; }
        public ICommand SortColumnCommand { get; set; }

        // Row Action Commands
        public ICommand ViewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion C O M M A N D S   D E F I N I T I O N

        #region C O N S T R U C T O R
        //===========================================================================================================

        public ContactsListViewModel()
        {
            PageSizesList = DefaultValuesData.GetPageSizesList();
            CurrentPageSize = PageSizesList[0];

            //GetData();

            _contactsSearchViewModel = new ContactsSearchViewModel()
            {
                ParentViewModel = this
            };

            SortColumnCommand = new RelayCommand(SortColumn);

            QuickSearchCommand = new RelayCommand(QuickSearch);
            ClearSearchCommand = new RelayCommand(ClearSearch);

            GoToFirstPageCommand = new RelayCommand(GoToFirstPage, CanGoToFirstOrPreviousPage);
            GoToPreviousPageCommand = new RelayCommand(GoToPreviousPage, CanGoToFirstOrPreviousPage);
            GoToNextPageCommand = new RelayCommand(GoToNextPage, CanGoToNextOrLastPage);
            GoToLastPageCommand = new RelayCommand(GoToLastPage, CanGoToNextOrLastPage);
            GoToPageCommand = new RelayCommand(GoToPage);

            AddNewCommand = new RelayCommand(AddNew);
            CloneCommand = new RelayCommand(Clone, CanExecute);
            DeleteCommand = new RelayCommand(Delete, CanExecute);
            DeleteListCommand = new RelayCommand(DeleteList, CanDeleteSelected);
            RefreshCommand = new RelayCommand(Refresh);
            ViewCommand = new RelayCommand(View, CanExecute);

        }

        #endregion C O N S T R U C T O R

        #region C O M M A N D S   E X E C U T I O N
        //===========================================================================================================

        /// <summary>
        /// Sort Column
        /// </summary>
        /// <param name="p"></param>
        private void SortColumn(object p)
        {
            try
            {
                if (_sortColumn.Equals(p.ToString(), StringComparison.CurrentCulture))
                    _sortColumn = p + " desc";
                else
                    _sortColumn = p.ToString();

                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        /// <summary>
        /// QUICK SEARCH
        /// </summary>
        /// <param name="p"></param>
        private void QuickSearch(object p)
        {
            //FilterExpression = string.Empty;
            FilterExpression = string.Format(CultureInfo.CurrentCulture, "Title LIKE N'%{0}%' "
                                                                         + "OR LastName LIKE N'%{0}%' "
                                                                         + "OR FirstName LIKE N'%{0}%' "
                                                                         + "OR Region LIKE N'%{0}%' "
                                                                         + "OR CityName LIKE N'%{0}%' "
                                                                         + "OR Country LIKE N'%{0}%' "
                                                                         + "OR ContactCategoryName LIKE N'%{0}%'", SearchText);
        }

        private void ClearSearch(object p)
        {
            SearchText = string.Empty;
        }

        private bool CanGoToFirstOrPreviousPage(object obj)
        {
            return CurrentPage != 1;
        }

        private bool CanGoToNextOrLastPage(object obj)
        {
            return CurrentPage != TotalPage;
        }

        /// <summary>
        /// Goto First Page
        /// </summary>
        private void GoToFirstPage(object obj)
        {
            try
            {
                CurrentPage = 1;
                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        /// <summary>
        /// Goto Previous Page
        /// </summary>
        private void GoToPreviousPage(object obj)
        {
            try
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                }
                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        /// <summary>
        /// Goto Next Page
        /// </summary>
        private void GoToNextPage(object obj)
        {
            try
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage++;
                }
                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        /// <summary>
        /// Goto Last Page
        /// </summary>
        private void GoToLastPage(object obj)
        {
            try
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage = TotalPage;
                }

                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        private void GoToPage(object obj)
        {
            try
            {
                if (CurrentPage > TotalPage)
                {
                    CurrentPage = TotalPage;
                }

                if (CurrentPage < 2)
                {
                    CurrentPage = 1;
                }

                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        private bool CanDeleteSelected(object obj)
        {
            //return SelectedCity != null;
            return ContactsList.Any(c => c.IsSelected);
        }

        public bool CanExecute(object obj)
        {
            return SelectedContact != null;
        }

        /// <summary>
        ///ADD New Record
        /// </summary>
        /// <param name="p"></param>
        private void AddNew(object p)
        {
            try
            {
                var vm = new ContactDetailsViewModel
                {
                    Contact = new Contact(),
                    PageMode = Mode.Add,
                    WindowHeader = "Προσθήκη Επαφής",
                    RecordHeader = "Νέα Επαφή",
                };
                var result = _dialogService.ShowDialog(vm);
                if (result == true)
                {
                    // Προσθήκη στη βάση και ενημέρωση λίστας
                    GetData();
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorMessage($"Σφάλμα κατά την προσθήκη νέας Επαφής: {ex.Message}");
            }
        }

        /// <summary>
        /// CLONE selected record
        /// </summary>
        /// <param name="p"></param>
        private void Clone(object p)
        {
            try
            {
                if (ParentWindowViewModel is MainWindowViewModel parent && SelectedContact != null)
                {
                    var clonedContact = (Contact)SelectedContact.Clone();
                    clonedContact.ContactId = 0;

                    parent.CurrentViewModel = new ContactDetailsViewModel()
                    {
                        ParentWindowViewModel = parent,
                        Contact = clonedContact,
                        PageMode = Mode.Add,
                        WindowHeader = "Προσθήκη Επαφής",
                        RecordHeader = "Νέα Επαφή"
                    };
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorMessage($"Σφάλμα κατά την προσθήκη νέας Επαφής: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE selected record
        /// </summary>
        /// <param name="p"></param>
        private void Delete(object p)
        {
            try
            {
                var result = MessageService.ShowMessageWithConfirm("Do you want to delete this Contact?");

                if (result != MessageBoxResult.Yes) return;

                if (ContactsData.Delete(SelectedContact.ContactId, out var errorMessage))
                {
                    GetData();

                    MessageService.ShowMessage($"Η Επαφή {SelectedContact.LastName} διαγράφηκε με επιτυχία!");
                }
                else
                {
                    MessageService.ShowMessage(errorMessage);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Σφάλμα κατά την επεξεργασία του αιτήματός σας.");
            }
        }

        /// <summary>
        /// DELETE List of Selected Records
        /// </summary>
        private void DeleteList(object p)
        {
            //var selectedContacts = ContactsList.Where(c => c.IsSelected).ToList();
            var selectedContacts = ContactsList.Where(c => c.IsSelected).ToList();

            if (selectedContacts.Count == 0)
            {
                MessageBox.Show("No records selected for deletion.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var deleted = new List<string>();
            var constraintViolations = new List<string>();
            var errors = new List<string>();

            foreach (var contact in selectedContacts)
            {
                try
                {
                    ContactsData.DeleteList(contact.ContactId);
                    deleted.Add(contact.LastName);
                }
                catch (SqlException ex) when (ex.Number == 547) // Foreign key constraint violation
                {
                    constraintViolations.Add(contact.LastName);
                }
                catch (Exception)
                {
                    errors.Add(contact.LastName);
                }
            }

            // Show result message
            var message = new StringBuilder();
            if (deleted.Count > 0) message.AppendLine($"Deleted successfully: {string.Join(", ", deleted)}");
            if (constraintViolations.Count > 0) message.AppendLine($"Cannot delete (FK constraint): {string.Join(", ", constraintViolations)}");
            if (errors.Count > 0) message.AppendLine($"Other errors: {string.Join(", ", errors)}");

            MessageBox.Show(message.ToString(), "Deletion Summary", MessageBoxButton.OK, MessageBoxImage.Information);

            GetData();
        }

        /// <summary>
        /// REFRESH Data
        /// </summary>
        /// <param name="p"></param>
        private void Refresh(object p)
        {
            try
            {
                SearchText = string.Empty;
                FilterExpression = string.Empty;

                GetData();
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        /// <summary>
        /// VIEW Selected Record
        /// </summary>
        /// <param name="p"></param>
        private void View(object p)
        {
            try
            {
                if (ParentWindowViewModel is MainWindowViewModel parent)
                {
                    var vm = new ContactDetailsViewModel
                    {
                        Contact = SelectedContact,
                        PageMode = Mode.View,
                        WindowHeader = "Προβολή Επαφής",
                        RecordHeader = SelectedContact.LastName + " " + SelectedContact.FirstName
                    };

                    _dialogService.ShowDialog(vm);

                }
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(
                    $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
                //throw ex;
            }
        }

        #endregion C O M M A N D S   E X E C U T I O N

        #region M E T H O D S
        //===========================================================================================================

        /// <summary>
        /// Refresh the Local Communities data
        /// </summary>
        private void GetData()
        {
            var objContactsList = ContactsData.GetList(FilterExpression, _sortColumn, CurrentPage, CurrentPageSize, out var total);
            ContactsList = new ObservableCollection<Contact>(objContactsList);
            TotalCount = total;
        }

        #endregion M E T H O D S

    }
}
