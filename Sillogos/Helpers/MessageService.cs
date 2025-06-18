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
using System.Windows;

namespace Sillogos.Helpers
{
    public class MessageService
    {
        public static Action<string> ShowMessage;
        public static Action<string> ShowErrorMessage;
        public static Func<string, MessageBoxResult> ShowMessageWithConfirm;
    }
}