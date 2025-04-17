namespace Acme.Store.Auth.Validators
{
    public class SenhaValidation
    {
        public string Senha { get; }
        public SenhaValidation(string senha)
        {
            Senha = senha;
        }
    }
}
