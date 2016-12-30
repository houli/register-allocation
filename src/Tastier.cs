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
                    Console.WriteLine("----IR Tuples----");
                    parser.program.ForEach(Console.WriteLine);
                    Console.WriteLine("");

                    List<BasicBlock> blocks = BasicBlock.CreateBlocks(parser.program);
                    Console.WriteLine("----Basic Blocks----");
                    blocks.ForEach(Console.WriteLine);
                    Console.WriteLine("");

                    ControlFlowGraph.BuildCFG(blocks);
                    Console.WriteLine("----Basic Blocks After Control Flow Graph Creation----");
                    blocks.ForEach(Console.WriteLine);
                    Console.WriteLine("");

                    Interference.calculateLiveness(blocks);
                    // Build interference graph
                    Console.WriteLine("----Register Allocator Test----");
                    RegisterAllocator.colour(RegisterAllocator.GraphBuilder());
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
