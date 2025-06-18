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
using System.Windows.Input;
using System.Globalization;
using Sillogos.Helpers;
using System.Text;

namespace Sillogos.ViewModels
{
    public class ContactCategoriesSearchViewModel : ViewModelBase
	{

        #region P R O P E R T I E S
        //============================================================================================

        private int? _contactCategoryId;
        public int? ContactCategoryId
        {
            get => _contactCategoryId;
            set
            {
                _contactCategoryId = value;
                OnPropertyChanged();
            }
        }

        private string _contactCategoryName;
        public string ContactCategoryName
        {
            get => _contactCategoryName;
            set
            {
                _contactCategoryName = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _remDateFrom;
        public DateTime? RemDateFrom
        {
            get => _remDateFrom;
            set
            {
                _remDateFrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _remDateTo;
        public DateTime? RemDateTo
        {
            get => _remDateTo;
            set
            {
                _remDateTo = value;
                OnPropertyChanged();
            }
        }

        private string _remUser;
        public string RemUser
        {
            get => _remUser;
            set
            {
                _remUser = value;
                OnPropertyChanged();
            }
        }

        private ContactCategoriesListViewModel _parentViewModel;
        public ContactCategoriesListViewModel ParentViewModel
        {
            get => _parentViewModel;
            set
            {
                _parentViewModel = value;
                OnPropertyChanged();
            }
        }

        private string _filterExpression;

        public string FilterExpression
        {
            get => _filterExpression;
            set
            {
                _filterExpression = value;
                _parentViewModel.FilterExpression = value;
                OnPropertyChanged();
            }
        }

        #endregion P R O P E R T I E S

        #region C O M M A N D S   D E F I N I T I O N
        //============================================================================================

        public ICommand SearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }

        #endregion C O M M A N D S   D E F I N I T I O N

        #region C O N S T R U C T O R
        //============================================================================================

        public ContactCategoriesSearchViewModel()
        {

            //ContactCategory = new ContactCategory();

            ClearSearchCommand = new RelayCommand(ClearSearch);
            SearchCommand = new RelayCommand(Search);

        }

        #endregion C O N S T R U C T O R

        #region C O M M A N D S   E X E C U T I O N
        //============================================================================================

        private void ClearSearch(object p)
        {
            try
            {
                ContactCategoryId = null;
                ContactCategoryName = string.Empty;
                RemDateFrom = null;
                RemDateTo = null;
                RemUser = string.Empty;

                FilterExpression = string.Empty;
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(string.Format(CultureInfo.CurrentCulture, "Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {0}.\n\nPlease contact technical support team!", ex.Message));
                // throw ex;
            }
        }

        private void Search(object p)
        {
            try
            {

                FilterExpression = string.Empty;
                var sbFilterExpression = new StringBuilder();


                if (ContactCategoryId != 0)
                    sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "ContactCategoryId = {0} AND ",
                        ContactCategoryId);

                if (!string.IsNullOrEmpty(ContactCategoryName))
                    sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "ContactCategoryName like '%{0}%' AND ",
                        ContactCategoryName);

                if (RemDateFrom != null)
                {
                    if (RemDateTo != null)
                        sbFilterExpression.AppendFormat("RemDate BETWEEN '{0}' AND '{1}' AND ",
                            Convert.ToDateTime(RemDateFrom).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture),
                            Convert.ToDateTime(RemDateTo).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture));
                    else
                        sbFilterExpression.AppendFormat("RemDate >= '{0}' AND ",
                            Convert.ToDateTime(RemDateFrom).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture));
                }
                else
                {
                    if (RemDateTo != null)
                        sbFilterExpression.AppendFormat("RemDate <= '{0}' AND ",
                            Convert.ToDateTime(RemDateTo).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture));
                }

                if (!string.IsNullOrEmpty(RemUser))
                    sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "RemUser like '%{0}%' AND ",
                        RemUser);

                if (sbFilterExpression.Length > 0)
                {
                    sbFilterExpression.Remove(sbFilterExpression.Length - 4, 4);
                    FilterExpression = sbFilterExpression.ToString();
                }
            }
            catch (Exception ex)
            {
                // Put your code for Exception Handling here
                // 1. Log the error
                // 2. Handle or Throw Exception
                MessageService.ShowErrorMessage(string.Format(CultureInfo.CurrentCulture,
                    "Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {0}.\n\nPlease contact technical support team!",
                    ex.Message));
                // throw ex;
            }
        }

        #endregion C O M M A N D S   E X E C U T I O N

    }
}
