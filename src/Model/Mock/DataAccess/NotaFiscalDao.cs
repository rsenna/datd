﻿using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class NotaFiscalDao : DataAccessBase<NotaFiscal>, INotaFiscalDao
    {
        public override NotaFiscal InitDto(NotaFiscal dto)
        {
            dto.CodNotaFiscal = GenerateInt32();
            dto.CodCliente = GenerateInt32();
            dto.CodFatura = GenerateInt32();
            dto.Numero = GenerateInt32();
            dto.Data = GenerateDateTime(-10);
            dto.Total = GenerateDecimal(20000);

            if (Depth > QueryDepth.FirstLevel)
            {
                var transacaoDao = new TransacaoDao {Depth = GetDetailDepth()};
                dto.Transacoes = transacaoDao.FindAll();
            }

            return dto;
        }

        public IEnumerable<NotaFiscal> FindAll(int codCliente)
        {
            return FindAll();
        }

        public override NotaFiscal FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        public NotaFiscal FindByPrimaryKey(int codNotaFiscal)
        {
            return InitDto(new NotaFiscal());
        }

        public string GetXml(int codNotaFiscal)
        {
            return GenerateXml();
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