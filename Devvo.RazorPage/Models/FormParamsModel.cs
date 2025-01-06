namespace Devvo.RazorPage.Models
{
    public class FormParamsModel
    {
        public string Titulo { get; set; }
        public List<InputModel> Inputs { get; set; } = new List<InputModel>();
        public string BotaoTexto { get; set; }
        public string Action { get; set; }
    }
}
