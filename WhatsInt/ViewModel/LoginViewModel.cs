using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MvvmBlazor.ViewModel;
using WhatsInt.Model;

namespace WhatsInt.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Criação de Variaveis

        public string UserMail;
        public string UserName;
        public string Password;
        public string PasswordConfirmation;
        public static string ErrorMessage;

        public static bool onLogin;
        private NavigationManager Nav;

        #endregion

        public async void LoginButtonClick()
        {

            var isValid = await ValidateFields();

            if (isValid)
            {
                if (onLogin)
                {

                }
                else
                {
                    CreateUser();
                }

            }
            else
            {
                ShowErrorMessage();
            }

            //Nav.NavigateTo("/user", true);
        }

        private void ShowErrorMessage()
        {

        }

        private void CreateUser()
        {
            /*
            UserDto user = new();

            user.Email = UserMail;
            user.Name = UserName;
            user.Password = Password;
            user.PasswordConfirmation = PasswordConfirmation;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync("https://localhost:7043/user/create", user);


            }
             */

            return;
        }

        private async Task<bool> ValidateFields()
        {
#if DEBUG
            return true;
#endif

            #region Validação Senha

            if (Password != PasswordConfirmation)
            {
                ErrorMessage = "AS SENHAS NÃO COINCIDEM";
                return false;
            }


            if (Password.Length < 6)
            {
                ErrorMessage = "A SENHA DEVE CONTER PELO MENOS 6 CARACTERES";
                return false;
            }

            #endregion

            ErrorMessage = "";
            return true;
        }
    }
}
