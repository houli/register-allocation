using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    public class BasicBlock {
        public List<IRTuple> statements { get; }
        public int id { get; }

        public BasicBlock(List<IRTuple> statements, int id) {
            this.statements = statements;
            this.id = id;
        }

        public override string ToString() {
            var s = $"Block number: {id}\n\n";
            foreach (var statement in statements) {
                s += $"{statement}\n";
            }
            return $"{s}\n";
        }

        public static List<BasicBlock> CreateBlocks(List<IRTuple> statements) {
            var blocks = new List<BasicBlock>();
            var leaders = new bool[statements.Count];

            // First statement is a leader
            leaders[0] = true;
            int k = 0;
            foreach (var statement in statements) {
                if (IsBranch(statement.op) || IsBoundary(statement.op)) {
                    if (!IsBoundary(statement.op)) {
                        var ind = FindTargetIndex(((IRTupleLabel)statement).label, statements);
                        leaders[ind] = true;
                    }
                    if (k < statements.Count - 1) {
                        leaders[k + 1] = true;
                    }
                }
                k++;
            }

            int tempid = 0;
            int i = 0;
            while (i < leaders.Length) {
                int j;
                for (j = i + 1; j < leaders.Length; j++) {
                    if (leaders[j]) {
                        var block = new BasicBlock(statements.Skip(i).Take(j - i).ToList(), tempid++);
                        blocks.Add(block);
                        break;
                    }
                }
                if (j >= leaders.Length) {
                    blocks.Add(new BasicBlock(statements.Skip(i).Take(j - i).ToList(), tempid++));
                }
                i = j;
            }
            return blocks;
        }

        private static int FindTargetIndex(string targetLabel, List<IRTuple> statements) {
            int i = 0;
            foreach (var statement in statements) {
                if (statement is IRTupleLabel
                    && statement.op == IROperation.LABEL
                    && ((IRTupleLabel)statement).label == targetLabel)
                {
                    return i;
                }
                i++;
            }
            throw new System.IndexOutOfRangeException($"{targetLabel} not found!");
        }

        private static bool IsBranch(IROperation op) {
            return op == IROperation.BFALSE || op == IROperation.BRANCH;
        }

        private static bool IsBoundary(IROperation op) {
            return op == IROperation.RET || op == IROperation.END;
        }

    }
}
