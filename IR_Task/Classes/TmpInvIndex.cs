using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IR_Task.Classes
{
    public class TmpInvIndex
    {

        public string Term { get; set; }

        #region MileStone2


        public List<KeyValuePair<int, List<int>>> docidPos { get; set; }


        /// <summary>
        /// intialize single doc and single pos
        /// </summary>
        /// <param name="term"></param>
        /// <param name="docid"></param>
        /// <param name="pos"></param>
        public TmpInvIndex(string term, int docid, int pos)
        {

            Term = term;
            docidPos = new List<KeyValuePair<int, List<int>>>()
            { new KeyValuePair<int, List<int>>(docid,new List<int>() { pos } )};
        }

        /// <summary>
        /// Updates an exsiting Term 
        /// </summary>
        /// <param name="docid"> Document id </param>
        /// <param name="newPos">new postion to insert</param>
        public void Update(int docid, int newPos)
        {
            var check = docidPos.Where(x => x.Key == docid).Select(x => x.Value.ToString()).SingleOrDefault();
            if (check != null)
            {
              
                var ind = this.docidPos.IndexOf(this.docidPos.Where(x => x.Key == docid).SingleOrDefault());
                List<int> list = this.docidPos[ind].Value;
                list.Add(newPos);
                KeyValuePair<int, List<int>> tmp2 = new KeyValuePair<int, List<int>>(this.docidPos[ind].Key, list);
                this.docidPos[ind] = tmp2;
            }
            else
                addNewDocid(docid, newPos);

        }

        /// <summary>
        /// add new document to an existing term
        /// </summary>
        /// <param name="docid"></param>
        /// <param name="pos"></param>
        private void addNewDocid(int docid, int pos)
        {
            this.docidPos.Add(new KeyValuePair<int, List<int>>(docid, new List<int>() { pos }));
        }

        #endregion



        public Dictionary<int, List<int>> DocidPosDic { get; set; }

        /// <summary>
        /// intialize list of docs and postions
        /// </summary>
        /// <param name="term"></param>
        /// <param name="DocIds"></param>
        /// <param name="Postions"></param>
        public TmpInvIndex(string term,string DocIds,string Postions)
        {
            Term = term;
            DocidPosDic = new Dictionary<int, List<int>>();
            var doc = DocIds.Split(',');
            var pos = Postions.Split('@');
            
            for (int i = 0; i < doc.Length; i++)
            {
                int docid = int.Parse(doc[i]);
                               
                var posForOneDoc = pos[i].Split(',');

                List<int> positions = new List<int>();
                for (int j = 0; j < posForOneDoc.Length; j++)
                {
                    positions.Add( int.Parse(posForOneDoc[j]));
                   
                }
                DocidPosDic.Add(docid, positions);
            }
        }
  }

}
