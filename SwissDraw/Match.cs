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

        public Match(int i, int j)
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
            int matchCount = keys.Length / 2;

            // 勝数でkeyを分割する
            int[][] SplittedKeys = splitPersons(persons, results);


            Match[] matches = MakeMatch1(matchCount, SplittedKeys, persons, results);

            return matches;
        }


        // 「対戦していない」「同じチームじゃない」「勝ち数が同じ」で対戦
        public static Match[] MakeMatch1(int matchCount, int[][] SplittedKeys, Dictionary<int, Person> persons, Match[] results)
        {
            Match[] matches = new Match[matchCount];

            for (int i = 0; i < matchCount; i++)
            {
                // 最小のくじ番号を取得する(使われていないこと)
                int minKey = GetMinimumKey(SplittedKeys, matches);

                // 対応する対戦相手のくじ番号を取得する（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
                int versusKey = getVersusKey1(minKey, SplittedKeys, matches, persons, results);

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

        // 「対戦していない」「同じチームじゃない」で対戦
        public static Match[] MakeMatch2(int matchCount, int[][] SplittedKeys, Dictionary<int, Person> persons, Match[] results)
        {
            Match[] matches = new Match[matchCount];

            for (int i = 0; i < matchCount; i++)
            {
                // 最小のくじ番号を取得する(使われていないこと)
                int minKey = GetMinimumKey(SplittedKeys, matches);

                // 対応する対戦相手のくじ番号を取得する（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
                int versusKey = getVersusKey2(minKey, SplittedKeys, matches, persons, results);

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
        

        // 勝ち数の多いほうから順番に、勝ち数が同じ人のIDを配列にまとめる
        public static int[][] splitPersons(Dictionary<int, Person> persons, Match[] results)
        {
            Dictionary<int, int> winCountDic = new Dictionary<int, int>();
            int[] keyArray = GetKeyArray(persons);
            int maxWinCount = -1;
            foreach (int i in keyArray)
            {
                int wCount = getWinCount(i, results);
                winCountDic.Add(i, wCount);
                if (maxWinCount < wCount)
                {
                    maxWinCount = wCount;
                }
            }

          
            if (results.Length == 0)
            {
                int[][] result = new int[1][];
                result[0] = keyArray;
                return result;

            }
            else
            {
                //3                   6
                int ikkainotaisensu = keyArray.Length / 2;
                //2              3                3
                int nannkaisen = results.Length / ikkainotaisensu + 1;
                
                 
                int[][] result = new int[nannkaisen][];

                int zensyo = nannkaisen - 1;

                int j = 0;

                for (int i = zensyo; 0 <= i; i--)
                {
                    
                    result[j] = makePersonArray(i, winCountDic);
                    j++;
                }

                return result;
            }
            
        }

        // winCountDicの中で、勝数がvの要素の配列を生成する
        public static int[] makePersonArray(int v, Dictionary<int, int> winCountDic)
        {
            List<int> l
                = new List<int>();
            foreach (int i in winCountDic.Keys)
            {
                if (winCountDic[i] == v)
                {
                    l.Add(i);
                }
            }
            return l.ToArray();
        }

        // 対応する対戦相手のくじ番号を取得する
        //（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
        public static int getVersusKey1(int minKey, int[][] splittedKeys, Match[] matches,
            Dictionary<int, Person> persons, Match[] results)
        {
            int i = 0;
            bool flag = true;
            while (flag == true)
            {
                if (splittedKeys[i].Contains(minKey) == true)
                {
                    flag = false;
                }
                else
                {
                    i++;
                }
            }

            foreach (int key in splittedKeys[i])
            {
                if (key != minKey)
                {
                    if (containsKey(matches, key) == false)
                    {
                        if (isMatched(results, key, minKey) == false)
                        {
                            if (isSameGroup(persons, key, minKey) == false)
                            {
                                return key;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        // 対応する対戦相手のくじ番号を取得する
        //（使われていない、対戦していない、同じチームじゃない、勝ち数無視）
        public static int getVersusKey2(int minKey, int[][] splittedKeys, Match[] matches,
            Dictionary<int, Person> persons, Match[] results)
        {
            int i = 0;
            bool flag = true;
            while (flag == true)
            {
                if (splittedKeys[i].Contains(minKey) == true)
                {
                    flag = false;
                }
                else
                {
                    i++;
                }
            }
            while (i <= splittedKeys.Length)
            {
                foreach (int key in splittedKeys[i])
                {
                    if (key != minKey)
                    {
                        if (containsKey(matches, key) == false)
                        {
                            if (isMatched(results, key, minKey) == false)
                            {
                                if (isSameGroup(persons, key, minKey) == false)
                                {
                                    return key;
                                }
                            }
                        }
                    }
                }
                i++;
            }
            return -1;
        }

        // 同じグループか調べる
        public static bool isSameGroup(Dictionary<int, Person> persons, int i, int j)
        {
            String iTeam = persons[i].PersonGroup;
            String jTeam = persons[j].PersonGroup;
            return iTeam.Equals(jTeam);
        }

        // resultsの中に、iとjの対戦があればtrueを返す
        public static bool isMatched(Match[] results, int i, int j)
        {
            foreach (Match result in results)
            {
                if (result.person1 == i && result.person2 == j)
                {
                    return true;
                }
                else if (result.person2 == i && result.person1 == j)
                {
                    return true;
                }
                else
                {
                }
            }
            return false;
        }

        // mがiとjの対戦ならtrueを返す
        private static bool isMatched(Match m, int i, int j)
        {
            if (m.person1 == i)
            {
                if (m.person2 == j)
                {
                    return true;
                }
            }
            if (m.person2 == i)
            {
                if (m.person1 == j)
                {
                    return true;
                }
            }
            return false;
        }

        // 指定されたkeyの勝ち数を調べる
        public static int getWinCount(int key, Match[] results)
        {
            int winCount = 0;
            foreach (Match result in results)
            {
                if (checkWin(key, result) == true)
                {
                    winCount++;
                }
            }
            return winCount;
        }

        // keyが勝っていればtrueを返す
        private static Boolean checkWin(int key, Match result)
        {
            if (containsKey(result, key) == true)
            {
                if (result.result == 1)
                {
                    if (result.person1 == key)
                    {
                        return (true);
                    }
                }
                else if (result.result == 2)
                {
                    if (result.person2 == key)
                    {
                        return (true);
                    }
                }
            }
            return false;
        }

        // 最小のくじ番号を取得する(使われていないこと)
        public static int GetMinimumKey(int[][] splittedKeys, Match[] matches)
        {
            int m = 1000;
            foreach (int[] Keys in splittedKeys)
            {
                foreach (int Key in Keys)
                {
                    if (m > Key)
                    {
                        if (containsKey(matches, Key) == false)
                        {
                            m = Key;
                        }
                    }
                    
                }
                if (m != 1000)
                {
                    return m;
                }
            }
            return m;
        }

        public static bool containsKey(Match[] matches, int i)
        {
            foreach (Match result in matches)
            {
                if (!(result == null) && containsKey(result, i) == true )
                {
                    return true;
                }
            }
            return false;
        }
        private static bool containsKey(Match m, int i)
        {
            if (m.person1 == i)
            {
                return true;
            }

            if (m.person2 == i)
            {
                return true;
            }
            return false;
        }

        // 2つの配列をマージして1つにする
        public static Match[] MergeMatch(Match[] results1, Match[] results2)
        {
            //コレクションを作成する
            System.Collections.Generic.List<Match>
            mergedList = new System.Collections.Generic.List<Match>(results1.Length + results2.Length);

            //配列をコレクションに追加する
            mergedList.AddRange(results1);
            mergedList.AddRange(results2);

            //配列に変換する
            Match[] mergedArray = mergedList.ToArray();

            return mergedArray;
        }

        // personsのkeyを取り出して配列にする
        public static int[] GetKeyArray(Dictionary<int, Person> persons)
        {
            return persons.Keys.ToArray();
        }

    }
}
