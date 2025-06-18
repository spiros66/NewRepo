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
using Microsoft.Data.SqlClient;
using Sillogos.Models;
using System.Globalization;
using System.Windows;

namespace Sillogos.Data
{
	/// <summary>
	/// Summary description for ContactCategoriesData.
	/// </summary>
    public class ContactCategoriesData: SillogosData
    {
        
		#region ContactCategories - Add Record
        /// <summary>
        /// Creates a new ContactCategoriesData row.
        /// </summary>
        public static int Add( int contactCategoryId, string contactCategoryName, DateTime remDate, string remUser)
        {
            const string query = "INSERT INTO ContactCategories ( ContactCategoryName, RemDate, RemUser) VALUES ( @ContactCategoryName, @RemDate, @RemUser)  SELECT SCOPE_IDENTITY() ";
			try
            {
				using (var connection = new SqlConnection(ConnectionString))
				{
					using (var command = new SqlCommand(query, connection))
					{
						
						
						command.Parameters.AddWithValue("@ContactCategoryName", contactCategoryName);
						command.Parameters.AddWithValue("@RemDate", remDate);
						command.Parameters.AddWithValue("@RemUser", remUser);
						connection.Open();
						var id = command.ExecuteScalar();
						return int.Parse(id.ToString(), CultureInfo.CurrentCulture);
					}
				}
			}
			catch(Exception ex)
			{
				// Put your code for Exception Handling here
				// 1. Log the error
				// 2. Handle or Throw Exception
				// Note: You may modify code generation template by editing ExceptionHandler CodeBlock
				throw ex;
			}
        }
        #endregion
        
		#region ContactCategories - Update Record
        /// <summary>
        /// Updates a ContactCategoriesData
        /// </summary>
        public static bool Update( int contactCategoryId, string contactCategoryName, DateTime remDate, string remUser)
        {
            const string query = "UPDATE ContactCategories SET ContactCategoryName = @ContactCategoryName, RemDate = @RemDate, RemUser = @RemUser WHERE ContactCategoryId = @ContactCategoryId";
			try
            {
				using (var connection = new SqlConnection(ConnectionString))
				{
					using (var command = new SqlCommand(query, connection))
					{
						
						
						command.Parameters.AddWithValue("@ContactCategoryId", contactCategoryId);
						command.Parameters.AddWithValue("@ContactCategoryName", contactCategoryName);
						command.Parameters.AddWithValue("@RemDate", remDate);
						command.Parameters.AddWithValue("@RemUser", remUser);
						connection.Open();
						var rowsAffected = command.ExecuteNonQuery();
						return (rowsAffected == 1);
					}
				}
			}
			catch(Exception ex)
			{
				// Put your code for Exception Handling here
				// 1. Log the error
				// 2. Handle or Throw Exception
				// Note: You may modify code generation template by editing ExceptionHandler CodeBlock
				throw ex;
			}
        }
        #endregion

        #region ContactCategories - Delete Record
        /// <summary>
        /// The purpose of this method is to delete the record based on specified primary key value
        /// </summary>
        /// <param name="contactCategoryId">Primary key value</param>
        /// <returns></returns>
        public static bool Delete(int contactCategoryId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                const string query = "DELETE FROM ContactCategories WHERE ContactCategoryId = @ContactCategoryId";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ContactCategoryId", contactCategoryId);
                var rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex) when (ex.Number == 547) // Foreign key constraint violation
            {
                errorMessage = "Αδύνατη η διαγραφή. Υπάρχουν σχετιζόμενες εγγραφές στον πίνακα < Επαφές >.";
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine($"Error deleting ContactCategory: {ex.Message}");
                throw new DataAccessException("Error deleting data from the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Παρουσιάστηκε απροσδόκητο σφάλμα: {ex.Message}");
                throw new DataAccessException("Παρουσιάστηκε απροσδόκητο σφάλμα.", ex);
            }

            return false;
        }
        #endregion

        #region ContactCategories - Delete Filter

        /// <summary>
        /// The purpose of this method is to delete all ContactCategoriesData data based on the Filter Expression criteria.
        /// </summary>
        /// <param name="filterExpression">A NameValueCollection object that defines various properties.
        /// For example, filterExpression - Where condition to be passed in SQL statement.
        /// </param>
        /// <returns>Returns the number of rows deleted</returns>
        public static int DeleteFilter(string filterExpression)
        {
            const string query_FILTER = "DELETE FROM ContactCategories {0}";
			try
            {
				var rowsAffected = 0;
				using (var connection = new SqlConnection(ConnectionString))
				{
					filterExpression = (string.IsNullOrEmpty(filterExpression) ? string.Empty : $"WHERE {filterExpression}");
					var strSql = string.Format(CultureInfo.CurrentCulture, query_FILTER, filterExpression);
					using (var command = new SqlCommand(strSql, connection))
					{
						
						connection.Open();
						rowsAffected = command.ExecuteNonQuery();
					}
				}
				return rowsAffected;
			}
			catch(Exception ex)
			{
				// Put your code for Exception Handling here
				// 1. Log the error
				// 2. Handle or Throw Exception
				// Note: You may modify code generation template by editing ExceptionHandler CodeBlock
				throw ex;
			}
        }
        #endregion

        #region Categorys - Delete Records

        /// <summary>
        /// Deletes a Tax Office record from the database based on the specified Category ID.
        /// </summary>
        public static void DeleteList(int categoryId)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryId = @CategoryId", conn);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region ContactCategories - Get List of ContactCategory objects
        /// <summary>
        /// Returns a collection with all the ContactCategory
        /// </summary>
		/// <returns>List of ContactCategory object</returns>
        public static List<ContactCategory> GetList()
        {
            const string selectStatement =
                "SELECT  ContactCategoryId, ContactCategoryName, RemDate, RemUser FROM ContactCategories WITH (NOLOCK)";

            var objList = new List<ContactCategory>();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(selectStatement, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                objList.Add(ContactCategory.Parse(reader)); // Use this to avoid null issues
                        }

                        return objList;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error getting Contact Categories: {ex.Message}");
                throw new DataAccessException("Error retrieving data from the database.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new DataAccessException("An unexpected error occurred.", ex);
            }
        }
        
        #endregion

        #region ContactCategories - List ContactCategoriesData by Filter Expression
        /// <summary>
        /// The purpose of this method is to get all ContactCategoriesData data based on the Filter Expression criteria.
        /// </summary>
        /// <param name="filterExpression">A NameValueCollection object that defines various properties.
        /// For example, filterExpression - Where condition to be passed in SQL statement.
        /// </param>
        /// <returns>List of ContactCategory object</returns>
        public static List<ContactCategory> GetList(string filterExpression)
		{
            const string SP_GET_FILTER = "SELECT  ContactCategoryId, ContactCategoryName, RemDate, RemUser FROM ContactCategories WITH (NOLOCK) {0}";
			try
            {
				using (var connection = new SqlConnection(ConnectionString))
				{
					filterExpression = (string.IsNullOrEmpty(filterExpression) ? string.Empty : $"WHERE {filterExpression.Trim()}");
					//if (SecurityHelper.CheckForSQLInjection(filterExpression)) 
					//    throw new Exception("Dangerous Input! Possibly SQL Injection Attack!!!));
					var strSql = string.Format(CultureInfo.CurrentCulture, SP_GET_FILTER, filterExpression);
					using (var command = new SqlCommand(strSql, connection))
					{
						
						command.Parameters.AddWithValue("@where_clause", filterExpression);
						connection.Open();
						var reader = command.ExecuteReader();

						var objList = new List<ContactCategory>();
						while (reader.Read())
						{
							//objList.Add(new ContactCategory(
							//	 (int) reader["ContactCategoryId"], (string) reader["ContactCategoryName"], (DateTime) reader["RemDate"], (string) reader["RemUser"]));
							objList.Add(ContactCategory.Parse(reader)); // Use this to avoid null issues
						}
						return objList;    
					}
				}
			}
			catch(Exception ex)
			{
				// Put your code for Exception Handling here
				// 1. Log the error
				// 2. Handle or Throw Exception
				// Note: You may modify code generation template by editing ExceptionHandler CodeBlock
				throw ex;
			}
		}
		#endregion
		
        #region ContactCategories - List ContactCategoriesData by filterExpression, sortExpression, pageIndex and pageSize
        /// <summary>
        /// The purpose of this method is to get all ContactCategoriesData data based on filterExpression, sortExpression, pageIndex and pageSize parameters
        /// </summary>
        /// <param name="filterExpression">Where condition to be passed in SQL statement. DO NOT include WHERE keyword.</param>
        /// <param name="sortExpression">Sort column name with direction. For Example, "ProductId ASC")</param>
        /// <param name="pageIndex">Page number to be retrieved. Default is 0.</param>
        /// <param name="pageSize">Number of rows to be retrieved. Default is 10.</param>
        /// <param name="rowsCount">Output: Total number of rows exist for the specified criteria.</param>
        /// <returns>List of ContactCategory object</returns>
        public static List<ContactCategory> GetList(string filterExpression, string sortExpression, int pageIndex,
            int pageSize, out int rowsCount)
        {
            var objList = new List<ContactCategory>();

            const string query = "SELECT * " +
                                 "FROM " +
                                 "      ContactCategories " +
                                 "{0}" +
                                 "ORDER BY {1} " +
                                 "OFFSET (({2} - 1) * {3}) ROWS " +
                                 "FETCH NEXT {3} ROWS ONLY " +
                                 "SELECT COUNT(*) " +
                                 "FROM " +
                                 "      ContactCategories " +
                                 "{0}";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    filterExpression = string.IsNullOrEmpty(filterExpression)
                        ? string.Empty
                        : $"WHERE {filterExpression.Trim()}";

                    var strSql = string.Format(query, filterExpression, sortExpression, pageIndex, pageSize);
                    using (var command = new SqlCommand(strSql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                objList.Add(ContactCategory.Parse(reader));

                            reader.NextResult();
                            reader.Read();

                            rowsCount = Convert.ToInt32(reader[0], CultureInfo.CurrentCulture);
                        }

                        return objList;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error getting Contact Categories: {ex.Message}");
                throw new DataAccessException("Error retrieving data from the database.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new DataAccessException("An unexpected error occurred.", ex);
            }
        }

        #endregion

        #region ContactCategories - Get Details by ID
        /// <summary>
        /// Returns an existing ContactCategory object with the specified ID 
        /// </summary>
        public static ContactCategory GetDetailsById(int refId)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
					connection.Open();
					const string query =
						"SELECT  ContactCategoryId, ContactCategoryName, RemDate, RemUser FROM ContactCategories WITH (NOLOCK) WHERE ContactCategoryId = @ref_id";

                    using (var command = new SqlCommand(query, connection))
                    {
						command.Parameters.AddWithValue("@ref_id", refId);

                        using (var reader = command.ExecuteReader())
                        {
                            return reader.Read() ? ContactCategory.Parse(reader) : null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error getting CarInfo by ID: {ex.Message}");
                throw new DataAccessException("Error retrieving data from the database.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Παρουσιάστηκε απροσδόκητο σφάλμα: {ex.Message}");
                throw new DataAccessException("Παρουσιάστηκε απροσδόκητο σφάλμα.", ex);
            }
        }
        #endregion

        #region ContactCategories - Add Record
        /// <summary>
        /// Creates a new Contact Category
        /// </summary>
        public static int Add(ContactCategory contactCategory)
        {
            const string query = "INSERT INTO ContactCategories " +
                                 "( " +
                                 "ContactCategoryName, RemDate, RemUser" +
                                 ") " +
                                 "OUTPUT INSERTED.ContactCategoryId " +
                                 "VALUES " +
                                 "( " +
                                 "@ContactCategoryName, @RemDate, @RemUser " +
                                 ") " +
                                 "";
			try
            {
				using (var connection = new SqlConnection(ConnectionString))
				{
                    connection.Open();
					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ContactCategoryName", contactCategory.ContactCategoryName);
						command.Parameters.AddWithValue("@RemDate", contactCategory.RemDate);
						command.Parameters.AddWithValue("@RemUser", contactCategory.RemUser);

                        var id = command.ExecuteScalar();

                        return int.Parse(id.ToString(), CultureInfo.CurrentCulture);
					}
				}
			}
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627) // Unique constraint violation
                    MessageBox.Show("A Contact Category with that name already exists.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                else
                    // Handle SQL exceptions
                    Console.WriteLine($"Error adding Contact Category: {ex.Message}");
                throw new DataAccessException("Error adding data to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new DataAccessException("An unexpected error occurred.", ex);
            }
        }

        #endregion

        #region ContactCategories - Update Record
        /// <summary>
        /// Updates a Contact Category
        /// </summary>
        public static bool Update(ContactCategory contactCategory)
		{
            const string query = "UPDATE ContactCategories SET ContactCategoryName = @ContactCategoryName, RemDate = @RemDate, RemUser = @RemUser WHERE ContactCategoryId = @ContactCategoryId";
			try
            {
				using (var connection = new SqlConnection(ConnectionString))
				{
                    connection.Open();
					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ContactCategoryId", contactCategory.ContactCategoryId);
						command.Parameters.AddWithValue("@ContactCategoryName", contactCategory.ContactCategoryName);
						command.Parameters.AddWithValue("@RemDate", contactCategory.RemDate);
						command.Parameters.AddWithValue("@RemUser", contactCategory.RemUser);
			
                        var rowsAffected = command.ExecuteNonQuery();
			
                        return (rowsAffected == 1);
					}
				}
			}
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine($"Error updating Contact Category: {ex.Message}");
                throw new DataAccessException("Error updating data in the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Παρουσιάστηκε απροσδόκητο σφάλμα: {ex.Message}");
                throw new DataAccessException("Παρουσιάστηκε απροσδόκητο σφάλμα.", ex);
            }
		}

        #endregion

    }
}
  