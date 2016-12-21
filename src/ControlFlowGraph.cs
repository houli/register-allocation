using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    public class ControlFlowGraph {
        public static void BuildCFG(List<BasicBlock> blocks) {
            for (int i = 0; i < blocks.Count; i++) {
                var lastTuple = blocks[i].statements.Last();
                switch (lastTuple.op) {
                case IROperation.BRANCH:
                    blocks[i].successors.Add(FindBlock(blocks, ((IRTupleLabel)lastTuple).label));
                    break;

                case IROperation.BFALSE:
                    blocks[i].successors.Add(blocks[i + 1]);
                    blocks[i].successors.Add(FindBlock(blocks, ((IRTupleLabel)lastTuple).label));
                    break;

                case IROperation.END:
                case IROperation.RET:
                    break;

                default:
                    blocks[i].successors.Add(blocks[i + 1]);
                    break;
                }
            }
        }

        private static BasicBlock FindBlock(List<BasicBlock> blocks, string label) {
            foreach (var block in blocks) {
                var stmt = block.statements.First();
                if (stmt is IRTupleLabel
                    && stmt.op == IROperation.LABEL
                    && ((IRTupleLabel)stmt).label == label) {
                    return block;
                }
            }
            throw new System.IndexOutOfRangeException($"No {label} block found!");
        }
    }
}
