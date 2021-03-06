﻿using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class NotaFiscalDao : Base.NotaFiscalDao
    {
        public override NotaFiscal InitDto(IReader reader, NotaFiscal dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Data = MockReader.GenerateDateTime(-10);
            dto.Total = MockReader.GenerateDecimal(20000);

            // Corrige geração do xml:
            dto.NfeXml = dto.Nfe? MockReader.GenerateXml() : null;

            return dto;
        }

        public override string GetStmtFindAllByCodCliente()
        {
            return string.Empty;
        }

        public override string GetStmtFindAllByCodClienteAndCodFatura()
        {
            return string.Empty;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return string.Empty;
        }
    }
}
