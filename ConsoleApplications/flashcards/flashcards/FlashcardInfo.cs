using System;

namespace flashcards
{
    internal class FlashcardInfo
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _front;
        public string Front
        {
            get { return _front; }
            set { _front = value; }
        }

        private string _back;
        public string Back
        {
            get { return _back; }
            set { _back = value; }
        }

        private int _stackKey;
        public int StackKey
        {
            get { return _stackKey; }
            set
            {
                _stackKey = value;
            }
        }

        public FlashcardInfo() 
        { 
        }
    }
}
