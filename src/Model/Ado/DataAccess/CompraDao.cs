using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class CompraDao : DataAccessBase<Compra>, ICompraDao
    {
        private ProfundidadeConsultaTransacao profundidade;

        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                RCODEMPRESA,
                RCODTRANSACAO,
                RNUMEROORDEMSERVICO,
                RREFERENCIA,
                RDATAHORAEMISSAO,
                RDATAHORAPREVISAO,
                RDATAHORAEXPEDICAO,
                RCODETAPA,
                RAVISOMENSAGEM,
                RTIPO
            FROM
                STP_WEBORDEMSERVICO_CONSULTAR(@PCOD_CLIENTE)
        ";

        // TODO: [STP]
        private const string SQL_STMT_COUNT_FECHADAS = @"
            SELECT
                COUNT(*)
            FROM
                transacao
            WHERE
                (tipotransacao = 'OS') AND
                (dataencerramento = CURRENT_DATE) AND
                (cod_pessoa = @PCOD_CLIENTE)
        ";

        // TODO: [STP]
        private const string SQL_STMT_COUNT_EM_PRODUCAO = @"
            SELECT
                COUNT(*)
            FROM
                oticarascaixa OTRC
                INNER JOIN oticarasordemservico OTOS ON
                    (OTOS.cod_oticarasordemservico = OTRC.cod_oticarasordemservico)
                INNER JOIN pessoa PESS ON
                    (PESS.nome = OTOS.nomecliente)
            WHERE
                (OTRC.numerocaixa IS NOT NULL) AND
                (PESS.cod_pessoa = @PCOD_CLIENTE)
        ";

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

        private const string SQL_STMT_CLOSE = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_FECHAR(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO
            )";

        private static TipoCompra GetTipoCompra(IDataRecord record)
        {
            return (TipoCompra) Helper.ReadInt32(record, "RTIPO");
        }
        
        private static void InitCompra(Compra dto, IDataRecord record)
        {
            dto.CodEmpresa = Helper.ReadInt32(record, "RCODEMPRESA").Value;
            dto.CodTransacao = Helper.ReadInt32(record, "RCODTRANSACAO").Value;
            dto.Numero = Helper.ReadInt32(record, "RNUMEROORDEMSERVICO").Value;
            dto.Referencia = Helper.ReadString(record, "RREFERENCIA");
            dto.Emissao = Helper.ReadDateTime(record, "RDATAHORAEMISSAO").Value;
            dto.Previsao = Helper.ReadDateTime(record, "RDATAHORAPREVISAO");
            dto.Expedicao = Helper.ReadDateTime(record, "RDATAHORAEXPEDICAO");
            dto.Etapa = (TipoEtapa) Helper.ReadInt32(record, "RCODETAPA");
            dto.AvisoMensagem = Helper.ReadString(record, "RAVISOMENSAGEM");
            dto.Tipo = (TipoCompra) Helper.ReadInt32(record, "RTIPO");
        }

        private static void InitLente(OrdemServicoLente lente, TipoLente tipo)
        {
            // TODO: [Detalhe Transacao] Inicializar dto com dados da lente via OrdemServicoLenteDao.
        }
        
        public override Compra FetchDto(IDataRecord record)
        {
            Compra result = null;
            OrdemServico os = null;
            TipoCompra? tipoCompra = null;

            if (profundidade >= ProfundidadeConsultaTransacao.Especializacao)
            {
                tipoCompra = GetTipoCompra(record);

                switch (tipoCompra)
                {
                    case TipoCompra.Pedido:
                        result = new Pedido();
                        break;
                    case TipoCompra.OrdemServico:
                        result = new OrdemServico();
                        os = (OrdemServico) result;
                        os.DescricaoArmacao = Helper.ReadString(record, "");
                        os.ObservacaoArmacao = Helper.ReadString(record, "");
                        os.CodMaterial = Helper.ReadInt32(record, "").Value;
                        os.TipoVt = Helper.ReadInt32(record, "").Value;
                        os.Ta = Helper.ReadDecimal(record, "").Value;
                        os.Md = Helper.ReadDecimal(record, "").Value;
                        os.Diametro = Helper.ReadDecimal(record, "").Value;
                        os.ObservacaoLente = Helper.ReadString(record, "");
                        os.Dp = Helper.ReadDecimal(record, "").Value;
                        os.Aa = Helper.ReadDecimal(record, "").Value;
                        os.Eixo = Helper.ReadDecimal(record, "").Value;
                        os.Ponte = Helper.ReadDecimal(record, "").Value;
                        break;
                    default:
                        throw new InvalidOperationException(string.Format("Valor inválido para tipoCompra. [{0}]", tipoCompra));
                }
            }

            if (result == null)
            {
                result = new Compra();
            }

            InitCompra(result, record);

            if (profundidade >= ProfundidadeConsultaTransacao.Item)
            {
                var itemTransacaoDao = new ItemTransacaoDao {Session = Session};
                result.Itens = itemTransacaoDao.FindByTransacao(result.CodEmpresa, result.CodTransacao);
            }

            if (profundidade == ProfundidadeConsultaTransacao.Completa && tipoCompra == TipoCompra.OrdemServico)
            {
                if (os == null) throw new InvalidOperationException();

                InitLente(os.LenteOd, TipoLente.OlhoDireito);
                InitLente(os.LenteOe, TipoLente.OlhoEsquerdo);
            }

            return result;
        }

        public override Compra[] FindAll()
        {
            throw new NotImplementedException();
        }

        public Compra[] FindAll(int codCliente, ProfundidadeConsultaTransacao profundidade)
        {
            this.profundidade = profundidade;

            Compra[] result = null;

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ALL;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                result = FetchDtos(c);
            });

            return result;
        }

        public override Compra FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public virtual Compra FindByPrimaryKey(int codEmpresa, int codTransacao)
        {
            Compra result = null;

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ALL;
                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                Helper.AddParameter(c, "@PCOD_TRANSACAO", DbType.Int32, codTransacao);
                result = FetchDto(c);
            });

            return result;
        }

        public override Compra Insert(Compra dto)
        {
            var os = dto as OrdemServico; // Se os não for OrdemServico, então os = null.

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_INSERT;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@POBSERVACAO", DbType.String, dto.Observacao);
                Helper.AddParameter(c, "@PREFERENCIA", DbType.String, dto.Referencia);

                // NOTE: esse if poderia ser removido via uma nova classe OrdemServicoDao
                if (os != null)
                {
                    Helper.AddParameter(c, "@PDESCRICAOARMACAO", DbType.String, os.DescricaoArmacao);
                    Helper.AddParameter(c, "@POBSERVACAOARMACAO", DbType.String, os.ObservacaoArmacao);
                    Helper.AddParameter(c, "@PCOD_OTICALENTEMATERIAL", DbType.Int32, os.CodMaterial);
                    Helper.AddParameter(c, "@PTIPOVT", DbType.Int32, os.TipoVt);
                    Helper.AddParameter(c, "@PTA", DbType.Decimal, os.Ta);
                    Helper.AddParameter(c, "@PMD", DbType.Decimal, os.Md);
                    Helper.AddParameter(c, "@PDIAMETRO", DbType.Decimal, os.Diametro);
                    Helper.AddParameter(c, "@POBSERVACAOLENTE", DbType.String, os.ObservacaoLente);
                    Helper.AddParameter(c, "@PDP", DbType.Decimal, os.Dp);
                    Helper.AddParameter(c, "@PAA", DbType.Decimal, os.Aa);
                    Helper.AddParameter(c, "@PEIXO", DbType.Decimal, os.Eixo);
                    Helper.AddParameter(c, "@PPONTE", DbType.Decimal, os.Ponte);
                }
                else
                {
                    // Parâmetros abaixo só são utilizados para OS, portanto vão em branco:
                    Helper.AddParameter(c, "@PREFERENCIA", DbType.String, DBNull.Value);
                    Helper.AddParameter(c, "@PDESCRICAOARMACAO", DbType.String, DBNull.Value);
                    Helper.AddParameter(c, "@POBSERVACAOARMACAO", DbType.String, DBNull.Value);
                    Helper.AddParameter(c, "@PCOD_OTICALENTEMATERIAL", DbType.Int32, DBNull.Value);
                    Helper.AddParameter(c, "@PTIPOVT", DbType.Int32, 0);
                    Helper.AddParameter(c, "@PTA", DbType.Decimal, DBNull.Value);
                    Helper.AddParameter(c, "@PMD", DbType.Decimal, DBNull.Value);
                    Helper.AddParameter(c, "@PDIAMETRO", DbType.Decimal, DBNull.Value);
                    Helper.AddParameter(c, "@POBSERVACAOLENTE", DbType.String, DBNull.Value);
                    Helper.AddParameter(c, "@PDP", DbType.Decimal, DBNull.Value);
                    Helper.AddParameter(c, "@PAA", DbType.Decimal, DBNull.Value);
                    Helper.AddParameter(c, "@PEIXO", DbType.Decimal, DBNull.Value);
                    Helper.AddParameter(c, "@PPONTE", DbType.Decimal, DBNull.Value);
                }

                // Saída:
                var paramCodEmpresa = Helper.AddReturnParameter(c, "@RCOD_EMPRESA", DbType.Int32);
                var paramCodOs = Helper.AddReturnParameter(c, "@RCOD_ORDEMSERVICO", DbType.Int32);
                var paramNumero = Helper.AddReturnParameter(c, "@RNUMEROORDEMSERVICO", DbType.Int32);

                c.ExecuteNonQuery();

                dto.CodEmpresa = (int)paramCodEmpresa.Value;
                dto.CodTransacao = (int)paramCodOs.Value;
                dto.Numero = (int)paramNumero.Value;
            });

            if (os != null)
            {
                var ordemServicoLenteDao = new OrdemServicoLenteDao {Session = Session};

                // Como só agora foi obtido os.CodTransacao e os.CodEmpresa, atualizo os demais DTO's:
                os.LenteOd.CodTransacao = os.CodTransacao;
                os.LenteOd.CodEmpresa = os.CodEmpresa;
                os.LenteOe.CodTransacao = os.CodTransacao;
                os.LenteOe.CodEmpresa = os.CodEmpresa;

                // NOTE: Por enquanto a descrição da lente vai ser igual ao nome da família - isso irá mudar no futuro.
                var familiaDao = new FamiliaDao {Session = Session};
                var familiaOd = familiaDao.FindByPrimaryKey(os.LenteOd.CodFamilia);
                var familiaOe = familiaDao.FindByPrimaryKey(os.LenteOe.CodFamilia);
                os.LenteOd.Descricao = familiaOd.Descricao;
                os.LenteOe.Descricao = familiaOe.Descricao;

                // Grava lentes:
                ordemServicoLenteDao.Insert(os.LenteOd);
                ordemServicoLenteDao.Insert(os.LenteOe);
            }

            if (dto.Itens != null)
            {
                foreach (var item in dto.Itens)
                {
                    item.CodTransacao = dto.CodTransacao;
                    item.CodEmpresa = dto.CodEmpresa;
                }

                // Grava os serviços que serão executados na OS:
                var itemTransacaoDao = new ItemTransacaoDao {Session = Session};
                itemTransacaoDao.Insert(dto.Itens);
            }

            return dto;
        }

        public override Compra Update(Compra dto)
        {
            throw new NotImplementedException();
        }

        protected int ExecuteScalarInt32(string sqlStmt, int codCliente)
        {
            var result = 0;

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = sqlStmt;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                result = Convert.ToInt32(c.ExecuteScalar());
            });

            return result;
        }

        public int GetCountFechadas(int codCliente)
        {
            return ExecuteScalarInt32(SQL_STMT_COUNT_FECHADAS, codCliente);
        }

        public int GetCountEmProducao(int codCliente)
        {
            return ExecuteScalarInt32(SQL_STMT_COUNT_EM_PRODUCAO, codCliente);
        }

        public Compra Close(Compra dto)
        {
            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_CLOSE;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);

                c.ExecuteNonQuery();
            });

            return dto;
        }
    }
}