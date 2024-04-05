using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName} ";
                CurrentUserAccount.TextFilePath = user.TextFilePath;
                CurrentUserAccount.PhotoFilePath = user.PhotoFilePath;
                if (!string.IsNullOrEmpty(user.TextFilePath))
                {
                    CurrentUserAccount.TextFileContent = System.IO.File.ReadAllText(user.TextFilePath);
                }
                else
                {
                    CurrentUserAccount.TextFileContent = "No text file available";
                }
            }
            else
            {
                CurrentUserAccount.DisplayName="Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
