using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MvvmBlazor.ViewModel;
using WhatsInt.Model;
using WhatsInt.Pages;

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

        public static string LabelCadasterText = "Ainda não possui cadastro? Cadastre-se Aqui";
        public static string labelButtonLogin = "Log In";
        public static bool onCadaster;
        private NavigationManager Nav;

        #endregion

        public void LoginButtonClick()
        {
            if (onCadaster)
            {
                var isValid = ValidateFields();

                if (!isValid)
                    return;

                UserCreate();
            }
            else
            {
                ErrorMessage = "";

                UserLogin();
            }
        }

        private void UserLogin()
        {
            throw new NotImplementedException();
        }


        private async void UserCreate()
        {
            #region Criação do Objeto UserDto

            UserDto user = new();

            user.Email = UserMail;
            user.Name = UserName;
            user.Password = Password;

            #endregion

            using (var client = new HttpClient())
            {
                Navigate(await PostApi(client, user));
            }
        }

        private static async Task<HttpResponseMessage> PostApi(HttpClient client, UserDto user)
        {
            var response = await client.PostAsJsonAsync("https://localhost:7043/user/create", user);
            return response;
        }

        private void Navigate(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Created)
                Nav.NavigateTo("/interaction", true);

            else if (response.StatusCode == HttpStatusCode.Conflict)
                ErrorMessage = "JA EXISTE UM USUARIO CADASTRADO COM ESSE E-MAIL";

            else
                ErrorMessage = "ERRO AO CRIAR USUARIO! TENTE NOVAMENTE MAIS TARDE";
        }

        private bool ValidateFields()
        {
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
