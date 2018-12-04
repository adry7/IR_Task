using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IR_Task.Models
{
    public class SearchViewModel
    {
        public string FullContent { get; set; }

        public string Url { get; set; }

        public string Term { get; set; }

        public string MinUrl { get; private set; }

        private void SetMinUrl()
        {
            int maxLength = Math.Min(Url.Length, 35);
            MinUrl = Url.Substring(0, maxLength);
        }

        public string MinContent { get; private set; }

        private void SetMinContent()
        {
            int maxLenght = Math.Min(FullContent.Length, 250);
            MinContent = FullContent.Substring(0, maxLenght);
        }

        public SearchViewModel(string content, string url )
        {
            FullContent = content;
            SetMinContent();
            Url = url;
            SetMinUrl();
           
        }
     
  
    }
}