using System.Linq;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class TransacaoDao : Base.TransacaoDao<Transacao>, ITransacaoDao
    {
        private const int MAX_FECHADAS = 20;
        private const int MAX_EM_PRODUCAO = 20;
        private const int MAX_DIAS = 20;

        public override Transacao InitDto(IReader reader, Transacao dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Referencia = MockReader.GenerateCode(7);
            dto.Emissao = MockReader.GenerateDateTime(-MAX_DIAS);
            dto.Previsao = MockReader.GenerateDateTime(MAX_DIAS);
            dto.Expedicao = MockReader.GenerateDateTime(MAX_DIAS);
            dto.AvisoMensagem = MockReader.GeneratePhrase();
            dto.Observacao = MockReader.GeneratePhrase();

            return dto;
        }

        public override int GetCountFechadas(int codCliente)
        {
            return MockReader.GenerateInt32(MAX_FECHADAS);
        }

        public override int GetCountEmProducao(int codCliente)
        {
            return MockReader.GenerateInt32(MAX_EM_PRODUCAO);
        }

        public override string GetStmtFindAll()
        {
            return string.Empty;
        }

        public override string GetStmtFindAllByCodClienteAndCodNotaFiscal()
        {
            return string.Empty;
        }

        public override string GetStmtFindOneByCodEmpresaAndCodTransacao()
        {
            return string.Empty;
        }

        public override string GetStmtInsert()
        {
            return string.Empty;
        }

        public override string GetStmtCountFechadas()
        {
            return string.Empty;
        }

        public override string GetStmtCountEmProducao()
        {
            return string.Empty;
        }

        public override string GetStmtClose()
        {
            return string.Empty;
        }
    }
}