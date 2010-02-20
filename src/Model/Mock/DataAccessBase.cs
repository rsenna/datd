using System;
using NLipsum.Core;

namespace Dataweb.Dilab.Model.Mock
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
        where T: DataTransferBase
    {
        private const int FIND_ALL_MAX_COUNT = 100;

        public abstract T FetchDto();

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

        protected static DateTime GenerateDateTime(int intervaloDias)
        {
            return DateTime.Now.AddDays(intervaloDias);
        }

        protected static int GenerateInt32(int maxValue)
        {
            var random = new Random();
            return random.Next(0, maxValue + 1);
        }

        protected static int GenerateInt32()
        {
            return GenerateInt32(int.MaxValue - 1);
        }

        protected static decimal GenerateDecimal()
        {
            var random = new Random();
            var idx = random.NextDouble();

            return decimal.MaxValue * (decimal)idx;
        }

        protected static string GenerateText(int length)
        {
            return LipsumGenerator.Generate(length);
        }

        protected static string GenerateCode(int length)
        {
            var result = new char[length];

            for (var i = 0; i < length; i++)
            {
                var digit = (char)('0' + (char) GenerateInt32(9));
                result[i] = i + 1 % 3 == 0? '.' : digit;
            }

            return result.ToString();
        }

        protected static Boolean GenerateBoolean()
        {
            return Convert.ToBoolean(GenerateInt32(1));
        }
    }
}