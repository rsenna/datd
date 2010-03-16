using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoLenteDao : DataAccessBase<OrdemServicoLente>, IOrdemServicoLenteDao
    {
        public override OrdemServicoLente Update(OrdemServicoLente dto)
        {
            throw new NotImplementedException();
        }

        public OrdemServicoLente FindByPrimaryKey(int codEmpresa, int codTransacao, TipoLente tipoLente)
        {
            var result = FindByPrimaryKey(null);

            result.CodEmpresa = codEmpresa;
            result.CodTransacao = codTransacao;
            result.TipoLente = tipoLente;

            return result;
        }

        public IEnumerable<OrdemServicoLente> FindAll(int codOrdemServico)
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente Insert(OrdemServicoLente dto)
        {
            return dto;
        }

        public override OrdemServicoLente InitDto(OrdemServicoLente dto)
        {
            dto.CodEmpresa = GenerateInt32();
            dto.CodTransacao = GenerateInt32();
            dto.TipoLente = (TipoLente) GenerateInt32(1, 2);
            dto.Descricao = GenerateName(3);
            dto.LongeEsf = GenerateDecimal(-9999.99m, 9999.99m, "###0.00");
            dto.LongeCil = GenerateDecimal(-9.99m, 9.99m, "0.00");
            dto.LongeEixo = GenerateDecimal(0, 180, "##0");
            dto.Adicao = GenerateDecimal(-9.99m, 9.99m, "0.00");
            dto.PertoEsf = GenerateDecimal(-9999.99m, 9999.99m, "###0.00");
            dto.PertoCil = GenerateDecimal(-9.99m, 9.99m, "0.00");
            dto.PertoEixo = GenerateDecimal(0, 180, "##0");
            dto.Dnp = GenerateDecimal(-99.9m, 99.9m, "#0.0");
            dto.Alt = GenerateDecimal(-9999.99m, 9999.99m, "###0.00");

            return dto;
        }
    }
}