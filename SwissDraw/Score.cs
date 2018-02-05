using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissDraw
{
    public class Score
    {
        // くじの番号
        public int LotNumber { get; set; }
        public int winCount { get; set; }
        public int score { get; set; }

        public static Dictionary<int, Score> CalcWinClont(Match[] result)
        {
            Dictionary<int, Score> kekka = new Dictionary<int, Score>();

            Score s = new Score();

            for (int i = 1; i <= result.Length * 2; i++)
            {
                s = new Score();

                s.LotNumber = i;

                s.winCount = Match.getWinCount(i, result);

                s.score = 0;

                kekka.Add(i, s);
            }

            return kekka;
        }

        public static Dictionary<int, Score> CalcScore(Match[] result)
        {
            Dictionary<int, Score> kekka = CalcWinClont(result);

            for (int i = 1; i <= kekka.Count; i++)
            {
                int count = 0;

                for (int j = 0; j < result.Length; j++)
                { 

                    if (result[j].Person1 == i && result[j].Result == 1)
                    {
                        int aite = result[j].Person2;
                        int aitesyouri = kekka[aite].winCount;
                        count += aitesyouri;
                    }
                    else if (result[j].Person2 == i && result[j].Result == 2)
                    {
                        int aite = result[j].Person1;
                        int aitesyouri = kekka[aite].winCount;
                        count += aitesyouri;
                    }
                    else
                    {

                    }
                }

                kekka[i].score = count;

            }

            return kekka;
        }

    }
}