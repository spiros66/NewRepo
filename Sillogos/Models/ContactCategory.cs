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
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace Sillogos.Models
{
	public class ContactCategory : ModelBase
	{

        #region C O N S T R U C T O R S
        //=========================================================================================
		public ContactCategory() { }
		
		public ContactCategory(int contactCategoryId,string contactCategoryName,DateTime remDate,string remUser)
		{
			ContactCategoryId = contactCategoryId;
			ContactCategoryName = contactCategoryName;
			RemDate = remDate;
			RemUser = remUser;
		}

		public static ContactCategory Parse(SqlDataReader objReader)
		{
			ContactCategory objContactCategory = new ContactCategory();

			objContactCategory.ContactCategoryId = (int) (DBNull.Value.Equals(objReader[nameof(ContactCategoryId)]) ? 0 : objReader[nameof(ContactCategoryId)]);
			objContactCategory.ContactCategoryName = (string) (DBNull.Value.Equals(objReader[nameof(ContactCategoryName)]) ? string.Empty : objReader[nameof(ContactCategoryName)]);
			if (!DBNull.Value.Equals(objReader[nameof(RemDate)]))
				objContactCategory.RemDate = DateTime.Parse(objReader[nameof(RemDate)].ToString(), CultureInfo.CurrentCulture);
			objContactCategory.RemUser = (string) (DBNull.Value.Equals(objReader[nameof(RemUser)]) ? string.Empty : objReader[nameof(RemUser)]);
			return objContactCategory;
		}

        #endregion C O N S T R U C T O R S

        #region P R O P E R T I E S
        //=========================================================================================

        private int _contactCategoryId;
			public int ContactCategoryId
			{
				get => _contactCategoryId;
                set
				{
					_contactCategoryId = value;
					OnPropertyChanged();
				}
			}
		
			private string _contactCategoryName = string.Empty;
			public string ContactCategoryName
			{
				get => _contactCategoryName;
                set
				{
					_contactCategoryName = value;
					OnPropertyChanged();
				}
			}
		
			private DateTime? _remDate;
			public DateTime? RemDate
			{
				get => _remDate;
                set
				{
					_remDate = value;
					OnPropertyChanged();
				}
			}
		
			private string _remUser = string.Empty;
			public string RemUser
			{
				get => _remUser;
                set
				{
					_remUser = value;
					OnPropertyChanged();
				}
			}

        #endregion P R O P E R T I E S

        #region V A L I D A T I O N S
        //=========================================================================================


        #endregion V A L I D A T I O N S

    }
}