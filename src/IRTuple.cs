using System.Collections.Generic;

// This file contains definitons for the different classes of tuples used
// as well as methos "Uses" and "Defines" for accessing uses and defines information
// for any particular tuple
namespace Tastier {
    public class IRTuple {
        public IROperation op;

        public IRTuple(IROperation op) {
            this.op = op;
        }

        public override string ToString() {
            return $"{{{op}}}";
        }

        public virtual List<string> Uses() {
            return new List<string>();
        }

        public virtual List<string> Defines() {
            return new List<string>();
        }

        public static void patchFunctions(List<IRTuple> tuples) {
            for (int i = 0; i < tuples.Count; i++) {
                var tuple = tuples[i];
                if (tuple.op == IROperation.ENTER) {
                    tuples.Remove(tuple);
                    var index = Utils.FindTargetIndex(((IRTupleEnter)tuple).name + "Body", tuples);
                    tuples.Insert(index + 1, tuple);
                }
            }
        }
    }

    public class IRTupleLabel : IRTuple {
        public string label;

        public IRTupleLabel(IROperation op, string label) : base(op) {
            this.label = label;
        }

        public override List<string> Uses() {
            return base.Uses();
        }

        public override List<string> Defines() {
            return base.Defines();
        }

        public override string ToString() {
            return $"{{{op}, {label}}}";
        }
    }

    public class IRTupleBinOp : IRTuple {
        public string src1;
        public string src2;

        public IRTupleBinOp(IROperation op, string src1, string src2) : base(op) {
            this.src1 = src1;
            this.src2 = src2;
        }

        public override List<string> Uses() {
            return new List<string> {src1, src2};
        }

        public override List<string> Defines() {
            return new List<string> {src1};
        }

        public override string ToString() {
            return $"{{{op}, {src1}, {src2}, {src1}}}";
        }
    }

    public class IRTupleRelOp : IRTuple {
        public string src1;
        public string src2;

        public IRTupleRelOp(IROperation op, string src1, string src2) : base(op) {
            this.src1 = src1;
            this.src2 = src2;
        }

        public override List<string> Uses() {
            return new List<string> {src1, src2};
        }

        public override List<string> Defines() {
            return base.Defines();
        }

        public override string ToString() {
            return $"{{{op}, {src1}, {src2}}}";
        }
    }

    public class IRTupleMove : IRTuple {
        public string src;
        public string dest;

        public IRTupleMove(IROperation op, string src, string dest) : base(op) {
            this.src = src;
            this.dest = dest;
        }

        public override List<string> Uses() {
            if (op == IROperation.NEG) {
                return new List<string> {src};
            } else {
                return new List<string>();
            }
        }

        public override List<string> Defines() {
            return new List<string> {dest};
        }

        public override string ToString() {
            return $"{{{op}, {src}, {dest}}}";
        }
    }

    public class IRTupleLoadStore : IRTuple {
        public string dest;
        public string name;
        public int address;
        public int scopeLevel;

        public IRTupleLoadStore(IROperation op, string dest, string name, int address, int scopeLevel) : base(op) {
            this.dest = dest;
            this.name = name;
            this.address = address;
            this.scopeLevel = scopeLevel;
        }

        public override List<string> Uses() {
            if (op == IROperation.LOAD) {
                return new List<string> {name};
            } else {
                return new List<string> {dest};
            }
        }

        public override List<string> Defines() {
            if (op == IROperation.LOAD) {
                return new List<string> {dest};
            } else {
                return new List<string> {name};
            }
        }

        public override string ToString() {
            return $"{{{op}, {dest}, {name}, {address}, {scopeLevel}}}";
        }

    }

    public class IRTupleWriteLocation : IRTuple {
        public string location;

        public IRTupleWriteLocation(IROperation op, string location) : base(op) {
            this.location = location;
        }

        public override List<string> Uses() {
            return new List<string> {location};
        }

        public override List<string> Defines() {
            return base.Defines();
        }

        public override string ToString() {
            return $"{{{op}, {location}}}";
        }
    }

    public class IRTupleWriteLiteral : IRTuple {
        public string literal;

        public IRTupleWriteLiteral(IROperation op, string literal) : base(op) {
            this.literal = literal;
        }

        public override List<string> Uses() {
            return base.Uses();
        }

        public override List<string> Defines() {
            return base.Defines();
        }

        public override string ToString() {
            return $"{{{op}, \"{literal}\"}}";
        }
    }


    public class IRTupleEnter : IRTuple {
        public string name;
        public int level;
        public int variableCount;

        public IRTupleEnter(string name, int level, int variableCount) : base(IROperation.ENTER) {
            this.name = name;
            this.level = level;
            this.variableCount = variableCount;
        }

        public override List<string> Uses() {
            return base.Uses();
        }

        public override List<string> Defines() {
            return base.Defines();
        }

        public override string ToString() {
            return $"{{{op}, {name}, {level}, {variableCount}}}";
        }
    }
}
