﻿using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IDocumentoHabilitadoDomainService : IDomainServiceBase<DocumentoHabilitado>
    {
        DocumentoHabilitado ObterPorDocumento(string cpfCnpj);
    }
}
