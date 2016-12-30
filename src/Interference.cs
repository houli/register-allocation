using System;
using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    public class Interference {
        // Method to create liveness information for a set of blocks
        public static Dictionary<IRTuple, List<string>> calculateLiveness(List<BasicBlock> blocks) {

            Dictionary<IRTuple, List<string>> liveness = new Dictionary<IRTuple, List<string>>();
            // Find the exit block
            var exitBlock = blocks.Find(block => block.successors.Count == 0);

            Stack<BasicBlock> worklist = new Stack<BasicBlock>();
            // Start the worklist
            worklist.Push(exitBlock);

            Dictionary<int, List<string>> inList = new Dictionary<int, List<string>>();
            Dictionary<int, List<string>> outList = new Dictionary<int, List<string>>();

            while (worklist.Count != 0) {
                // Get current worklist item
                var block = worklist.Pop();

                outList[block.id] = UnionOfInSucc(block);
                var temp = block.Uses().Union(outList[block.id].Except(block.Defines())).ToList();

                if (!inList.ContainsKey(block.id)
                    || !(new HashSet<string>(inList[block.id]).SetEquals(new HashSet<string>(temp)))) {

                    // If liveness information has changed
                    inList[block.id] = temp;
                    foreach (var predecessor in block.predecessors) {
                        worklist.Push(predecessor);
                    }
                }
            }

            return liveness;
        }

        // Union of the uses in the successor blocks
        public static List<string> UnionOfInSucc(BasicBlock block) {
            List<string> inSet = new List<string>();
            foreach (var successor in block.successors) {
                inSet = inSet.Union(successor.Uses().Except(successor.Defines())).ToList();
            }
            return inSet;
        }
    }
}
