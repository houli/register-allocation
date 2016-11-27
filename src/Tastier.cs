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
                    foreach (var tuple in parser.program) {
                        Console.WriteLine(tuple);
                    }
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
