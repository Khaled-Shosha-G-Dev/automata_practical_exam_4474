using System.Linq;

namespace minimization_DFA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DFA dfa= new DFA();
            dfa.SetState();
            dfa.partetioning();
            dfa.MinimizePartitions();
        }
    }
    class DFA
    {
        public Dictionary<string, string[]> _States = new Dictionary<string, string[]>();
        Dictionary<string, string[]> finalGroup = new Dictionary<string, string[]>();
        Dictionary<string, string[]> nonFinalGroup = new Dictionary<string, string[]>();
        public List<Dictionary<string, string[]>> partetion = new List<Dictionary<string, string[]>>();

        public List<string> finalState = new List<string>();
        public List<string> startState=new List<string>();

        public void SetState()
        {
            Console.WriteLine("all states : ");
            string[] States = Console.ReadLine().Split(",");
            foreach (var state in States)
            {
                Console.WriteLine($"Set output to state \"{state}\"  0 then 1 :");
                _States.Add(state, Console.ReadLine().Split(",")) ;
            }

            Console.WriteLine("Start state : ");
            string[] startStates = Console.ReadLine().Split(",");
            foreach (var state in  startStates)
            {
                startState.Add(state);
            }
            Console.WriteLine("Final state : ");
            string[] finalStates = Console.ReadLine().Split(",");
            foreach (var state in finalStates)
            {
                finalState.Add(state);
            }
        }
        public void partetioning()
        {
            foreach(var state in _States)
            {
                if (finalState.Contains(state.Key))
                {
                    finalGroup[state.Key] = state.Value;
                }
                else
                {
                    nonFinalGroup[state.Key] = state.Value;
                }
            }
            if (finalGroup.Count > 0)
                partetion.Add(finalGroup);

            if (nonFinalGroup.Count > 0)
                partetion.Add(nonFinalGroup);
        }
        public void PrintPartition()
        {
            int groupNum = 1;
            foreach (var group in partetion)
            {
                Console.WriteLine($"Group {groupNum++}:");
                foreach (var kv in group)
                {
                    Console.WriteLine($"  {kv.Key} => {string.Join(",", kv.Value)}");
                }
            }
        }
        public void MinimizePartitions()
        {
            bool changed;

            do
            {
                changed = false;
                var newPartitions = new List<Dictionary<string, string[]>>();

                foreach (var group in partetion)
                {
                    var processed = new HashSet<string>();
                    foreach (var state in group)
                    {
                        if (processed.Contains(state.Key))
                            continue;

                        var newGroup = new Dictionary<string, string[]>();
                        newGroup[state.Key] = state.Value;
                        processed.Add(state.Key);

                        foreach (var other in group)
                        {
                            if (processed.Contains(other.Key))
                                continue;

                            if (SameGroup(state.Key, other.Key))
                            {
                                newGroup[other.Key] = other.Value;
                                processed.Add(other.Key);
                            }
                        }

                        newPartitions.Add(newGroup);
                    }
                }

                if (newPartitions.Count != partetion.Count)
                {
                    changed = true;
                    partetion = newPartitions;
                }

            } while (changed);

            PrintPartition();
        }

        private bool SameGroup(string state1, string state2)
        {
            var next1 = _States[state1];
            var next2 = _States[state2];

            for (int i = 0; i < next1.Length; i++)
            {
                string dest1 = next1[i];
                string dest2 = next2[i];

                int group1 = GetGroupIndex(dest1);
                int group2 = GetGroupIndex(dest2);

                if (group1 != group2)
                    return false;
            }

            return true;
        }

        private int GetGroupIndex(string state)
        {
            for (int i = 0; i < partetion.Count; i++)
            {
                if (partetion[i].ContainsKey(state))
                    return i;
            }
            return -1; // not found
        }

    }
}