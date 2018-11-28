using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaitlynTextStats
{
    class Word
    {
        public String Text
        {
            get;
            set;
        }

        public int Frequency
        {
            get;
            set;
        }


        public Word(String text)
        {
            Text = text;
            Frequency = 1;

        }

        public override string ToString()
        {
            return Text + "," + Frequency;
        }
    }
}
