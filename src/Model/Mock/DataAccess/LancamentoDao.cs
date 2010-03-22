using System;
using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class LancamentoDao : Base.LancamentoDao
    {
        public override Lancamento InitDto(IReader reader, Lancamento dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Vencimento = MockReader.GenerateDateTime(-5, 5);
            dto.Pagamento = MockReader.Maybe<int, DateTime>(MockReader.GenerateDateTime, -5, 50);
            dto.Total = MockReader.GenerateDecimal(20000);

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
