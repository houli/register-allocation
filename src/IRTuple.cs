namespace Tastier {
    public class IRTuple {
        private IROperation op;

        public IRTuple(IROperation op) {
            this.op = op;
        }

        public override string ToString() {
            return $"{{{op}}}";
        }
    }
}
