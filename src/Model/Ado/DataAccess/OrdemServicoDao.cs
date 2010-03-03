using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoDao : DataAccessBase<OrdemServico>, IOrdemServicoDao
    {
        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ABRIR(
                @PCOD_CLIENTE,
                @POBSERVACAO,
                @PREFERENCIA,
                @PDESCRICAOARMACAO,
                @POBSERVACAOARMACAO,
                @PCOD_OTICALENTEMATERIAL,
                @PTIPOVT,
                @PTA,
                @PMD,
                @PDIAMETRO,
                @POBSERVACAOLENTE,
                @PDP,
                @PAA,
                @PEIXO,
                @PPONTE
            )";

        private const string SQL_STMT_INSERT_ITEM = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDITEM(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ITEM,
                @PQUANTIDADE
            )";

        private const string SQL_STMT_CLOSE = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_FECHAR(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO
            )";

        public override OrdemServico FetchDto(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        public override OrdemServico[] FindAll()
        {
            throw new NotImplementedException();
        }

        public OrdemServico[] FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        public override OrdemServico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override OrdemServico Insert(OrdemServico dto)
        {
            Helper.UsingCommand(Connection, c =>
            {
                c.CommandText = SQL_STMT_INSERT;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@POBSERVACAO", DbType.String, dto.Observacao);
                Helper.AddParameter(c, "@PREFERENCIA", DbType.String, dto.Referencia);
                Helper.AddParameter(c, "@PDESCRICAOARMACAO", DbType.String, dto.DescricaoArmacao);
                Helper.AddParameter(c, "@POBSERVACAOARMACAO", DbType.String, dto.ObservacaoArmacao);
                Helper.AddParameter(c, "@PCOD_OTICALENTEMATERIAL", DbType.Int32, dto.CodMaterial);
                Helper.AddParameter(c, "@PTIPOVT", DbType.Int32, dto.TipoVt);
                Helper.AddParameter(c, "@PTA", DbType.Decimal, dto.Ta);
                Helper.AddParameter(c, "@PMD", DbType.Decimal, dto.Md);
                Helper.AddParameter(c, "@PDIAMETRO", DbType.Decimal, dto.Diametro);
                Helper.AddParameter(c, "@POBSERVACAOLENTE", DbType.String, dto.ObservacaoLente);
                Helper.AddParameter(c, "@PDP", DbType.Decimal, dto.Dp);
                Helper.AddParameter(c, "@PAA", DbType.Decimal, dto.Aa);
                Helper.AddParameter(c, "@PEIXO", DbType.Decimal, dto.Eixo);
                Helper.AddParameter(c, "@PPONTE", DbType.Decimal, dto.Ponte);

                // Saída:
                var paramCodEmpresa = Helper.AddReturnParameter(c, "@RCOD_EMPRESA", DbType.Int32);
                var paramCodOs = Helper.AddReturnParameter(c, "@RCOD_ORDEMSERVICO", DbType.Int32);
                var paramNumero = Helper.AddReturnParameter(c, "@RNUMEROORDEMSERVICO", DbType.Int32);

                c.ExecuteNonQuery();

                dto.CodEmpresa = (int) paramCodEmpresa.Value;
                dto.CodOrdemServico = (int) paramCodOs.Value;
                dto.Numero = (int) paramNumero.Value;
            });

            return dto;
        }

        public virtual ServicoOrdemServico[] InsertItens(ServicoOrdemServico[] dtos)
        {
            foreach (var dto in dtos)
            {
                var servico = dto; // P/ evitar warning; objeto será acessado dentro do delegate.

                Helper.UsingCommand(Connection, c =>
                {
                    c.CommandText = SQL_STMT_INSERT_ITEM;

                    Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, servico.CodEmpresa);
                    Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, servico.CodOrdemServico);
                    Helper.AddParameter(c, "@PCOD_ITEM", DbType.String, servico.CodItem);
                    Helper.AddParameter(c, "@PQUANTIDADE", DbType.Decimal, servico.Quantidade);

                    c.ExecuteNonQuery();
                });
            }

            return dtos; // Deveriam ser atualizados a partir dos registros completos na base.
        }

        public override OrdemServico Update(OrdemServico dto)
        {
            throw new NotImplementedException();
        }

        public OrdemServico Close(OrdemServico dto)
        {
            Helper.UsingCommand(Connection, c => {
                c.CommandText = SQL_STMT_CLOSE;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodOrdemServico);

                c.ExecuteNonQuery();
            });

            return dto;
        }
    }
}
