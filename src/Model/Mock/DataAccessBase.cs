#region

using System;
using System.Globalization;
using NLipsum.Core;

#endregion

namespace Dataweb.Dilab.Model.Mock
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
        where T : DataTransferBase
    {
        public const int FIND_ALL_MAX_COUNT = 100;
        public const int MAX_NAME_LENGTH = 50;

        public static readonly string NameGenerator = Lipsums.LoremIpsum;
        public static readonly string TextGenerator = Lipsums.LoremIpsum;

        private static readonly Random random = new Random();

        public virtual T FindByPrimaryKey(object pk)
        {
            return FetchDto();
        }

        public virtual T Insert(T dto)
        {
            return dto;
        }

        public virtual T Update(T dto)
        {
            return dto;
        }

        public virtual T[] FindAll()
        {
            var count = GenerateInt32(FIND_ALL_MAX_COUNT);
            var result = new T[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = FetchDto();
            }

            return result;
        }

        public abstract T FetchDto();

        protected static DateTime GenerateDateTime(int intervaloDias)
        {
            return DateTime.Now.AddDays(GenerateInt32(intervaloDias));
        }

        protected static int GenerateInt32(int maxValue)
        {
            var rand = random.Next(0, Math.Abs(maxValue) + 1);
            return rand * Math.Sign(maxValue);
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
    }
}