using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traductor
{
    class Translate
    {
        private SortedList words;
        public Translate()
        {
            words = new SortedList();
        }
        public void addWord(string word, string translation)
        {
            words.Add(word, translation);
        }
        public string search(string word)
        {
            if (words.Contains(word))
            {
                return words[word].ToString();
            }
            else
            {
                return "";
            }
        }
        public void removeWord(string word)
        {
            words.Remove(word);
        }
        public void removeTranslation(string translation)
        {
            int pos = words.IndexOfValue(translation);
            if (pos >= 0)
            {
                words.RemoveAt(pos);
            }
        }
        public string firstWord()
        {
            return words.GetByIndex(0).ToString();
        }

    }
}
