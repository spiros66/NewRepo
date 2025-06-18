//*************************************************************************************************
// Assembly         : Sillogos v2.0
// Author           : Óðýñïò
// Created          : 01-04-2023
//
// Last Modified By : Óðýñïò
// Last Modified On : 01-04-2023
// Description      : 
//
// Copyright        : (c) Spiros. All rights reserved.
//*************************************************************************************************

namespace Sillogos.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {

        #region P R O P E R T I E S
        //============================================================================================
        
         private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
       #endregion P R O P E R T I E S

        #region C O N S T R U C T O R
        //============================================================================================
        public HomePageViewModel()
        {

        }

        #endregion

    }
}
