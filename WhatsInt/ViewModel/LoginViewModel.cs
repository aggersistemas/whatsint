using Microsoft.AspNetCore.Components;
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
        private NavigationManager Nav;

        #endregion

        public async void LoginButtonClick()
        {

            var isValid = await ValidateFields();

            if (isValid)
            {

            }
            else
            {

            }

            Nav.NavigateTo("/user", true);
        }

        private async Task<bool> ValidateFields()
        {
            UserDto user = new();

            user.Email = UserMail;
            user.Name = UserName;
            user.Password = Password;
            user.PasswordConfirmation = PasswordConfirmation;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync("https://localhost:7043/user/create", user);


            }

            return true;
        }
    }
}
