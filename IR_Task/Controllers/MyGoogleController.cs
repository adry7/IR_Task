using IR_Task.Classes;
using IR_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IR_Task.Controllers
{
    public class MyGoogleController : Controller
    {
        IRdbEntities db = new IRdbEntities();
        List<SearchViewModel> SearchResult = new List<SearchViewModel>();
        List<TmpInvIndex> InvIndexList = new List<TmpInvIndex>();

        // GET: MyGoogle
        public ActionResult Index()
        {
            ViewBag.soundex = "";
            ViewBag.Bigram = "";
            ViewBag.ResCount = 0;
            ViewBag.SearchTxt = "";

            return View(SearchResult);
        }

        [HttpPost]
        public ActionResult Search(string SearchTxt, string SearchType)
        {
            ViewBag.soundex = "";
            ViewBag.Bigram = "";
            ViewBag.ResCount = 0;
            ViewBag.SearchTxt = SearchTxt;

            if (!string.IsNullOrWhiteSpace(SearchTxt))
            {


                string TmpSearchTxt = SearchTxt.ToLower();
                TmpSearchTxt = TmpSearchTxt.Trim('"');

                TmpSearchTxt = TmpSearchTxt.Trim();
                var terms = TmpSearchTxt.Split(' ', ',');

                for (int i = 0; i < terms.Length; i++)
                {
                    terms[i] = terms[i].RemovePunct();
                    terms[i] = terms[i].ToLower();

                    string tmpTerm = terms[i].Stem();


                    var DbTerm = db.inverted_index.Where(x => x.Term == tmpTerm).SingleOrDefault(); //position term 
                    if (DbTerm != null)
                    {
                        InvIndexList.Add(new TmpInvIndex(terms[i], DbTerm.Docid, DbTerm.Positions));
                    }
                }

                List<int> SharedDocIds = new List<int>();

                if (InvIndexList.Count > 0)
                {
                    SharedDocIds = InvIndexList[0].DocidPosDic.Keys.ToList();
                    for (int i = 1; i < InvIndexList.Count; i++)
                    {
                        SharedDocIds = InvIndexList[i].DocidPosDic.Keys.Intersect(SharedDocIds).ToList();
                    }
                }

                if (SearchTxt[0] == '"' && SearchTxt[SearchTxt.Length - 1] == '"')
                {

                    var resultDocIds = ExactSearch(InvIndexList, SharedDocIds);//if exact

                    foreach (var item in resultDocIds)
                    {
                        var tmp = db.AllPages.Where(x => x.id == item).Single();
                        SearchResult.Add(new SearchViewModel(tmp.mycontent, tmp.linkUrl));
                    }
                }
                else
                {

                    foreach (var item in SharedDocIds)
                    {
                        var tmp = db.AllPages.Where(x => x.id == item).Single();
                        SearchResult.Add(new SearchViewModel(tmp.mycontent, tmp.linkUrl));
                    }
                    if (SearchType == "Phonetic")
                    {
                        var SoundexDic = ExtensionAndCommon.build_soundex(terms);
                        Dictionary<string, List<string>> ds = new Dictionary<string, List<string>>();
                        foreach (var item in SoundexDic)
                        {
                            var tmp = db.soundex_index.Where(x => x.soundex == item.Key).SingleOrDefault();
                            ds.Add(item.Value[0], tmp.Terms.Split(',').ToList());
                        }
                        ViewBag.soundex = ds;

                    }
                    else if (SearchType == "Bigram")
                    {
                        var queryTermAndBigramsDic = ExtensionAndCommon.build_Bigram(terms);
                        List<BigramViewModel> BigramViewModelList = new List<BigramViewModel>();
                        foreach (var item in queryTermAndBigramsDic)
                        {
                            BigramViewModelList.Add(new BigramViewModel(item.Key, item.Value, new Dictionary<string, List<string>>()));


                            foreach (var bigram in item.Value)
                            {
                                var gramTerms = db.bigram_index.Where(x => x.K_gram == bigram).Select(x => x.Terms).Single().Split(',');


                                var ind = BigramViewModelList.IndexOf(BigramViewModelList.Where(x => x.Term == item.Key).Single());

                                var res = ExtensionAndCommon.build_Bigram(gramTerms);

                                foreach (var item4 in res)
                                {
                                    if (!BigramViewModelList[ind].dbTermAndBigrams.ContainsKey(item4.Key))
                                        BigramViewModelList[ind].dbTermAndBigrams.Add(item4.Key, item4.Value);
                                }

                            }
                        }

                        foreach (var item in BigramViewModelList)
                        {
                            item.CountCommonBigrams();
                        }
                        ViewBag.Bigram = BigramViewModelList;
                    }
                }

            }

            if (SearchResult.Count > 0)
            {
                ViewBag.ResCount = SearchResult.Count;
                return View("Index", SearchResult);
            }
            else
                return View("Index", new List<SearchViewModel>() { new SearchViewModel("NoResult", "OPS ... Try Again") });


        }


        private bool RecExactSearch(List<int> listOfposResult, List<TmpInvIndex> AllTerms, int TermInd, int DocKey)
        {
            if (AllTerms.Count == listOfposResult.Count)
                return true;
            if (AllTerms.Count == TermInd)
                return false;

            int PostionsCount = AllTerms[TermInd].DocidPosDic[DocKey].Count;
            for (int k = 0; k < PostionsCount; k++)
            {
                bool r = false;
                if (isInSequence(listOfposResult, AllTerms[TermInd].DocidPosDic[DocKey][k]))
                {
                    listOfposResult.Add(AllTerms[TermInd].DocidPosDic[DocKey][k]);
                    TermInd++;
                    r = RecExactSearch(listOfposResult, AllTerms, TermInd, DocKey);
                    if (!r)
                    {
                        listOfposResult.Remove(listOfposResult.Last());
                        TermInd--;
                    }
                    else
                        return true;
                }

            }
            return false;

        }

        private bool isInSequence(List<int> listOfpos, int newPos)
        {
            if (listOfpos.Count >= 1)
            {
                if (listOfpos.Last() == (newPos - 1))
                    return true;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Searchs for exact sequence , 
        /// returns all shared documents id
        /// </summary>
        /// <param name="AllTerms"></param>
        /// <param name="SharedDocKeys"></param>
        /// <returns></returns>
        public List<int> ExactSearch(List<TmpInvIndex> AllTerms, List<int> SharedDocKeys)
        {

            List<int> DocKeys = new List<int>();
            foreach (var item in SharedDocKeys)
            {

                int termind = 0;
                List<int> listOfposResult = new List<int>();
                if (RecExactSearch(listOfposResult, AllTerms, termind, item))
                {
                    DocKeys.Add(item);
                }
            }
            return DocKeys;
        }




    }
}