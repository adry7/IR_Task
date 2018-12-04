using IR_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace IR_Task.Classes
{
    public static class ExtensionAndCommon
    {
        public static string Stem(this string str)
        {
            Stemmer stemmer = new Stemmer();
            stemmer.add(str.ToCharArray(), str.Length);
            stemmer.stem();
            str = stemmer.ToString();
            return str;
        }

        public static string RemovePunct(this string str)
        {
          //  str = Regex.Replace(str, @"\p{P}", "");
           str = Regex.Replace(str, "[^A-Za-z0-9_]+", "");
            return str;
        }

       
        /// <summary>
        /// search for item with specified term 
        /// return index if found else return -1
        /// </summary>
        /// <param name="searchModelView"></param>
        /// <param name="term"></param>
        /// <returns> index of the item</returns>
        public static int isExist(this List<SearchViewModel> searchModelView ,string term)
        {
            var check = searchModelView.Where(x => x.Term == term).SingleOrDefault();
            if (check != null)
            {
                var ind = searchModelView.IndexOf(check);
                return ind;
            }
            return -1;
        }


        /// <summary>
        /// Takes list of terms and return dictionary of terms per each soundex
        /// </summary>
        /// <param name="invIndex"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> build_soundex(string [] terms)
        {
            var tempIndex = terms;
            Dictionary<string, List<string>> Dic = new Dictionary<string, List<string>>();
            foreach (var item in tempIndex)
            {
                int value;
                string Soundex = "";
                if (!(int.TryParse(item, out value)))
                {

                    Soundex = item.Substring(0, 1);
                    for (int i = 1; i < item.Length; i++)
                    {


                        if (item[i] == 'a' || item[i] == 'e' || item[i] == 'i' || item[i] == 'o' || item[i] == 'u' || item[i] == 'h' || item[i] == 'w' || item[i] == 'y')
                        {
                            Soundex += "0";
                        }
                        else if (item[i] == 'b' || item[i] == 'p' || item[i] == 'f' || item[i] == 'v')
                        {
                            Soundex += "1";
                        }
                        else if (item[i] == 'c' || item[i] == 'j' || item[i] == 'g' || item[i] == 'k' || item[i] == 'q' || item[i] == 's' || item[i] == 'x' || item[i] == 'z')
                        {
                            Soundex += "2";
                        }
                        else if (item[i] == 'd' || item[i] == 't')
                        {
                            Soundex += "3";
                        }
                        else if (item[i] == 'l')
                        {
                            Soundex += "4";
                        }
                        else if (item[i] == 'm' || item[i] == 'n')
                        {
                            Soundex += "5";
                        }
                        else if (item[i] == 'r')
                        {
                            Soundex += "6";
                        }

                    }


                    // remove one out of each pair of consecutive identical digits.
                    string tmp = Soundex;
                    for (int i = 2; i < tmp.Length; i++)
                    {
                        if (tmp[i] == tmp[i - 1])
                        {
                            var ind = Soundex.IndexOf(tmp[i]);
                            Soundex = Soundex.Remove(ind, 1);
                        }
                    }

                    // remove zeros
                    if (Soundex.Contains("0"))
                        Soundex = Soundex.Replace("0", "");
                    // 4 characters
                    if (Soundex.Length > 4)
                        Soundex = Soundex.Substring(0, 4);
                    else if (Soundex.Length < 4)
                    {
                        for (int i = Soundex.Length; i < 4; i++)
                        {
                            Soundex += "0";
                        }
                    }

                    if (!Dic.ContainsKey(Soundex))
                        Dic.Add(Soundex, new List<string>() { item });
                    else
                        Dic[Soundex].Add(item);

                }

            }

            return Dic;
            
        }

        /// <summary>
        /// Takes list of invIndex and return dictionary of terms per each soundex
        /// </summary>
        /// <param name="invIndex"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> build_soundex(List<TmpInvIndex> invIndexList)
        {
            var tempIndex = invIndexList;
            Dictionary<string, List<string>> Dic = new Dictionary<string, List<string>>();
            foreach (var item in tempIndex)
            {
                int value;
                string Soundex = "";
                if (!(int.TryParse(item.Term, out value)))
                {

                    Soundex = item.Term.Substring(0, 1);
                    for (int i = 1; i < item.Term.Length; i++)
                    {


                        if (item.Term[i] == 'a' || item.Term[i] == 'e' || item.Term[i] == 'i' || item.Term[i] == 'o' || item.Term[i] == 'u' || item.Term[i] == 'h' || item.Term[i] == 'w' || item.Term[i] == 'y')
                        {
                            Soundex += "0";
                        }
                        else if (item.Term[i] == 'b' || item.Term[i] == 'p' || item.Term[i] == 'f' || item.Term[i] == 'v')
                        {
                            Soundex += "1";
                        }
                        else if (item.Term[i] == 'c' || item.Term[i] == 'j' || item.Term[i] == 'g' || item.Term[i] == 'k' || item.Term[i] == 'q' || item.Term[i] == 's' || item.Term[i] == 'x' || item.Term[i] == 'z')
                        {
                            Soundex += "2";
                        }
                        else if (item.Term[i] == 'd' || item.Term[i] == 't')
                        {
                            Soundex += "3";
                        }
                        else if (item.Term[i] == 'l')
                        {
                            Soundex += "4";
                        }
                        else if (item.Term[i] == 'm' || item.Term[i] == 'n')
                        {
                            Soundex += "5";
                        }
                        else if (item.Term[i] == 'r')
                        {
                            Soundex += "6";
                        }

                    }


                    // remove one out of each pair of consecutive identical digits.
                    string tmp = Soundex;
                    for (int i = 2; i < tmp.Length; i++)
                    {
                        if (tmp[i] == tmp[i - 1])
                        {
                            var ind = Soundex.IndexOf(tmp[i]);
                            Soundex = Soundex.Remove(ind, 1);
                        }
                    }

                    // remove zeros
                    if (Soundex.Contains("0"))
                        Soundex = Soundex.Replace("0", "");
                    // 4 characters
                    if (Soundex.Length > 4)
                        Soundex = Soundex.Substring(0, 4);
                    else if (Soundex.Length < 4)
                    {
                        for (int i = Soundex.Length; i < 4; i++)
                        {
                            Soundex += "0";
                        }
                    }

                    if (!Dic.ContainsKey(Soundex))
                        Dic.Add(Soundex, new List<string>() { item.Term });
                    else
                        Dic[Soundex].Add(item.Term);

                }

            }

            return Dic;

        }

        /// <summary>
        /// Takes list of terms and return dictionary of bigrams per each term
        /// </summary>
        /// <param name="Terms"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> build_Bigram(string[] Terms)
        {
            Dictionary<string, List<string>> Dic = new Dictionary<string, List<string>>();
            foreach (var item in Terms)
            {
                if (!Dic.ContainsKey(item))
                {
                    int value;
                    // Check if it is not Number
                    if (!(int.TryParse(item, out value)))
                    {
                        string gram = "";
                        gram = '$' + item.Substring(0, 1);
                        Dic.Add(item, new List<string>() { gram});
                        for (int x1 = 0; x1 < item.Length; x1++)
                        {
                            if (x1 == (item.Length - 1))
                            {
                                gram = item[x1] + "$";
                                Dic[item].Add(gram);
                                break;
                            }

                            gram = item[x1] + item[x1 + 1].ToString();
                            Dic[item].Add(gram);
                           
                        }
                    }
                }
            }
            return Dic;
     
          
           

        }



        /// <summary>
        /// Takes list of inIndex and return dictionary of bigrams per each term
        /// </summary>
        /// <param name="Terms"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> build_Bigram(List<TmpInvIndex> invIndexList)
        {
            Dictionary<string, List<string>> Dic = new Dictionary<string, List<string>>();
            foreach (var item in invIndexList)
            {
                if (!Dic.ContainsKey(item.Term))
                {
                    int value;
                    // Check if it is not Number
                    if (!(int.TryParse(item.Term, out value)))
                    {
                        string gram = "";
                        gram = '$' + item.Term.Substring(0, 1);
                        Dic.Add(item.Term, new List<string>() { gram });

                        for (int i = 0; i < item.Term.Length; i++)
                        {
                            if (i == (item.Term.Length - 1))
                            {
                                gram = item.Term[i] + "$";
                                Dic[item.Term].Add(gram);
                                break;
                            }

                            gram = item.Term[i] + item.Term[i + 1].ToString();
                            Dic[item.Term].Add(gram);

                        }
                    }
                }
            }
            return Dic;




        }
    }
}