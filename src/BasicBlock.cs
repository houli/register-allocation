using System;
using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    public class BasicBlock {
        public List<IRTuple> statements { get; }
        public List<BasicBlock> successors { get; }
        public List<BasicBlock> predecessors { get; }
        public int id { get; }

        public BasicBlock(List<IRTuple> statements, int id) {
            this.statements = statements;
            this.successors = new List<BasicBlock>();
            this.predecessors = new List<BasicBlock>();
            this.id = id;
        }

        public override string ToString() {
            var successorStr = "[" + string.Join(", ",
                successors.Select(i => i.id.ToString()).ToArray()) + "]";

            var predecessorStr = "[" + string.Join(", ",
                predecessors.Select(i => i.id.ToString()).ToArray()) + "]";

            var str = $"Block number: {id} Successors: {successorStr} Predecessors: {predecessorStr}\n\n";
            foreach (var statement in statements) {
                str += $"{statement}\n";
            }
            return $"{str}\n";
        }

        public List<string> Uses() {
            var uses = new List<string>();
            foreach (var statement in statements) {
                uses = uses.Union(statement.Uses()).ToList();
            }
            return uses;
        }

        public List<string> Defines() {
            var defines = new List<string>();
            foreach (var statement in statements) {
                defines = defines.Union(statement.Defines()).ToList();
            }
            return defines;
        }

        public static List<BasicBlock> CreateBlocks(List<IRTuple> statements) {
            var blocks = new List<BasicBlock>();
            var leaders = new bool[statements.Count];

            // First statement is a leader
            leaders[0] = true;
            int k = 0;
            foreach (var statement in statements) {
                if (IsBranch(statement.op) || IsBoundary(statement.op)) {
                    if (statement.op == IROperation.CALL) {
                        var ind = Utils.FindTargetIndex(((IRTupleLabel)statement).label + "Body", statements);
                        leaders[ind] = true;
                    }
                    else if (!IsBoundary(statement.op)) {
                        var ind = Utils.FindTargetIndex(((IRTupleLabel)statement).label, statements);
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

        private static bool IsBranch(IROperation op) {
            return op == IROperation.BFALSE || op == IROperation.BRANCH || op == IROperation.CALL;
        }

        private static bool IsBoundary(IROperation op) {
            return op == IROperation.RET || op == IROperation.END;
        }

    }
}
