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
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace Sillogos.Models
{
	public class DefaultValue : ModelBase
	{

        #region C O N S T R U C T O R S
        //============================================================================================
        public DefaultValue() { }

        public DefaultValue(int defaultValueId, string fontFamilyName, string line1, int fontSizeLine1,
            bool isBoldLine1, bool isItalicLine1, string line2, int fontSizeLine2, bool isBoldLine2, bool isItalicLine2,
            string line3, int fontSizeLine3, bool isBoldLine3, bool isItalicLine3, string line4, int fontSizeLine4,
            bool isBoldLine4, bool isItalicLine4, string line5, int fontSizeLine5, bool isBoldLine5, bool isItalicLine5,
            string line6, int fontSizeLine6, bool isBoldLine6, bool isItalicLine6, string line7, int fontSizeLine7,
            bool isBoldLine7, bool isItalicLine7, string line8, int fontSizeLine8, bool isBoldLine8, bool isItalicLine8,
            string line9, int fontSizeLine9, bool isBoldLine9, bool isItalicLine9, string pageSizes, int pageSize2,
            int pageSize3, int pageSize4, string sample, string footnote, string inCharge, DateTime remDate, string remUser)
        {
            DefaultValueId = defaultValueId;
            FontFamilyName = fontFamilyName;
            Line1 = line1;
            FontSizeLine1 = fontSizeLine1;
            IsBoldLine1 = isBoldLine1;
            IsItalicLine1 = isItalicLine1;
            Line2 = line2;
            FontSizeLine2 = fontSizeLine2;
            IsBoldLine2 = isBoldLine2;
            IsItalicLine2 = isItalicLine2;
            Line3 = line3;
            FontSizeLine3 = fontSizeLine3;
            IsBoldLine3 = isBoldLine3;
            IsItalicLine3 = isItalicLine3;
            Line4 = line4;
            FontSizeLine4 = fontSizeLine4;
            IsBoldLine4 = isBoldLine4;
            IsItalicLine4 = isItalicLine4;
            Line5 = line5;
            FontSizeLine5 = fontSizeLine5;
            IsBoldLine5 = isBoldLine5;
            IsItalicLine5 = isItalicLine5;
            Line6 = line6;
            FontSizeLine6 = fontSizeLine6;
            IsBoldLine6 = isBoldLine6;
            IsItalicLine6 = isItalicLine6;
            Line7 = line7;
            FontSizeLine7 = fontSizeLine7;
            IsBoldLine7 = isBoldLine7;
            IsItalicLine7 = isItalicLine7;
            Line8 = line8;
            FontSizeLine8 = fontSizeLine8;
            IsBoldLine8 = isBoldLine8;
            IsItalicLine8 = isItalicLine8;
            Line9 = line9;
            FontSizeLine9 = fontSizeLine9;
            IsBoldLine9 = isBoldLine9;
            IsItalicLine9 = isItalicLine9;
            PageSizes = pageSizes;
            PageSize2 = pageSize2;
            PageSize3 = pageSize3;
            PageSize4 = pageSize4;
            Sample = sample;
            Footnote = footnote;
            InCharge = inCharge;
            RemDate = remDate;
            RemUser = remUser;
        }

        public static DefaultValue Parse(SqlDataReader reader)
        {
            DefaultValue objDefaultValue = new DefaultValue();

            objDefaultValue.DefaultValueId = (int)(DBNull.Value.Equals(reader[nameof(DefaultValueId)]) ? 0 : reader[nameof(DefaultValueId)]);
            objDefaultValue.FontFamilyName = (string)(DBNull.Value.Equals(reader[nameof(FontFamilyName)]) ? string.Empty : reader[nameof(FontFamilyName)]);
            objDefaultValue.Line1 = (string)(DBNull.Value.Equals(reader[nameof(Line1)]) ? string.Empty : reader[nameof(Line1)]);
            objDefaultValue.FontSizeLine1 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine1)]) ? 0 : reader[nameof(FontSizeLine1)]);
            objDefaultValue.IsBoldLine1 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine1)]) ? false : reader[nameof(IsBoldLine1)]);
            objDefaultValue.IsItalicLine1 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine1)]) ? false : reader[nameof(IsItalicLine1)]);
            objDefaultValue.Line2 = (string)(DBNull.Value.Equals(reader[nameof(Line2)]) ? string.Empty : reader[nameof(Line2)]);
            objDefaultValue.FontSizeLine2 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine2)]) ? 0 : reader[nameof(FontSizeLine2)]);
            objDefaultValue.IsBoldLine2 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine2)]) ? false : reader[nameof(IsBoldLine2)]);
            objDefaultValue.IsItalicLine2 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine2)]) ? false : reader[nameof(IsItalicLine2)]);
            objDefaultValue.Line3 = (string)(DBNull.Value.Equals(reader[nameof(Line3)]) ? string.Empty : reader[nameof(Line3)]);
            objDefaultValue.FontSizeLine3 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine3)]) ? 0 : reader[nameof(FontSizeLine3)]);
            objDefaultValue.IsBoldLine3 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine3)]) ? false : reader[nameof(IsBoldLine3)]);
            objDefaultValue.IsItalicLine3 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine3)]) ? false : reader[nameof(IsItalicLine3)]);
            objDefaultValue.Line4 = (string)(DBNull.Value.Equals(reader[nameof(Line4)]) ? string.Empty : reader[nameof(Line4)]);
            objDefaultValue.FontSizeLine4 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine4)]) ? 0 : reader[nameof(FontSizeLine4)]);
            objDefaultValue.IsBoldLine4 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine4)]) ? false : reader[nameof(IsBoldLine4)]);
            objDefaultValue.IsItalicLine4 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine4)]) ? false : reader[nameof(IsItalicLine4)]);
            objDefaultValue.Line5 = (string)(DBNull.Value.Equals(reader[nameof(Line5)]) ? string.Empty : reader[nameof(Line5)]);
            objDefaultValue.FontSizeLine5 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine5)]) ? 0 : reader[nameof(FontSizeLine5)]);
            objDefaultValue.IsBoldLine5 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine5)]) ? false : reader[nameof(IsBoldLine5)]);
            objDefaultValue.IsItalicLine5 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine5)]) ? false : reader[nameof(IsItalicLine5)]);
            objDefaultValue.Line6 = (string)(DBNull.Value.Equals(reader[nameof(Line6)]) ? string.Empty : reader[nameof(Line6)]);
            objDefaultValue.FontSizeLine6 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine6)]) ? 0 : reader[nameof(FontSizeLine6)]);
            objDefaultValue.IsBoldLine6 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine6)]) ? false : reader[nameof(IsBoldLine6)]);
            objDefaultValue.IsItalicLine6 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine6)]) ? false : reader[nameof(IsItalicLine6)]);
            objDefaultValue.Line7 = (string)(DBNull.Value.Equals(reader[nameof(Line7)]) ? string.Empty : reader[nameof(Line7)]);
            objDefaultValue.FontSizeLine7 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine7)]) ? 0 : reader[nameof(FontSizeLine7)]);
            objDefaultValue.IsBoldLine7 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine7)]) ? false : reader[nameof(IsBoldLine7)]);
            objDefaultValue.IsItalicLine7 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine7)]) ? false : reader[nameof(IsItalicLine7)]);
            objDefaultValue.Line8 = (string)(DBNull.Value.Equals(reader[nameof(Line8)]) ? string.Empty : reader[nameof(Line8)]);
            objDefaultValue.FontSizeLine8 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine8)]) ? 0 : reader[nameof(FontSizeLine8)]);
            objDefaultValue.IsBoldLine8 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine8)]) ? false : reader[nameof(IsBoldLine8)]);
            objDefaultValue.IsItalicLine8 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine8)]) ? false : reader[nameof(IsItalicLine8)]);
            objDefaultValue.Line9 = (string)(DBNull.Value.Equals(reader[nameof(Line9)]) ? string.Empty : reader[nameof(Line9)]);
            objDefaultValue.FontSizeLine9 = (int)(DBNull.Value.Equals(reader[nameof(FontSizeLine9)]) ? 0 : reader[nameof(FontSizeLine9)]);
            objDefaultValue.IsBoldLine9 = (bool)(DBNull.Value.Equals(reader[nameof(IsBoldLine9)]) ? false : reader[nameof(IsBoldLine9)]);
            objDefaultValue.IsItalicLine9 = (bool)(DBNull.Value.Equals(reader[nameof(IsItalicLine9)]) ? false : reader[nameof(IsItalicLine9)]);
            objDefaultValue.PageSizes = (string)(DBNull.Value.Equals(reader[nameof(PageSizes)]) ? string.Empty : reader[nameof(PageSizes)]);
            objDefaultValue.PageSize2 = (int)(DBNull.Value.Equals(reader[nameof(PageSize2)]) ? 0 : reader[nameof(PageSize2)]);
            objDefaultValue.PageSize3 = (int)(DBNull.Value.Equals(reader[nameof(PageSize3)]) ? 0 : reader[nameof(PageSize3)]);
            objDefaultValue.PageSize4 = (int)(DBNull.Value.Equals(reader[nameof(PageSize4)]) ? 0 : reader[nameof(PageSize4)]);
            objDefaultValue.Sample = (string)(DBNull.Value.Equals(reader[nameof(Sample)]) ? string.Empty : reader[nameof(Sample)]);
            objDefaultValue.Footnote = (string)(DBNull.Value.Equals(reader[nameof(Footnote)]) ? string.Empty : reader[nameof(Footnote)]);
            objDefaultValue.InCharge = (string)(DBNull.Value.Equals(reader[nameof(InCharge)]) ? string.Empty : reader[nameof(InCharge)]);
            if (!DBNull.Value.Equals(reader[nameof(RemDate)]))
                objDefaultValue.RemDate = DateTime.Parse(reader[nameof(RemDate)].ToString(), CultureInfo.CurrentCulture);
            objDefaultValue.RemUser = (string)(DBNull.Value.Equals(reader[nameof(RemUser)]) ? string.Empty : reader[nameof(RemUser)]);

            return objDefaultValue;
        }
        #endregion C O N S T R U C T O R S

        #region P R O P E R T I E S
        //============================================================================================

        private int _defaultValueId;
        public int DefaultValueId
        {
            get => _defaultValueId;
            set
            {
                _defaultValueId = value;
                OnPropertyChanged();
            }
        }

        private string _fontFamilyName = string.Empty;
        public string FontFamilyName
        {
            get => _fontFamilyName;
            set
            {
                _fontFamilyName = value;
                OnPropertyChanged();
            }
        }

        private string _line1 = string.Empty;
        public string Line1
        {
            get => _line1;
            set
            {
                _line1 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine1;
        public int FontSizeLine1
        {
            get => _fontSizeLine1;
            set
            {
                _fontSizeLine1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine1;
        public bool IsBoldLine1
        {
            get => _isBoldLine1;
            set
            {
                _isBoldLine1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine1;
        public bool IsItalicLine1
        {
            get => _isItalicLine1;
            set
            {
                _isItalicLine1 = value;
                OnPropertyChanged();
            }
        }

        private string _line2 = string.Empty;
        public string Line2
        {
            get => _line2;
            set
            {
                _line2 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine2;
        public int FontSizeLine2
        {
            get => _fontSizeLine2;
            set
            {
                _fontSizeLine2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine2;
        public bool IsBoldLine2
        {
            get => _isBoldLine2;
            set
            {
                _isBoldLine2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine2;
        public bool IsItalicLine2
        {
            get => _isItalicLine2;
            set
            {
                _isItalicLine2 = value;
                OnPropertyChanged();
            }
        }

        private string _line3 = string.Empty;
        public string Line3
        {
            get => _line3;
            set
            {
                _line3 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine3;
        public int FontSizeLine3
        {
            get => _fontSizeLine3;
            set
            {
                _fontSizeLine3 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine3;
        public bool IsBoldLine3
        {
            get => _isBoldLine3;
            set
            {
                _isBoldLine3 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine3;
        public bool IsItalicLine3
        {
            get => _isItalicLine3;
            set
            {
                _isItalicLine3 = value;
                OnPropertyChanged();
            }
        }

        private string _line4 = string.Empty;
        public string Line4
        {
            get => _line4;
            set
            {
                _line4 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine4;
        public int FontSizeLine4
        {
            get => _fontSizeLine4;
            set
            {
                _fontSizeLine4 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine4;
        public bool IsBoldLine4
        {
            get => _isBoldLine4;
            set
            {
                _isBoldLine4 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine4;
        public bool IsItalicLine4
        {
            get => _isItalicLine4;
            set
            {
                _isItalicLine4 = value;
                OnPropertyChanged();
            }
        }

        private string _line5 = string.Empty;
        public string Line5
        {
            get => _line5;
            set
            {
                _line5 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine5;
        public int FontSizeLine5
        {
            get => _fontSizeLine5;
            set
            {
                _fontSizeLine5 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine5;
        public bool IsBoldLine5
        {
            get => _isBoldLine5;
            set
            {
                _isBoldLine5 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine5;
        public bool IsItalicLine5
        {
            get => _isItalicLine5;
            set
            {
                _isItalicLine5 = value;
                OnPropertyChanged();
            }
        }

        private string _line6 = string.Empty;
        public string Line6
        {
            get => _line6;
            set
            {
                _line6 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine6;
        public int FontSizeLine6
        {
            get => _fontSizeLine6;
            set
            {
                _fontSizeLine6 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine6;
        public bool IsBoldLine6
        {
            get => _isBoldLine6;
            set
            {
                _isBoldLine6 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine6;
        public bool IsItalicLine6
        {
            get => _isItalicLine6;
            set
            {
                _isItalicLine6 = value;
                OnPropertyChanged();
            }
        }

        private string _line7 = string.Empty;
        public string Line7
        {
            get => _line7;
            set
            {
                _line7 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine7;
        public int FontSizeLine7
        {
            get => _fontSizeLine7;
            set
            {
                _fontSizeLine7 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine7;
        public bool IsBoldLine7
        {
            get => _isBoldLine7;
            set
            {
                _isBoldLine7 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine7;
        public bool IsItalicLine7
        {
            get => _isItalicLine7;
            set
            {
                _isItalicLine7 = value;
                OnPropertyChanged();
            }
        }

        private string _line8 = string.Empty;
        public string Line8
        {
            get => _line8;
            set
            {
                _line8 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine8;
        public int FontSizeLine8
        {
            get => _fontSizeLine8;
            set
            {
                _fontSizeLine8 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine8;
        public bool IsBoldLine8
        {
            get => _isBoldLine8;
            set
            {
                _isBoldLine8 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine8;
        public bool IsItalicLine8
        {
            get => _isItalicLine8;
            set
            {
                _isItalicLine8 = value;
                OnPropertyChanged();
            }
        }

        private string _line9 = string.Empty;
        public string Line9
        {
            get => _line9;
            set
            {
                _line9 = value;
                OnPropertyChanged();
            }
        }

        private int _fontSizeLine9;
        public int FontSizeLine9
        {
            get => _fontSizeLine9;
            set
            {
                _fontSizeLine9 = value;
                OnPropertyChanged();
            }
        }

        private bool _isBoldLine9;
        public bool IsBoldLine9
        {
            get => _isBoldLine9;
            set
            {
                _isBoldLine9 = value;
                OnPropertyChanged();
            }
        }

        private bool _isItalicLine9;
        public bool IsItalicLine9
        {
            get => _isItalicLine9;
            set
            {
                _isItalicLine9 = value;
                OnPropertyChanged();
            }
        }

        private string _pageSizes = string.Empty;
        public string PageSizes
        {
            get => _pageSizes;
            set
            {
                _pageSizes = value;
                OnPropertyChanged();
            }
        }

        private int _pageSize2;
        public int PageSize2
        {
            get => _pageSize2;
            set
            {
                _pageSize2 = value;
                OnPropertyChanged();
            }
        }

        private int _pageSize3;
        public int PageSize3
        {
            get => _pageSize3;
            set
            {
                _pageSize3 = value;
                OnPropertyChanged();
            }
        }

        private int _pageSize4;
        public int PageSize4
        {
            get => _pageSize4;
            set
            {
                _pageSize4 = value;
                OnPropertyChanged();
            }
        }

        private string _sample = string.Empty;
        public string Sample
        {
            get => _sample;
            set
            {
                _sample = value;
                OnPropertyChanged();
            }
        }

        private string _footnote = string.Empty;
        public string Footnote
        {
            get => _footnote;
            set
            {
                _footnote = value;
                OnPropertyChanged();
            }
        }

        private string _inCharge = string.Empty;
        public string InCharge
        {
            get => _inCharge;
            set
            {
                _inCharge = value;
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

	}
}