using System;

namespace Tastier {

class Tastier {

	public static void Main (string[] arg) {
		if (arg.Length > 0) {
			Scanner scanner = new Scanner(arg[0]);
			Parser parser = new Parser(scanner);
			parser.tab = new SymbolTable(parser);
			parser.gen = new CodeGenerator();
			parser.Parse();
            if (parser.errors.count == 0)
                Environment.Exit(0);
            else {
                Console.WriteLine("{0} compilation error(s)", parser.errors.count);
                Environment.Exit(parser.errors.count);
            }
        } else Console.WriteLine("-- No source file specified");
        Environment.Exit(1);
    }

}

} // end namespace
