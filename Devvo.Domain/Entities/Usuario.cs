using Microsoft.AspNetCore.Identity;

namespace Devvo.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public string? Nome { get; set; }
        public virtual List<Anel>? Aneis { get; set; }
    }
}
