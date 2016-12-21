using System;
using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    public class Interference {
        public static Dictionary<IRTuple, List<string>> calculateLiveness(List<BasicBlock> blocks) {

            Dictionary<IRTuple, List<string>> liveness = new Dictionary<IRTuple, List<string>>();
            var exitBlock = blocks.Find(block => block.successors.Count == 0);

            Stack<BasicBlock> worklist = new Stack<BasicBlock>();
            worklist.Push(exitBlock);

            Dictionary<int, List<string>> inList = new Dictionary<int, List<string>>();
            Dictionary<int, List<string>> outList = new Dictionary<int, List<string>>();

            while (worklist.Count != 0) {
                var block = worklist.Pop();

                outList[block.id] = UnionOfInSucc(block);
                var temp = block.Uses().Union(outList[block.id].Except(block.Defines())).ToList();

                if (!inList.ContainsKey(block.id)
                    || !(new HashSet<string>(inList[block.id]).SetEquals(new HashSet<string>(temp)))) {

                    inList[block.id] = temp;
                    foreach (var predecessor in block.predecessors) {
                        worklist.Push(predecessor);
                    }
                }
            }

            return liveness;
        }

        public static List<string> UnionOfInSucc(BasicBlock block) {
            List<string> inSet = new List<string>();
            foreach (var successor in block.successors) {
                inSet = inSet.Union(successor.Uses().Except(successor.Defines())).ToList();
            }
            return inSet;
        }
    }
}
