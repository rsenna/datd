#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using NLipsum.Core;

#endregion

namespace Dataweb.Dilab.Model.Mock
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
        where T : DataTransferBase, new()
    {
        public const int FIND_ALL_MAX_COUNT = 100;
        public const int MAX_NAME_LENGTH = 50;

        public static readonly string NameGenerator = Lipsums.LoremIpsum;
        public static readonly string TextGenerator = Lipsums.LoremIpsum;

        private static readonly Random random = new Random();

        public QueryDepth Depth { get; set; }
        public ISession Session { get; set; }

        public void Dispose() { }

        public virtual T FindByPrimaryKey(object pk)
        {
            return InitDto(new T());
        }

        public virtual T Insert(T dto)
        {
            return dto;
        }

        public virtual T Update(T dto)
        {
            return dto;
        }

        public virtual IEnumerable<T> FindAll()
        {
            var count = GenerateInt32(FIND_ALL_MAX_COUNT);
            var result = new List<T>();

            for (var i = 0; i < count; i++)
            {
                result.Add(InitDto(new T()));
            }

            return result;
        }

        public abstract T InitDto(T dto);

        // TODO: método teve que ser repetido no Mock e no Ado - refatorar para classe básica ou usando composição
        protected QueryDepth GetDetailDepth()
        {
            switch (Depth)
            {
                case QueryDepth.None:
                case QueryDepth.FirstLevel:
                    return QueryDepth.None;

                case QueryDepth.Complete:
                    return QueryDepth.Complete;

                default:
                    return Depth - 1;
            }
        }

        protected static DateTime GenerateDateTime(int intervaloDias)
        {
            return DateTime.Now.AddDays(GenerateInt32(intervaloDias));
        }

        protected static DateTime GenerateDateTime(int inicio, int fim)
        {
            return DateTime.Now.AddDays(GenerateInt32(inicio, fim));
        }

        protected static int GenerateInt32(int maxValue)
        {
            var rand = random.Next(0, Math.Abs(maxValue) + 1);
            return rand * Math.Sign(maxValue);
        }

        protected static int GenerateInt32(int minValue, int maxValue)
        {
            return GenerateInt32(maxValue - minValue) + minValue;
        }

        protected static int GenerateInt32()
        {
            return GenerateInt32(int.MaxValue - 1);
        }

        protected static decimal GenerateDecimal()
        {
            var idx = random.NextDouble();
            return decimal.MaxValue * (decimal) idx;
        }

        protected static decimal GenerateDecimal(decimal maxValue)
        {
            var idx = random.NextDouble();
            return (maxValue + 1) * (decimal)idx;
        }

        protected static decimal GenerateDecimal(decimal minValue, decimal maxValue)
        {
            return GenerateDecimal(maxValue - minValue) + minValue;
        }

        protected static decimal GenerateDecimal(decimal minValue, decimal maxValue, string format)
        {
            var result = GenerateDecimal(minValue, maxValue);
            return Convert.ToDecimal(result.ToString(format));
        }

        protected static string GeneratePhrase()
        {
            return LipsumGenerator.Generate(1, Features.Sentences, FormatStrings.Sentence.Phrase, TextGenerator);
        }

        protected static string GenerateParagraph()
        {
            return LipsumGenerator.Generate(1, Features.Paragraphs, FormatStrings.Unformatted, TextGenerator);
        }

        protected static string GenerateCode(int length)
        {
            var result = new char[length];

            for (var i = 0; i < length; i++)
            {
                var digit = (char) ('0' + (char) GenerateInt32(9));
                result[i] = (i + 1) % 4 == 0? '.' : digit;
            }

            return new string(result);
        }

        protected static Boolean GenerateBoolean()
        {
            return Convert.ToBoolean(GenerateInt32(1));
        }

        protected static string GenerateIdentifier(int words)
        {
            return LipsumGenerator.Generate(words, Features.Words, FormatStrings.Unformatted, NameGenerator).Trim().Replace(' ', '.');
        }

        protected static string GenerateEmail()
        {
            return string.Format(
                "{0}@{1}.com",
                GenerateIdentifier(2),
                GenerateIdentifier(1)
            );
        }

        protected static string GenerateName(int words)
        {
            var result = LipsumGenerator.Generate(words, Features.Words, FormatStrings.Unformatted, NameGenerator);
            return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(result);
        }

        protected static IEnumerable<string> GenerateWords(int words)
        {
            var text = LipsumGenerator.Generate(words, Features.Words, FormatStrings.Unformatted, TextGenerator);
            return text.Split(' ');
        }

        protected static string GenerateXml()
        {
            var doc = new XmlDocument();

            var declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, "declaration", null);
            doc.AppendChild(declaration);

            var root = doc.CreateElement("articles");
            doc.AppendChild(root);

            var cItens = GenerateInt32(1, 20);
            for (var i = 0; i < cItens; i++)
            {
                var item = doc.CreateElement("article");
                root.AppendChild(item);

                var author = doc.CreateElement("author");
                author.InnerText = GenerateName(2);
                item.AppendChild(author);

                var isadmin = doc.CreateAttribute("isadmin");
                isadmin.Value = GenerateBoolean().ToString();
                author.Attributes.Append(isadmin);

                var title = doc.CreateElement("title");
                title.InnerText = GenerateName(3);
                item.AppendChild(title);

                var body = doc.CreateElement("body");
                var cdata = doc.CreateCDataSection(GenerateParagraph());
                body.AppendChild(cdata);
                item.AppendChild(body);
            }

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = new XmlTextWriter(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                return stringWriter.ToString();
            }
        }
    }
}