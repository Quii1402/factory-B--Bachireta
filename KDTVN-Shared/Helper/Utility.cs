using AutoMapper;
using KDTVN_Shared.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KDTVN_Shared.Helper
{
    public static class Utility
    {
        public static Account Employee2Account(Employee emp)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, Account>();
            });

            var mapper = configuration.CreateMapper();
            return mapper.Map<Account>(emp);
        }

        public static String SHA256Hash(String value)
        {
            string encrypted = Convert.ToBase64String(new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(value)));
            return encrypted;
        }

        public static double StdDev(this IEnumerable<double> values, bool as_sample)
        {
            // Get the mean.
            double mean = values.Sum() / values.Count();

            // Get the sum of the squares of the differences
            // between the values and the mean.
            var squares_query =
                from double value in values
                select (value - mean) * (value - mean);
            double sum_of_squares = squares_query.Sum();

            if (as_sample)
            {
                return Math.Sqrt(sum_of_squares / (values.Count() - 1));
            }
            else
            {
                return Math.Sqrt(sum_of_squares / values.Count());
            }
        }

    }
}
