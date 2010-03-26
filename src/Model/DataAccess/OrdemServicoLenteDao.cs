using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class OrdemServicoLenteDao : DataAccessBase<OrdemServicoLente>, IOrdemServicoLenteDao
    {
        public abstract string GetStmtFindAllByCodEmpresaAndCodTransacao();
        public abstract string GetStmtInsert();

        public override OrdemServicoLente InitDto(IReader reader, OrdemServicoLente dto)
        {
            dto.CodEmpresa = reader.ReadRequired<int>("cod_empresa");
            dto.CodTransacao = reader.ReadRequired<int>("cod_ordemservicootica");
            dto.TipoLente = reader.ReadRequired<TipoLente>("cod_ordemservicooticalente");
            dto.Descricao = reader.ReadOptional("descricaolente");
            dto.LongeEsf = reader.ReadOptional<decimal>("longe_esf");
            dto.LongeCil = reader.ReadOptional<decimal>("longe_cil");
            dto.LongeEixo = reader.ReadOptional<decimal>("longe_eixo");
            dto.Adicao = reader.ReadOptional<decimal>("adicao");
            dto.PertoEsf = reader.ReadOptional<decimal>("perto_esf");
            dto.PertoCil = reader.ReadOptional<decimal>("perto_cil");
            dto.PertoEixo = reader.ReadOptional<decimal>("perto_eixo");
            dto.Dnp = reader.ReadOptional<decimal>("dnp");
            dto.Alt = reader.ReadOptional<decimal>("alt");

            return dto;
        }

        public override IEnumerable<OrdemServicoLente> FindAll()
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public virtual OrdemServicoLente FindByPrimaryKey(int codEmpresa, int codTransacao, TipoLente tipoLente)
        {
            var result = new OrdemServicoLente();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodEmpresaAndCodTransacao();

                c.AddParameter("@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                c.AddParameter("@PCOD_ORDEMSERVICOOTICA", DbType.Int32, codTransacao);
                c.AddParameter("@PCOD_ORDEMSERVICOOTICALENTE", DbType.Int32, (int) tipoLente);

                InitDto(c, result);
            }

            return result;
        }

        public override OrdemServicoLente Update(OrdemServicoLente dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemServicoLente> FindAll(int codOrdemServico)
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente Insert(OrdemServicoLente dto)
        {
            using (var c = Session.CreateCommand()) {
                c.CommandText = GetStmtInsert();

                c.AddParameter("@PCOD_EMPRESA", DbType.Int32, dto.CodEmpresa);
                c.AddParameter("@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);
                c.AddParameter("@PCOD_ORDEMSERVICOOTICALENTE", DbType.Int32, (int) dto.TipoLente);
                c.AddParameter("@PDESCRICAOLENTE", DbType.String, dto.Descricao);
                c.AddParameter("@PLONGE_ESF", DbType.Decimal, dto.LongeEsf);
                c.AddParameter("@PLONGE_CIL", DbType.Decimal, dto.LongeCil);
                c.AddParameter("@PLONGE_EIXO", DbType.Decimal, dto.LongeEixo);
                c.AddParameter("@PADICAO", DbType.Decimal, dto.Adicao);
                c.AddParameter("@PPERTO_ESF", DbType.Decimal, dto.PertoEsf);
                c.AddParameter("@PPERTO_CIL", DbType.Decimal, dto.PertoCil);
                c.AddParameter("@PPERTO_EIXO", DbType.Decimal, dto.PertoEixo);
                c.AddParameter("@PDNP", DbType.Decimal, dto.Dnp);
                c.AddParameter("@PALT", DbType.Decimal, dto.Alt);

                c.Execute();
            }

            return dto;
        }
    }
}