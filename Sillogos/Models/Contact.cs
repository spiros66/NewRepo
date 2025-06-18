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
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

namespace Sillogos.Models
{
    public class Contact : ModelBase
    {

        #region C O N S T R U C T O R S
        //=========================================================================================

        public Contact()
        {
            LastName = string.Empty;
            CityId = 0;
        }

        public Contact(int contactId, string title, string lastName, string firstName, string address, string region,
            string postal, int cityId, string country, string phone, string mobile, string email, string distributionNr,
            int contactCategoryId, DateTime remDate, string remUser)
        {
            ContactId = contactId;
            Title = title;
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            Region = region;
            Postal = postal;
            CityId = cityId;
            Country = country;
            Phone = phone;
            Mobile = mobile;
            Email = email;
            DistributionNr = distributionNr;
            ContactCategoryId = contactCategoryId;
            RemDate = remDate;
            RemUser = remUser;
        }

        public static Contact Parse(SqlDataReader reader)
        {
            var objContactsData = new Contact
            {
                ContactId = (int)reader["ContactId"],
                Title = (string)(DBNull.Value.Equals(reader[nameof(Title)]) ? string.Empty : reader[nameof(Title)]),
                LastName = (string)(DBNull.Value.Equals(reader[nameof(LastName)]) ? string.Empty : reader[nameof(LastName)]),
                FirstName = (string)(DBNull.Value.Equals(reader[nameof(FirstName)]) ? string.Empty : reader[nameof(FirstName)]),
                Address = (string)(DBNull.Value.Equals(reader[nameof(Address)]) ? string.Empty : reader[nameof(Address)]),
                Region = (string)(DBNull.Value.Equals(reader[nameof(Region)]) ? string.Empty : reader[nameof(Region)]),
                Postal = (string)(DBNull.Value.Equals(reader[nameof(Postal)]) ? string.Empty : reader[nameof(Postal)]),
                CityId = (int)(DBNull.Value.Equals(reader[nameof(CityId)]) ? 0 : reader[nameof(CityId)]),
                Country = (string)(DBNull.Value.Equals(reader[nameof(Country)]) ? string.Empty : reader[nameof(Country)]),
                Phone = (string)(DBNull.Value.Equals(reader[nameof(Phone)]) ? string.Empty : reader[nameof(Phone)]),
                Mobile = (string)(DBNull.Value.Equals(reader[nameof(Mobile)]) ? string.Empty : reader[nameof(Mobile)]),
                Email = (string)(DBNull.Value.Equals(reader[nameof(Email)]) ? string.Empty : reader[nameof(Email)]),
                DistributionNr = (string)(DBNull.Value.Equals(reader[nameof(DistributionNr)]) ? string.Empty : reader[nameof(DistributionNr)]),
                ContactCategoryId = (int)(DBNull.Value.Equals(reader[nameof(ContactCategoryId)]) ? 0 : reader[nameof(ContactCategoryId)]),
                ContactCategoryName = (string)(DBNull.Value.Equals(reader[nameof(ContactCategoryName)]) ? string.Empty : reader[nameof(ContactCategoryName)]),
                RemDate = reader["RemDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["RemDate"],
                RemUser = reader["RemUser"] == DBNull.Value ? null : (string)reader["RemUser"]
            };

            return objContactsData;
        }

        #endregion C O N S T R U C T O R S

        #region P R O P E R T I E S
        //=========================================================================================

        private int _contactId;
        public int ContactId
        {
            get => _contactId;
            set
            {
                _contactId = value;
                OnPropertyChanged();
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _address = string.Empty;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _region = string.Empty;
        public string Region
        {
            get => _region;
            set
            {
                _region = value;
                OnPropertyChanged();
            }
        }

        private string _postal = string.Empty;
        public string Postal
        {
            get => _postal;
            set
            {
                _postal = value;
                OnPropertyChanged();
            }
        }

        private int _cityId;
        public int CityId
        {
            get => _cityId;
            set
            {
                _cityId = value;
                OnPropertyChanged();
            }
        }

        private string _country = string.Empty;
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        private string _mobile = string.Empty;
        public string Mobile
        {
            get => _mobile;
            set
            {
                _mobile = value;
                OnPropertyChanged();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _distributionNr = string.Empty;
        public string DistributionNr
        {
            get => _distributionNr;
            set
            {
                _distributionNr = value;
                OnPropertyChanged();
            }
        }

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

        #region L O O K U P   P R O P E R T I E S
        //=========================================================================================

        private string _contactCategoryName;

        public string ContactCategoryName
        {
            get => _contactCategoryName;
            set
            {
                _contactCategoryName=value;
                OnPropertyChanged();
            }
        }

        #endregion L O O K U P   P R O P E R T I E S

        #region V A L I D A T I O N S
        //=========================================================================================

        protected void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            // Custom validation logic
            switch (propertyName)
            {
                case nameof(LastName):
                    if (string.IsNullOrWhiteSpace(_lastName))
                    {
                        AddError("LastName", "Υποχρεωτικό πεδίο");
                    }
                    else
                    {
                        ClearErrors("LastName");
                    }
                    break;

                case nameof(CityId):
                    if (_cityId <= 0)
                        AddError("CityId", "Υποχρεωτικό πεδίο.");
                    else
                        ClearErrors("CityId");
                    break;
            }
        }

        #endregion V A L I D A T I O N S

    }
}