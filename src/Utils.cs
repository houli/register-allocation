using System.Collections.Generic;

namespace Tastier {
    public class Utils {
        public static int FindTargetIndex(string targetLabel, List<IRTuple> statements) {
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
    }
}
