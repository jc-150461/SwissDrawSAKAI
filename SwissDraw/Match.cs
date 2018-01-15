using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissDraw
{
    public class Match
    {
        private int person1;
        private int person2;
        // result 1:Person1の勝ち　2:Person2の勝ち　0:決着がついていない　他:エラー
        private int result;

        public int Person1 { get => person1; set => person1 = value; }
        public int Person2 { get => person2; set => person2 = value; }
        public int Result { get => result; set => result = value; }

        public Match(int i,int j)
        {
            person1 = i;
            person2 = j;
            result = 0;
        }

        public static Match[] MakeMatch(Dictionary<int, Person> persons, Match[] results)
        {
            // personsのkeyのみ取り出す
            int[] keys = GetKeyArray(persons);
            
            // 配列を初期化する
            int matchCount = keys.Length/2;

            Match[] matches = MakeMatch1(matchCount, keys, results);

            return matches;
        }

        // 「対戦していない」「同じチームじゃない」「勝ち数が同じ」で対戦
        public static Match[] MakeMatch1(int matchCount, int[] keys, Match[] results)
        {
            Match[] matches = new Match[matchCount];

            for (int i = 0; i < matchCount; i++)
            {
                // 最小のくじ番号を取得する(使われていないこと)
                int minKey = getMinimumKey(keys, matches);

                // 対応する対戦相手のくじ番号を取得する（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
                int versusKey = getVersusKey1(minKey, keys, matches, results);

                // versusKey<0なら、対戦相手は見つからなかったため、nullを返す
                if (versusKey < 0)
                {
                    return null;
                }
                //対戦を保存する
                matches[i] = new Match(minKey, versusKey);
            }
            return matches;
        }

        // 対応する対戦相手のくじ番号を取得する
        //（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
        private static int getVersusKey1(int minKey, int[] keys, Match[] matches, Match[] results)
        {
            throw new NotImplementedException();
        }

        // 最小のくじ番号を取得する(使われていないこと)
        private static int getMinimumKey(int[] keys, Match[] matches)
        {
            throw new NotImplementedException();
        }

        // 2つの配列をマージして1つにする
        public static Match[] MergeMatch(Match[] results1, Match[] results2)
        {
            return results1;
        }

        // personsのkeyを取り出して配列にする
        public static int[] GetKeyArray(Dictionary<int, Person> persons)
        {
            return persons.Keys.ToArray();
        }

    }

}
