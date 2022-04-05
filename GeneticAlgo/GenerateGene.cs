using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo
{
    internal class GenerateGene
    {
        public List<List<string>> WOSchudule(List<List<string>> wo)
        {
            Random r = new Random();
            List<List<string>> res = new();
            res.AddRange(wo);
            int x = r.Next(10);
            if (x < 7)
            {
                return res;
            }
            else
            {
                int startIndex = r.Next(0, wo.Count-1);
                int endIndex = r.Next(startIndex + 1, wo.Count);
                List<string> tmp = new();
                tmp = res[startIndex];
                res[startIndex] = res[endIndex];
                res[endIndex] = tmp;
                return res;
            }
        }

        public List<List<int>> MachineSchedule(List<List<int>> schedule)
        {
            Random r = new Random();
            int rate = r.Next(10);
            List<List<int>> res = new();
            res.AddRange(schedule);

            //mutation: 30%
            if (rate > 6)
            {
                for (int i = 0; i < schedule.Count; i++)
                {
                    res[i] = GenerateSchuduleForOneWo();
                }
            }
            //crossover: 70%
            else
            {
                Random r2 = new Random();
                int startIndex = r.Next(schedule.Count-1);
                int endIndex;
                do
                {
                    endIndex = r.Next(schedule.Count);
                }
                while (endIndex <= startIndex);
                for (int i = startIndex; i < endIndex ; i++)
                {
                    res[i] = GenerateSchuduleForOneWo();
                }
            }
            


            //log
            /*
            foreach (List<int> s in res)
            {
                foreach (int i in s)
                {
                    Console.Write(i);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("---schedule end---");
            */
            return res;
        }

        public List<int> GenerateSchuduleForOneWo()
        {
            List<int> res = new List<int>();
            Random r = new Random();
            int x;

            for (int j = 0; j < 3; j++)
            {
                x = r.Next(0, 100) % 2;
                if (x == 0)
                {
                    res.Add(0);
                    res.Add(1);
                }
                else
                {
                    res.Add(1);
                    res.Add(0);
                }
            }

            return res;

        }

        //combine wo and machines schelule
        public List<List<string>> GenerateCompleteSchedule(List<List<string>> wo, List<List<int>> schedule)
        {
            List<List<string>> res = new List<List<string>>();

            for (int i = 0; i < wo.Count; i++)
            {
                res.Add(new List<string>());
                for (int j = 0; j < wo[i].Count; j++)
                {
                    //wo name or due time
                    if (j == 0 || j == wo[i].Count - 1)
                    {
                        res[i].Add(wo[i][j].ToString());
                    }
                    //index at 1~3 is cost time
                    else
                    {
                        if (schedule[i][j * 2 - 2] == 0 && schedule[i][j * 2 - 1] == 1)
                        {
                            res[i].Add("00");
                            res[i].Add(wo[i][j]);
                        }
                        else if (schedule[i][j * 2 - 2] == 1 && schedule[i][j * 2 - 1] == 0)
                        {
                            res[i].Add(wo[i][j]);
                            res[i].Add("00");
                        }
                    }
                }
            }
            /*
            foreach (List<string> w in res)
            {
                foreach (string s in w)
                {
                    Console.Write(s+" | ");
                }
                Console.WriteLine("");
            }
            */
            return res;
        }

        public int CaculateDelay(List<List<string>> allSchedule)
        {
            int res = 0;
            int tmp;
            int dueTime;
            for (int i = 0; i < allSchedule.Count; i++)
            {
                dueTime = int.Parse(allSchedule[i][^1]);
                List<int> tmpCTList = new();
                for (int j = 1; j < allSchedule[i].Count - 1; j++)
                {
                    if (int.Parse(allSchedule[i][j]) != 0)
                    {
                        tmp = 0;
                        for (int k = i-1; k >= 0; k--)
                        {
                            tmp += int.Parse(allSchedule[k][j]);
                        }
                        tmpCTList.Add(tmp);
                        tmpCTList.Add(int.Parse(allSchedule[i][j]));
                    }
                }

                int preTime = 0;
                for (int l = 0; l < tmpCTList.Count; l+=2)//pre, ct
                {
                    preTime = preTime > tmpCTList[l] ? preTime : tmpCTList[l];
                    preTime += tmpCTList[l + 1];
                }



                preTime = preTime > dueTime ? preTime - dueTime : 0;
                res += preTime;
            }
            return res;
        }
    }
}
