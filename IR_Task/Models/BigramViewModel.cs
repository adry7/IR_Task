using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IR_Task.Models
{
    public class BigramViewModel
    {
        private const double threshold = 0.45;

        public string Term { get; set; }
        public List<string> Bigrams { get; set; }

        public Dictionary<string,List<string>> dbTermAndBigrams { get; set; }

        public List<string> TopTerms { get; set; }



        public BigramViewModel(string term, List<string> bigrams, Dictionary<string, List<string>> dbTermAndBigrams)
        {
            TopTerms = new List<string>();
            Term = term;
            Bigrams = bigrams;
            this.dbTermAndBigrams = dbTermAndBigrams;
        }

        /// <summary>
        ///  jaccardCoefficient
        /// </summary>
        /// <returns></returns>
        public void CountCommonBigrams()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int i = 0; i < dbTermAndBigrams.Count; i++)
            {
                
                var tmpTerm = dbTermAndBigrams.ElementAt(i);
                if (tmpTerm.Key != Term)
                {
                    double s = Bigrams.Intersect(tmpTerm.Value).Count() * 2;
                    double JaccardCoefficient = s / (Bigrams.Count + tmpTerm.Value.Count);
                    if (JaccardCoefficient >= threshold)
                        dic.Add(tmpTerm.Key, CalcLevenshteinDistance(tmpTerm.Key, Term));
                }
            }

            TopTerms = dic.OrderBy(x => x.Value).Take(10).Select(x => x.Key).ToList() ;
          //  return dic;
        }

        private  int CalcLevenshteinDistance(string a, string b)
        {
            if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b)) return 0;

            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return distances[lengthA, lengthB];
        }

    }
}