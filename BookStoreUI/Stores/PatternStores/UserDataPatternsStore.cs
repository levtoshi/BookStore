using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace BookStoreUI.Stores.PatternStores
{
    public class UserDataPatternsStore
    {
        public string NamePattern { get; }
        public string LoginPattern { get; }
        public string PasswordPattern { get; }

        public string NameError { get; }
        public string LoginError { get; }
        public string PasswordError { get; }


        public UserDataPatternsStore()
        {
            NamePattern = @"^[a-zA-Z]{2,30}$";
            LoginPattern = @"^[a-zA-Z0-9_@.]{4,40}$";
            PasswordPattern = @"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{6,24}$";

            NameError = "must have length from 2 to 30";
            LoginError = "must have length from 4 to 40";
            PasswordError = "must have length from 6 to 24";
        }
    }
}