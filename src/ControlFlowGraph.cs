using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    public class ControlFlowGraph {
        public static void BuildCFG(List<BasicBlock> blocks) {
            for (int i = 0; i < blocks.Count; i++) {
                var lastTuple = blocks[i].statements.Last();
                BasicBlock foundBlock = null;

                switch (lastTuple.op) {
                // For a branch the successor is the called block
                case IROperation.BRANCH:
                    foundBlock = FindBlock(blocks, ((IRTupleLabel)lastTuple).label, true);
                    blocks[i].successors.Add(foundBlock);
                    foundBlock.predecessors.Add(blocks[i]);
                    break;

                // Branch on false has two successors, one for true, one for false
                case IROperation.BFALSE:
                    blocks[i].successors.Add(blocks[i + 1]);
                    blocks[i + 1].predecessors.Add(blocks[i]);

                    foundBlock = FindBlock(blocks, ((IRTupleLabel)lastTuple).label, true);
                    blocks[i].successors.Add(foundBlock);
                    foundBlock.predecessors.Add(blocks[i]);
                    break;

                case IROperation.CALL:
                    // Find the called block
                    var calledBlock = FindBlock(blocks, ((IRTupleLabel)lastTuple).label + "Body", true);
                    blocks[i].successors.Add(calledBlock);
                    calledBlock.predecessors.Add(blocks[i]);

                    // Find the block containing the functions return tuple
                    var returnBlock = FindBlock(blocks, ((IRTupleLabel)lastTuple).label, false);
                    returnBlock.successors.Add(blocks[i + 1]);
                    blocks[i + 1].predecessors.Add(returnBlock);
                    break;

                case IROperation.END:
                case IROperation.RET:
                    break;

                default:
                    // Otherwise add the next block to the successors
                    blocks[i].successors.Add(blocks[i + 1]);
                    blocks[i + 1].predecessors.Add(blocks[i]);
                    break;
                }
            }
        }

        private static BasicBlock FindBlock(List<BasicBlock> blocks, string label, bool first) {
            foreach (var block in blocks) {
                IRTuple stmt = null;
                if (first) {
                    stmt = block.statements.First();
                } else {
                    stmt = block.statements.Last();
                }
                if (stmt is IRTupleLabel
                    && (stmt.op == IROperation.LABEL || stmt.op == IROperation.RET)
                    && ((IRTupleLabel)stmt).label == label) {
                    return block;
                }
            }
            throw new System.IndexOutOfRangeException($"No {label} block found!");
        }
    }
}
