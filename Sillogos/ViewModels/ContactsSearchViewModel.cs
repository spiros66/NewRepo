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
using Sillogos.Models;
using System.Globalization;
using Sillogos.Helpers;

namespace Sillogos.ViewModels
{
    public class ContactsSearchViewModel : ViewModelBase
	{
		#region Private Members
		private Contact _contact;
		private string _filterExpression;
		private ContactsListViewModel _parentViewModel;
		#endregion

		#region *** Properties ***
		/// <summary>
		/// for validation if number
		/// </summary>
		public Contact Contact
		{
			get => _contact;
            set
			{
				_contact = value;
				OnPropertyChanged();
			}
		}
		public ContactsListViewModel ParentViewModel
		{
			get => _parentViewModel;
            set
			{
				_parentViewModel = value;
				OnPropertyChanged();
			}
		}
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
		// Lookup Lists

        #endregion

        #region Commands Definition
        public ICommand SearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }
 		#endregion

		#region *** Constructor ***
		public ContactsSearchViewModel()
        {
            try
            {
				Contact = new Contact();

                SearchCommand = new RelayCommand((p) =>
                {
					//if (!string.IsNullOrEmpty(Error))
					//{
					//	return;
					//}

					try
					{
						FilterExpression = string.Empty;
						System.Text.StringBuilder sbFilterExpression = new System.Text.StringBuilder();
					
	
					if (Contact.ContactId != 0)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "ContactId = {0} AND ", Contact.ContactId);
		
					if (Contact.Title != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Title like '%{0}%' AND ", Contact.Title);
		
					if (Contact.LastName != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "LastName like '%{0}%' AND ", Contact.LastName);
		
					if (Contact.FirstName != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "FirstName like '%{0}%' AND ", Contact.FirstName);
		
					if (Contact.Address != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Address like '%{0}%' AND ", Contact.Address);
		
					if (Contact.Region != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Region like '%{0}%' AND ", Contact.Region);
		
					if (Contact.Postal != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Postal like '%{0}%' AND ", Contact.Postal);
		
					if (Contact.CityId != 0)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "CityId = {0} AND ", Contact.CityId);
		
					if (Contact.Country != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Country like '%{0}%' AND ", Contact.Country);
		
					if (Contact.Phone != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Phone like '%{0}%' AND ", Contact.Phone);
		
					if (Contact.Mobile != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Mobile like '%{0}%' AND ", Contact.Mobile);
		
					if (Contact.Email != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "Email like '%{0}%' AND ", Contact.Email);
		
					if (Contact.DistributionNr != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "DistributionNr like '%{0}%' AND ", Contact.DistributionNr);
		
					if (Contact.ContactCategoryId != 0)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "ContactCategoryId = {0} AND ", Contact.ContactCategoryId);
		
					if (Contact.RemDate != null)
						sbFilterExpression.AppendFormat("RemDate BETWEEN '{0}' AND '{1}' AND ", Convert.ToDateTime(Contact.RemDate).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture), Convert.ToDateTime(Contact.RemDate).AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture));
		
					if (Contact.RemUser != string.Empty)
						sbFilterExpression.AppendFormat(CultureInfo.CurrentCulture, "RemUser like '%{0}%' AND ", Contact.RemUser);
		
						if (sbFilterExpression.Length > 0)
						{
							sbFilterExpression.Remove(sbFilterExpression.Length - 4, 4);
							FilterExpression = sbFilterExpression.ToString();
						}
					}
					catch(Exception ex)
					{
							// Put your code for Exception Handling here
							// 1. Log the error
							// 2. Handle or Throw Exception
							MessageService.ShowErrorMessage(
                                $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
							// throw ex;
					}
				}, p => CanSearch);

                ClearSearchCommand = new RelayCommand((p) =>
                {
					try
					{
						Contact = null;
						Contact = new Contact();

						FilterExpression = string.Empty;
					}
					catch(Exception ex)
					{
							// Put your code for Exception Handling here
							// 1. Log the error
							// 2. Handle or Throw Exception
							MessageService.ShowErrorMessage(
                                $"Σφάλμα κατά την επεξεργασία του αιτήματός σας. \n\nERROR: {ex.Message}.\n\nPlease contact technical support team!");
							// throw ex;
					}
                });
            }
            catch (Exception ex)
            {
                MessageService.ShowMessage(ex.Message);
            }
        }
		#endregion

		bool CanSearch => !Contact.HasErrors;
    }
}
