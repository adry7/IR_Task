using IR_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace IR_Task.Classes
{
    public class PreProcess
    {
        private IRdbEntities db = new IRdbEntities();
        
      //   string pattern = "[^A-Za-z0-9_]+";

        // @"\p{P}"


        private List<string> stopWords = new List<string>()
            {
"a",
"able",
"about",
"above",
"according",
"accordingly",
"across",
"actually",
"after",
"afterwards",
"again",
"against",
"ain't",
"all",
"allow",
"allows",
"almost",
"alone",
"along",
"already",
"also",
"although",
"always",
"am",
"among",
"amongst",
"an",
"and",
"another",
"any",
"anybody",
"anyhow",
"anyone",
"anything",
"anyway",
"anyways",
"anywhere",
"apart",
"appear",
"appreciate",
"appropriate",
"are",
"aren't",
"around",
"as",
"a's",
"aside",
"ask",
"asking",
"associated",
"at",
"available",
"away",
"awfully",
"be",
"became",
"because",
"become",
"becomes",
"becoming",
"been",
"before",
"beforehand",
"behind",
"being",
"believe",
"below",
"beside",
"besides",
"best",
"better",
"between",
"beyond",
"both",
"brief",
"but",
"by",
"came",
"can",
"cannot",
"cant",
"can't",
"cause",
"causes",
"certain",
"certainly",
"changes",
"clearly",
"c'mon",
"co",
"com",
"come",
"comes",
"concerning",
"consequently",
"consider",
"considering",
"contain",
"containing",
"contains",
"corresponding",
"could",
"couldn't",
"course",
"c's",
"currently",
"definitely",
"described",
"despite",
"did",
"didn't",
"different",
"do",
"does",
"doesn't",
"doing",
"done",
"don't",
"down",
"downwards",
"during",
"each",
"edu",
"eg",
"eight",
"either",
"else",
"elsewhere",
"enough",
"entirely",
"especially",
"et",
"etc",
"even",
"ever",
"every",
"everybody",
"everyone",
"everything",
"everywhere",
"ex",
"exactly",
"example",
"except",
"far",
"few",
"fifth",
"first",
"five",
"followed",
"following",
"follows",
"for",
"former",
"formerly",
"forth",
"four",
"from",
"further",
"furthermore",
"get",
"gets",
"getting",
"given",
"gives",
"go",
"goes",
"going",
"gone",
"got",
"gotten",
"greetings",
"had",
"hadn't",
"happens",
"hardly",
"has",
"hasn't",
"have",
"haven't",
"having",
"he",
"he'd",
"he'll",
"hello",
"help",
"hence",
"her",
"here",
"hereafter",
"hereby",
"herein",
"here's",
"hereupon",
"hers",
"herself",
"he's",
"hi",
"him",
"himself",
"his",
"hither",
"hopefully",
"how",
"howbeit",
"however",
"how's",
"i",
"i'd",
"ie",
"if",
"ignored",
"i'll",
"i'm",
"immediate",
"in",
"inasmuch",
"inc",
"indeed",
"indicate",
"indicated",
"indicates",
"inner",
"insofar",
"instead",
"into",
"inward",
"is",
"isn't",
"it",
"it'd",
"it'll",
"its",
"it's",
"itself",
"i've",
"just",
"keep",
"keeps",
"kept",
"know",
"known",
"knows",
"last",
"lately",
"later",
"latter",
"latterly",
"least",
"less",
"lest",
"let",
"let's",
"like",
"liked",
"likely",
"little",
"look",
"looking",
"looks",
"ltd",
"mainly",
"many",
"may",
"maybe",
"me",
"mean",
"meanwhile",
"merely",
"might",
"more",
"moreover",
"most",
"mostly",
"much",
"must",
"mustn't",
"my",
"myself",
"name",
"namely",
"nd",
"near",
"nearly",
"necessary",
"need",
"needs",
"neither",
"never",
"nevertheless",
"new",
"next",
"nine",
"no",
"nobody",
"non",
"none",
"noone",
"nor",
"normally",
"not",
"nothing",
"novel",
"now",
"nowhere",
"obviously",
"of",
"off",
"often",
"oh",
"ok",
"okay",
"old",
"on",
"once",
"one",
"ones",
"only",
"onto",
"or",
"other",
"others",
"otherwise",
"ought",
"our",
"ours",
"ourselves",
"out",
"outside",
"over",
"overall",
"own",
"particular",
"particularly",
"per",
"perhaps",
"placed",
"please",
"plus",
"possible",
"presumably",
"probably",
"provides",
"que",
"quite",
"qv",
"rather",
"rd",
"re",
"really",
"reasonably",
"regarding",
"regardless",
"regards",
"relatively",
"respectively",
"right",
"said",
"same",
"saw",
"say",
"saying",
"says",
"second",
"secondly",
"see",
"seeing",
"seem",
"seemed",
"seeming",
"seems",
"seen",
"self",
"selves",
"sensible",
"sent",
"serious",
"seriously",
"seven",
"several",
"shall",
"shan't",
"she",
"she'd",
"she'll",
"she's",
"should",
"shouldn't",
"since",
"six",
"so",
"some",
"somebody",
"somehow",
"someone",
"something",
"sometime",
"sometimes",
"somewhat",
"somewhere",
"soon",
"sorry",
"specified",
"specify",
"specifying",
"still",
"sub",
"such",
"sup",
"sure",
"take",
"taken",
"tell",
"tends",
"th",
"than",
"thank",
"thanks",
"thanx",
"that",
"thats",
"that's",
"the",
"their",
"theirs",
"them",
"themselves",
"then",
"thence",
"there",
"thereafter",
"thereby",
"therefore",
"therein",
"theres",
"there's",
"thereupon",
"these",
"they",
"they'd",
"they'll",
"they're",
"they've",
"think",
"third",
"this",
"thorough",
"thoroughly",
"those",
"though",
"three",
"through",
"throughout",
"thru",
"thus",
"to",
"together",
"too",
"took",
"toward",
"towards",
"tried",
"tries",
"truly",
"try",
"trying",
"t's",
"twice",
"two",
"un",
"under",
"unfortunately",
"unless",
"unlikely",
"until",
"unto",
"up",
"upon",
"us",
"use",
"used",
"useful",
"uses",
"using",
"usually",
"value",
"various",
"very",
"via",
"viz",
"vs",
"want",
"wants",
"was",
"wasn't",
"way",
"we",
"we'd",
"welcome",
"well",
"we'll",
"went",
"were",
"we're",
"weren't",
"we've",
"what",
"whatever",
"what's",
"when",
"whence",
"whenever",
"when's",
"where",
"whereafter",
"whereas",
"whereby",
"wherein",
"where's",
"whereupon",
"wherever",
"whether",
"which",
"while",
"whither",
"who",
"whoever",
"whole",
"whom",
"who's",
"whose",
"why",
"why's",
"will",
"willing",
"wish",
"with",
"within",
"without",
"wonder",
"won't",
"would",
"wouldn't",
"yes",
"yet",
"you",
"you'd",
"you'll",
"your",
"you're",
"yours",
"yourself",
"yourselves",
"you've",
"zero"
            };
       
        ///  Build Database .
        public void StartIndexing()
        {

            List<inverted_index> dbTerm = new List<inverted_index>();

            List<TmpInvIndex> invIndexList = new List<TmpInvIndex>();
            List<TmpInvIndex> TempinvIndexList = new List<TmpInvIndex>();

            var allDocs = db.AllPages.Where(x => x.id <= 1600).ToList();

            foreach (var doc in allDocs)
            {
                var Tempterms = doc.mycontent.Split(' ', ',');
                //var Tempterm = Regex.Split(row, pattern);

                for (int i = 0; i < Tempterms.Length; i++)
                {
                    Tempterms[i] = Tempterms[i].RemovePunct();

                    if (!String.IsNullOrWhiteSpace(Tempterms[i]))
                    {
                        if (!stopWords.Contains(Tempterms[i]))
                        {

                            // Stemmer stemmer = new Stemmer();
                            string termAfterStem = Tempterms[i].ToLower();
                            termAfterStem =  termAfterStem.Stem();
                            //stemmer.add(termAfterStem.ToCharArray(), termAfterStem.Length);
                            //stemmer.stem();
                            //termAfterStem = stemmer.ToString();


                            
                            var check = invIndexList.Where(x => x.Term == termAfterStem).SingleOrDefault();
                            if (check != null)
                            {
                                var ind = invIndexList.IndexOf(check);
                                invIndexList[ind].Update(doc.id, i + 1);

                            }
                            else
                            {
                                invIndexList.Add(new TmpInvIndex(termAfterStem, doc.id, i + 1));
                            }

                            check = TempinvIndexList.Where(x => x.Term == Tempterms[i].ToLower()).SingleOrDefault();
                            if (check != null)
                            {
                                var ind = TempinvIndexList.IndexOf(check);
                                TempinvIndexList[ind].Update(doc.id, i + 1);

                            }
                            else
                            {
                                TempinvIndexList.Add(new TmpInvIndex(Tempterms[i].ToLower(), doc.id, i + 1));
                            }



                        }


                    }
                }


            }


            build_Bigram(TempinvIndexList);
            build_soundex(TempinvIndexList);

            for (int i = 0; i < invIndexList.Count; i++)
            {
                dbTerm.Add(new inverted_index());

                dbTerm[i].Term = invIndexList[i].Term;

                for (int docind = 0; docind < invIndexList[i].docidPos.Count; docind++)
                {
                    if (docind == 0) // ,
                    {
                        dbTerm[i].Docid += invIndexList[i].docidPos[0].Key;
                        dbTerm[i].Frequency += invIndexList[i].docidPos[0].Value.Count;
                    }
                    else
                    {
                        dbTerm[i].Docid += "," + invIndexList[i].docidPos[docind].Key;
                        dbTerm[i].Frequency += "," + invIndexList[i].docidPos[docind].Value.Count;
                    }

                    dbTerm[i].Positions += invIndexList[i].docidPos[docind].Value[0];

                    for (int posind = 1; posind < invIndexList[i].docidPos[docind].Value.Count; posind++)
                    {
                        dbTerm[i].Positions += "," + invIndexList[i].docidPos[docind].Value[posind];
                    }
                    if (docind != invIndexList[i].docidPos.Count - 1)
                        dbTerm[i].Positions += "@";
                }
            }


            db.inverted_index.AddRange(dbTerm);
            db.SaveChanges();
            
        }

        private void build_Bigram(List<TmpInvIndex> invIndex) // without stemming
        {
            Dictionary<string, List<string>> Dic = new Dictionary<string, List<string>>();
            List<bigram_index> dbBigram = new List<bigram_index>();

            Dic = ExtensionAndCommon.build_Bigram(invIndex);
           
            // 3shan elkey bta3 eldb hwa elgram f b3ks eldic 
            Dictionary<string, List<string>> Dic2 = new Dictionary<string, List<string>>();

            foreach (var item in Dic)
            {
                foreach (var gram in item.Value)
                {
                    if (!Dic2.ContainsKey(gram))
                    {
                        Dic2.Add(gram, new List<string>() { item.Key });
                    }
                    else
                    {
                        Dic2[gram].Add(item.Key);
                    }
                }
            }
            int count = 0;
            foreach (var item in Dic2)
            {
                dbBigram.Add(new bigram_index());
                dbBigram[count].K_gram = item.Key;
                dbBigram[count].Terms = item.Value[0];
                for (int i = 1; i < item.Value.Count; i++)
                {
                    dbBigram[count].Terms += "," + item.Value[i];
                }
                count++;
            }

            db.bigram_index.AddRange(dbBigram);
           

        }

        private void build_soundex(List<TmpInvIndex> invIndex)
        {
           
            Dictionary<string, List<string>> Dic = new Dictionary<string, List<string>>();
            List<soundex_index> dbSoundex = new List<soundex_index>();
    

            Dic = ExtensionAndCommon.build_soundex(invIndex);

            int count = 0;
            foreach (var item in Dic)
            {
                dbSoundex.Add(new soundex_index());
                dbSoundex[count].soundex = item.Key;
                dbSoundex[count].Terms = item.Value[0];
                for (int i = 1; i < item.Value.Count; i++)
                {
                    dbSoundex[count].Terms += "," + item.Value[i];
                }
                count++;
            }

            db.soundex_index.AddRange(dbSoundex);
        }
    }
}
