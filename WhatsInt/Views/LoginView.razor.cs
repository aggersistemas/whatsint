namespace WhatsInt.Views
{
    public partial class LoginView
    {
        private string userMail;
        private string userName;
        private string password;
        private string passwordConfirmation;

        private string _labelCadasterText;
        public string LabelCadasterText = "Ainda não possui cadastro? Cadastre-se Aqui";

        private bool onLogin;

        private string labelButtonLogin = "Log In";

        public void LoginButtonClick()
        {

        }

        public void CadasterLabelClick()
        {
            onLogin = !onLogin;
            switch (LabelCadasterText)
            {
                case "Ainda não possui cadastro? Cadastre-se Aqui":
                    LabelCadasterText = "Ja possui cadastro? Clique aqui para logar";
                    labelButtonLogin = "Cadastrar";
                    _labelCadasterText = LabelCadasterText;
                    return;

                case "Ja possui cadastro? Clique aqui para logar":
                    LabelCadasterText = "Ainda não possui cadastro? Cadastre-se Aqui";
                    labelButtonLogin = "Log In";
                    _labelCadasterText = LabelCadasterText;
                    return;
            }
        }
    }
}
