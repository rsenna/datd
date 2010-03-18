using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class NotaFiscalDao : DataAccessBase<NotaFiscal>, INotaFiscalDao
    {
        public override NotaFiscal InitDto(IDataRecord record, NotaFiscal dto)
        {
            /*
            dto.CodNotaFiscal = GenerateInt32();
            dto.CodCliente = GenerateInt32();
            dto.CodFatura = GenerateInt32();
            dto.Numero = GenerateInt32();
            dto.Data = GenerateDateTime(-10);
            dto.Total = GenerateDecimal(20000);
            dto.Nfe = GenerateBoolean();
            */

            if (Depth > QueryDepth.FirstLevel)
            {
                var transacaoDao = new TransacaoDao {Depth = GetDetailDepth()};
                dto.Transacoes = transacaoDao.FindAll(dto.CodCliente, dto.CodNotaFiscal).ToArray();
            }

            return dto;
        }

        public override IEnumerable<NotaFiscal> FindAll()
        {
            throw new NotImplementedException();
        }

        // TODO: implementação
        public IEnumerable<NotaFiscal> FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        // TODO: implementação
        public IEnumerable<NotaFiscal> FindAll(int codCliente, int codFatura)
        {
            throw new NotImplementedException();
        }

        public override NotaFiscal FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        // TODO: implementação
        public NotaFiscal FindByPrimaryKey(int codNotaFiscal)
        {
            throw new NotImplementedException();
        }

        // TODO: implementação
        public string GetXml(int codNotaFiscal)
        {
            throw new NotImplementedException();
        }

        public override NotaFiscal Insert(NotaFiscal dto)
        {
            throw new NotImplementedException();
        }

        public override NotaFiscal Update(NotaFiscal dto)
        {
            throw new NotImplementedException();
        }
    }
}