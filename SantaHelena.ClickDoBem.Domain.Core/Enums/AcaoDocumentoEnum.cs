using System.ComponentModel;

namespace SantaHelena.ClickDoBem.Domain.Core.Enums
{
    public enum AcaoDocumento
    {

        [Description("Incluir documento")]
        Inclusao = 1,

        [Description("Alterar documento")]
        Alteracao = 2,

        [Description("Documento ou situação inválida")]
        DocumentoOuSituacaoInvalida = 3,

        [Description("Documento duplicado")]
        DocumentoDuplicado = 4

    }
}
