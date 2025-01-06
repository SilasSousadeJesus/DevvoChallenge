namespace Devvo.RazorPage.Models
{
    public class InfoUsuarioLogadoModel
    {
        public InfoUsuarioLogadoModel()
        {

        }
        public InfoUsuarioLogadoModel(string id, string email, string nome) {
            Id = id;
            Email = email;
            Nome = nome; 
        }
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
