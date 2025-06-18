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

namespace Sillogos.Helpers
{
    public static class SecurityHelper
    {
        public static bool CheckForSQLInjection(string userInput)
        {
			bool isSQLInjection = false;
			string[] sqlCheckList = { "--", ";--", ";", "/*","*/","@@","@","char","nchar","varchar","nvarchar","alter","begin",
									   "cast","create","cursor","declare","delete","drop","end","exec","execute",
									   "fetch","insert","kill","select","sys","sysobjects","syscolumns","table","update" };
			//string CheckString = userInput.Replace("'", "''));

			for (int i = 0; i <= sqlCheckList.Length - 1; i++)
			{
				if ((userInput.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
				{
					isSQLInjection = true;
				}
			}
			return isSQLInjection;
        }

		public static string ReplaceQuotes(string userInput)
		{
			return userInput.Replace("'", "''");
		}
    }
}