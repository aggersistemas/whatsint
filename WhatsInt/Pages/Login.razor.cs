using WhatsInt.Model;

namespace WhatsInt.Pages
{
    public partial class Login
    {
        #region Criação de Variaveis

        private string userMail;
        private string userName;
        private string password;
        private string passwordConfirmation;

        #endregion

        private void LoginButtonClick()
        {

            var isValid = ValidateFields();

            if ()
            {

            }
            else
            {

            }

            Nav.NavigateTo("/user", true);
        }

        private bool ValidateFields()
        {
            UserDto user = new();

            user.Email = userMail;
            user.Name = userName;
            user.Password = password;
            user.PasswordConfirmation = passwordConfirmation;



            return true;
        }
    }
}