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
using Sillogos.Services;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Sillogos.ViewModels
{
    public class ContactCategoriesListViewModel : ViewModelBase
    {

        #region P R O P E R T I E S
        //==================================================================================================

        private string _sortColumn = "ContactCategoryName";
        private readonly DialogService _dialogService = new DialogService();

        private ObservableCollection<ContactCategory> _contactCategoriesList;
        public ObservableCollection<ContactCategory> ContactCategoriesList
        {
            get => _contactCategoriesList;
            set
            {
                {
                    _contactCategoriesList = value;
                    OnPropertyChanged();
                }
            }
        }

        public ContactCategory SelectedContactCategories;

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private ContactCategory _selectedContactCategory;
        public ContactCategory SelectedContactCategory
        {
            get => _selectedContactCategory;
            set
            {
                _selectedContactCategory = value;
                OnPropertyChanged();
            }
        }

        private ContactCategoriesSearchViewModel _contactCategoriesSearchViewModel;
        public ContactCategoriesSearchViewModel ContactCategoriesSearchViewModel
        {
            get => _contactCategoriesSearchViewModel;
            set
            {
                _contactCategoriesSearchViewModel = value;
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
                GetData();
            }
        }

        private bool _selectAllNone;
        public bool SelectAllNone
        {
            get => _selectAllNone;
            set
            {
                _selectAllNone = value;
                ContactCategoriesList.ToList().ForEach(x => x.IsSelected = value);
                OnPropertyChanged();
            }
        }

        #endregion P R O P E R T I E S

        #region P R O P E R T I E S   f o r   P A G I N G
        //==================================================================================================
        public List<int> PageSizesList { get; set; }

        private int _currentPageSize;
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

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage = 1;
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

        private int _currentStart;
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

        private int _currentEnd;
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

        private int _totalCount;
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

        #endregion P R O P E R T I E S   f o r   P A G I N G

        #region C O M M A N D S   D E F I N I T I O N S
        //==================================================================================================

        // Search Commands
        public ICommand ClearSearchCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        // Paging Commands
        public ICommand GoToFirstPageCommand { get; set; }
        public ICommand GoToPreviousPageCommand { get; set; }
        public ICommand GoToNextPageCommand { get; set; }
        public ICommand GoToLastPageCommand { get; set; }
        public ICommand GoToPageCommand { get; set; }

        // Action Commands
        public ICommand AddNewCommand { get; set; }
        public ICommand DeleteListCommand { get; set; }
        public ICommand CloneCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ShowSearchCommand { get; set; }
        public ICommand SortColumnCommand { get; set; }

        // Row Action Commands
        public ICommand DeleteCommand { get; set; }
        public ICommand DisplayMessageCommand { get; }
        public ICommand ViewCommand { get; set; }

        #endregion C O M M A N D S   D E F I N I T I O N S

        #region C O N S T R U C T O R
        //==================================================================================================

        public ContactCategoriesListViewModel()
        {
            PageSizesList = DefaultValuesData.GetPageSizesList();
            CurrentPageSize = PageSizesList[0];

            //GetData();

            _contactCategoriesSearchViewModel = new ContactCategoriesSearchViewModel
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
        //==================================================================================================

        /// <summary>
        /// Sort Column
        /// </summary>
        /// <param name="p"></param>
        private void SortColumn(object p)
        {
            try
            {
                if (_sortColumn.Equals(p.ToString()))
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

        private bool CanGoToFirstOrPreviousPage(object obj)
        {
            return CurrentPage != 1;
        }

        private void QuickSearch(object p)
        {
            FilterExpression = string.Empty;
            FilterExpression = string.Format(CultureInfo.CurrentCulture, "ContactCategoryName LIKE '%{0}%'", SearchText);
        }

        private void ClearSearch(object p)
        {
            SearchText = string.Empty;
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
            //return SelectedContactCategory != null;
            return ContactCategoriesList.Any(c => c.IsSelected);
        }

        private bool CanExecute(object obj) => SelectedContactCategory != null;

        /// <summary>
        ///ADD New Record
        /// </summary>
        /// <param name="p"></param>
        private void AddNew(object p)
        {
            try
            {
                var vm = new ContactCategoryDetailsViewModel
                {
                    ContactCategory = new ContactCategory(),
                    PageMode = Mode.Add,
                    WindowHeader = "Προσθήκη Κατηγορίας επαφής",
                    RecordHeader = "Νέα Κατηγορία επαφής",
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
                MessageService.ShowErrorMessage($"Σφάλμα κατά την προσθήκη νέας Κατηγορίας επαφής: {ex.Message}");
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
                if (ParentWindowViewModel is MainWindowViewModel parent && SelectedContactCategory != null)
                {
                    var clonedContactCategory = (ContactCategory)SelectedContactCategory.Clone();
                    clonedContactCategory.ContactCategoryId = 0;

                    var vm = new ContactCategoryDetailsViewModel
                    {
                        ContactCategory = clonedContactCategory,
                        PageMode = Mode.Add,
                        WindowHeader = "Προσθήκη Κατηγορίας επαφής",
                        RecordHeader = "Νέα Κατηγορία επαφής",
                    };
                    var result = _dialogService.ShowDialog(vm);
                    if (result == true)
                    {
                        // Προσθήκη στη βάση και ενημέρωση λίστας
                        GetData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorMessage($"Σφάλμα κατά την προσθήκη νέας Κατηγορίας επαφής: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE selected record
        /// </summary>
        private void Delete(object p)
        {
            try
            {
                var result = MessageService.ShowMessageWithConfirm("Do you want to delete this ContactCategory?");

                if (result != MessageBoxResult.Yes) return;

                if (ContactCategoriesData.Delete(SelectedContactCategory.ContactCategoryId, out var errorMessage))
                {
                    GetData();

                    MessageService.ShowMessage($"Η Δ.Ο.Υ. {SelectedContactCategory.ContactCategoryName} διαγράφηκε με επιτυχία!");
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
            //var selectedContactCategories = ContactCategoriesList.Where(c => c.IsSelected).ToList();
            var selectedContactCategories = ContactCategoriesList.Where(c => c.IsSelected).ToList();

            if (selectedContactCategories.Count == 0)
            {
                MessageBox.Show("No records selected for deletion.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var deleted = new List<string>();
            var constraintViolations = new List<string>();
            var errors = new List<string>();

            foreach (var contactCategory in selectedContactCategories)
            {
                try
                {
                    ContactCategoriesData.DeleteList(SelectedContactCategory.ContactCategoryId);
                    deleted.Add(contactCategory.ContactCategoryName);
                }
                catch (SqlException ex) when (ex.Number == 547) // Foreign key constraint violation
                {
                    constraintViolations.Add(contactCategory.ContactCategoryName);
                }
                catch (Exception)
                {
                    errors.Add(contactCategory.ContactCategoryName);
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
        /// Refresh the ContactCategories data
        /// </summary>
        private void Refresh(object obj)
        {
            try
            {
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
                    var vm = new ContactCategoryDetailsViewModel
                    {
                        ContactCategory = SelectedContactCategory,
                        PageMode = Mode.View,
                        WindowHeader = "Προβολή Κατηγορίας επαφής",
                        RecordHeader = "Κατηγορία επαφής: " + SelectedContactCategory.ContactCategoryName
                    };

                    _dialogService.ShowDialog(vm);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Σφάλμα κατά την επεξεργασία του αιτήματός σας.");
            }
        }

        #endregion C O M M A N D S   E X E C U T I O N

        #region M E T H O D S
        //==================================================================================================

        /// <summary>
        /// Refresh the ContactCategories data
        /// </summary>
        public void GetData()
        {
            var objContactCategoriesList = ContactCategoriesData.GetList(FilterExpression, _sortColumn, CurrentPage, CurrentPageSize, out var total);
            ContactCategoriesList = new ObservableCollection<ContactCategory>(objContactCategoriesList);
            TotalCount = total;
        }

        #endregion M E T H O D S

    }
}
