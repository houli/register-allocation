namespace Tastier {
    public class IRTuple {
        public IROperation op;

        public IRTuple(IROperation op) {
            this.op = op;
        }

        public override string ToString() {
            return $"{{{op}}}";
        }
    }

    public class IRTupleLabel : IRTuple {
        public string label;

        public IRTupleLabel(IROperation op, string label) : base(op) {
            this.label = label;
        }

        public override string ToString() {
            return $"{{{op}, {label}}}";
        }
    }

    public class IRTupleBinOp : IRTuple {
        public string src1;
        public string src2;
        public string dest;

        public IRTupleBinOp(IROperation op, string src1, string src2, string dest) : base(op) {
            this.src1 = src1;
            this.src2 = src2;
            this.dest = dest;
        }

        public override string ToString() {
            return $"{{{op}, {src1}, {src2}, {dest}}}";
        }
    }

    public class IRTupleRelOp : IRTuple {
        public string src1;
        public string src2;

        public IRTupleRelOp(IROperation op, string src1, string src2) : base(op) {
            this.src1 = src1;
            this.src2 = src2;
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

        public override string ToString() {
            return $"{{{op}, {src}, {dest}}}";
        }
    }

    public class IRTupleLoadStore : IRTuple {
        public string dest;
        public string name;
        public int address;
        public int scopeLevel;
        public bool global;

        public IRTupleLoadStore(IROperation op, string dest, string name, int address, int scopeLevel, bool global) : base(op) {
            this.dest = dest;
            this.name = name;
            this.address = address;
            this.scopeLevel = scopeLevel;
            this.global = global;
        }

        public override string ToString() {
            return $"{{{op}, {dest}, {name}, {address}, {scopeLevel}, {global}}}";
        }

    }

    public class IRTupleReadWrite : IRTuple {
        public string location;

        public IRTupleReadWrite(IROperation op, string location) : base(op) {
            this.location = location;
        }

        public override string ToString() {
            return $"{{{op}, {location}}}";
        }
    }
}
