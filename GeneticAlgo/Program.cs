using GeneticAlgo;
//3 types(M1, M2, M3) of machine and 2 of each type(M1.2, M1.2...)
#region main

#region intialize WOs and machines schedule 
List<List<string>> Wo = new();//wo, ct_,1, ct_m2, ct_m3, due time
Wo.Add(new List<string> { "wo1", "10", "20", "30", "010" });
Wo.Add(new List<string> { "wo2", "20", "50", "15", "020" });
Wo.Add(new List<string> { "wo3", "05", "30", "10", "015" });
Wo.Add(new List<string> { "wo4", "30", "10", "05", "050" });
Wo.Add(new List<string> { "wo5", "20", "10", "50", "100" });

List<List<int>> schedule = new();
schedule.Add(new List<int> { 1, 0, 1, 0, 1, 0 });
schedule.Add(new List<int> { 1, 0, 1, 0, 1, 0 });
schedule.Add(new List<int> { 1, 0, 1, 0, 1, 0 });
schedule.Add(new List<int> { 1, 0, 1, 0, 1, 0 });
schedule.Add(new List<int> { 1, 0, 1, 0, 1, 0 });
#endregion
//**********
int iterationTimes = 10000;
//**********


int i = 0;
GenerateGene g = new GenerateGene();
while (i < iterationTimes)
{
    var newWO = g.WOSchudule(Wo);
    var newSchedule = g.MachineSchedule(schedule);
    int record = g.CaculateDelay(g.GenerateCompleteSchedule(Wo, schedule));
    int newTmp = g.CaculateDelay(g.GenerateCompleteSchedule(newWO, newSchedule));
    if (record > newTmp)
    {
        Console.WriteLine(newTmp + " is better than " + record +" at "+ i + "round");

        foreach (List<string> w in g.GenerateCompleteSchedule(newWO, newSchedule))
        {
            foreach (string s in w)
            {
                Console.Write(s + " | ");
            }
            Console.WriteLine("");
        }
        Console.WriteLine("----------");

        Wo = newWO;
        schedule = newSchedule;
    }



    i++;
}

Console.WriteLine("---end scheduling---");

#endregion

