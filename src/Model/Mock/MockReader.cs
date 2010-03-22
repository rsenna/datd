using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using NLipsum.Core;

namespace Dataweb.Dilab.Model.Mock
{
    public class MockReader : IReader
    {
        public const int NULL_PERCENTUAL = 33;
        public const int FIND_ALL_MAX_COUNT = 100;
        public const int MAX_NAME_LENGTH = 50;

        public static readonly string NameGenerator = Lipsums.LoremIpsum;
        public static readonly string TextGenerator = Lipsums.LoremIpsum;

        private static readonly Random random = new Random();

        private int totalRecords;
        
        public ICommand Command { get; set; }

        public MockReader()
        {
            totalRecords = GenerateInt32(FIND_ALL_MAX_COUNT);
        }

        public void Dispose() {}

        private void CheckCurrentRecord()
        {
            if (totalRecords <= 0)
            {
                throw new InvalidOperationException("Não há mais registros a serem lidos.");
            }
        }
        
        public bool ReadRecord()
        {
            return --totalRecords > 0; 
        }

        public T? ReadOptional<T>(string name)
            where T : struct
        {
            CheckCurrentRecord();

            if (typeof(T).IsEnum)
            {
                return (T?) (object) Maybe<int>(GenerateEnum<T>);
            }

            if (typeof(T) == typeof(long))
            {
                return (T?) (object) Maybe<long>(GenerateInt64);
            }

            if (typeof(T) == typeof(DateTime))
            {
                return (T?) (object) Maybe<DateTime>(GenerateDateTime);
            }

            if (typeof(T) == typeof(decimal))
            {
                return (T?) (object) Maybe<decimal>(GenerateDecimal);
            }

            if (typeof(T) == typeof(bool))
            {
                return (T?) (object) Maybe<bool>(GenerateBoolean);
            }

            return (T?) (object) Maybe<int>(GenerateInt32);
        }

        public string ReadOptional(string name)
        {
            return Maybe(GeneratePhrase);
        }

        public T ReadRequired<T>(string name)
        {
            if (typeof(T).IsEnum)
            {
                return (T) (object) GenerateEnum<T>();
            }

            if (typeof(T) == typeof(long))
            {
                return (T) (object) GenerateInt64();
            }

            if (typeof(T) == typeof(DateTime))
            {
                return (T) (object) GenerateDateTime(-30, + 30);
            }

            if (typeof(T) == typeof(decimal))
            {
                return (T) (object) GenerateDecimal();
            }

            if (typeof(T) == typeof(bool))
            {
                return (T) (object) GenerateBoolean();
            }

            return (T) (object) GenerateInt32();
        }

        public string ReadRequired(string name)
        {
            return GeneratePhrase();
        }

        public static string Maybe(Func<string> func)
        {
            return Maybe(func, NULL_PERCENTUAL);
        }

        public static T? Maybe<T>(Func<T> func)
            where T : struct
        {
            return Maybe(func, NULL_PERCENTUAL);
        }

        public static string Maybe(Func<string> func, int percentual)
        {
            var dice = random.Next(0, 101);
            return dice <= percentual? func() : null;
        }

        public static T? Maybe<T>(Func<T> func, int percentual)
            where T : struct
        {
            var dice = random.Next(0, 101);
            return dice <= percentual? (T?) func() : null;
        }

        public static TResult? Maybe<T1, TResult>(Func<T1, TResult> func, T1 param1, int percentual)
            where TResult : struct
        {
            var dice = random.Next(0, 101);
            return dice <= percentual? (TResult?) func(param1) : null;
        }

        public static TResult? Maybe<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 param1, T2 param2, int percentual)
            where TResult : struct
        {
            var dice = random.Next(0, 101);
            return dice <= percentual? (TResult?) func(param1, param2) : null;
        }

        internal static int GenerateEnum<T>()
        {
            var enumType = typeof (T);
            var values = Enum.GetValues(enumType);
            var firstValue = values.GetValue(0);
            var lastValue = values.GetValue(values.GetUpperBound(0));
            var chosen = GenerateInt32(Convert.ToInt32(firstValue), Convert.ToInt32(lastValue));
            return chosen;
        }

        internal static DateTime GenerateDateTime()
        {
            return GenerateDateTime(-30, 30);
        }

        internal static DateTime GenerateDateTime(int intervaloDias)
        {
            return DateTime.Now.AddDays(GenerateInt32(intervaloDias));
        }

        internal static DateTime GenerateDateTime(int inicio, int fim)
        {
            return DateTime.Now.AddDays(GenerateInt32(inicio, fim));
        }

        internal static int GenerateInt32(int maxValue)
        {
            var rand = random.Next(0, Math.Abs(maxValue) + 1);
            return rand * Math.Sign(maxValue);
        }

        internal static int GenerateInt32(int minValue, int maxValue)
        {
            return GenerateInt32(maxValue - minValue) + minValue;
        }

        internal static int GenerateInt32()
        {
            return GenerateInt32(int.MaxValue - 1);
        }

        internal static long GenerateInt64()
        {
            var idx = random.NextDouble();
            return Convert.ToInt64(long.MaxValue * idx);
        }

        internal static decimal GenerateDecimal()
        {
            var idx = random.NextDouble();
            return decimal.MaxValue * (decimal) idx;
        }

        internal static decimal GenerateDecimal(decimal maxValue)
        {
            var idx = random.NextDouble();
            return (maxValue + 1) * (decimal)idx;
        }

        internal static decimal GenerateDecimal(decimal minValue, decimal maxValue)
        {
            return GenerateDecimal(maxValue - minValue) + minValue;
        }

        internal static decimal GenerateDecimal(decimal minValue, decimal maxValue, string format)
        {
            var result = GenerateDecimal(minValue, maxValue);
            return Convert.ToDecimal(result.ToString(format));
        }

        internal static string GeneratePhrase()
        {
            return LipsumGenerator.Generate(1, Features.Sentences, FormatStrings.Sentence.Phrase, TextGenerator);
        }

        internal static string GenerateParagraph()
        {
            return LipsumGenerator.Generate(1, Features.Paragraphs, FormatStrings.Unformatted, TextGenerator);
        }

        internal static string GenerateCode(int length)
        {
            var result = new char[length];

            for (var i = 0; i < length; i++)
            {
                var digit = (char) ('0' + (char) GenerateInt32(9));
                result[i] = (i + 1) % 4 == 0? '.' : digit;
            }

            return new string(result);
        }

        internal static Boolean GenerateBoolean()
        {
            return Convert.ToBoolean(GenerateInt32(1));
        }

        internal static string GenerateIdentifier(int words)
        {
            return LipsumGenerator.Generate(words, Features.Words, FormatStrings.Unformatted, NameGenerator).Trim().Replace(' ', '.');
        }

        internal static string GenerateEmail()
        {
            return string.Format(
                "{0}@{1}.com",
                GenerateIdentifier(2),
                GenerateIdentifier(1)
            );
        }

        internal static string GenerateName(int words)
        {
            var result = LipsumGenerator.Generate(words, Features.Words, FormatStrings.Unformatted, NameGenerator);
            return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(result);
        }

        internal static IEnumerable<string> GenerateWords(int words)
        {
            var text = LipsumGenerator.Generate(words, Features.Words, FormatStrings.Unformatted, TextGenerator);
            return text.Split(' ');
        }

        internal static string GenerateXml()
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
