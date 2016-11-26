CSHARPCOMPILER = dmcs

build: src
	mkdir -p generated
	mono bin/Coco.exe -frames src/frame -o generated -namespace Tastier src/Tastier.ATG
	$(CSHARPCOMPILER) src/*.cs generated/*.cs -out:bin/tcc.exe

compile: build
	mono bin/tcc.exe test/TastierProgram.TAS > generated/Tastier.s
	cat src/asm/TastierProjectHeader.s generated/Tastier.s src/asm/TastierProjectFooter.s > bin/TastierProject.s

clean:
	rm -f bin/TastierProject.s
	rm -f bin/tcc.exe
	rm -rf generated/
