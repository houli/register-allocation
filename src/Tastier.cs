using System;
using System.Collections.Generic;

namespace Tastier {
    class Tastier {
        public static void Main (string[] arg) {
            if (arg.Length > 0) {
                Scanner scanner = new Scanner(arg[0]);
                Parser parser = new Parser(scanner);
                parser.tab = new SymbolTable(parser);
                parser.gen = new CodeGenerator();
                parser.program = new List<IRTuple>();
                parser.Parse();
                if (parser.errors.count == 0) {
                    IRTuple.patchFunctions(parser.program);
                    List<BasicBlock> blocks = BasicBlock.CreateBlocks(parser.program);
                    ControlFlowGraph.BuildCFG(blocks);
                    foreach (var block in blocks) {
                        Console.WriteLine(block);
                    }
                    Interference.calculateLiveness(blocks);
                    // Build liveness information from blocks
                    // Build interference graph
                    // RegisterAllocator.colour(RegisterAllocator.GraphBuilder());
                    // Code generation
                    Environment.Exit(0);
                } else {
                    Console.WriteLine("{0} compilation error(s)", parser.errors.count);
                    Environment.Exit(parser.errors.count);
                }
            } else {
                Console.WriteLine("-- No source file specified");
            }
            Environment.Exit(1);
        }
    }
}
