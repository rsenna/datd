using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class TransacaoDao : TransacaoDao<Transacao>, ITransacaoDao { }

    public abstract class TransacaoDao<T> : DataAccessBase<T>, ITransacaoDao<T>
        where T : Transacao, new()
    {
        private const int MAX_FECHADAS = 20;
        private const int MAX_EM_PRODUCAO = 20;
        private const int MAX_DIAS = 20;

        public override T InitDto(T dto)
        {
            dto.Tipo = (TipoTransacao) GenerateInt32(1, 2);
            dto.CodEmpresa = GenerateInt32();
            dto.CodTransacao = GenerateInt32();
            dto.Numero = GenerateInt32();
            dto.Referencia = GenerateCode(7);
            dto.Emissao = GenerateDateTime(-MAX_DIAS);
            dto.Previsao = GenerateDateTime(MAX_DIAS);
            dto.Expedicao = GenerateDateTime(MAX_DIAS);
            dto.Etapa = (TipoEtapa) GenerateInt32(4);
            dto.AvisoMensagem = GeneratePhrase();
            dto.Observacao = GeneratePhrase();

            if (Depth > QueryDepth.FirstLevel)
            {
                var itemTransacaoDao = new ItemTransacaoDao {Depth = GetDetailDepth()};
                dto.Itens = itemTransacaoDao.FindAll(dto.CodEmpresa, dto.CodTransacao);
            }

            return dto;
        }

        public override T FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override T Update(T dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll(int codCliente)
        {
            return FindAll();
        }

        public T FindByPrimaryKey(int codEmpresa, int codTransacao)
        {
            return InitDto(new T());
        }

        public int GetCountFechadas(int codCliente)
        {
            return GenerateInt32(MAX_FECHADAS);
        }

        public int GetCountEmProducao(int codCliente)
        {
            return GenerateInt32(MAX_EM_PRODUCAO);
        }

        public T Close(T dto)
        {
            return dto;
        }

        public override T Insert(T dto)
        {
            dto.Numero = GenerateInt32();
            return dto;
        }
    }
}