thisdir = tools

net_4_5_dirs := \
	al		\
	linker		\
	culevel		\
	genxs		\
	mkbundle	\
	monop		\
	mono-service	\
	mono-xsd	\
	resgen		\
	gacutil		\
	wsdl		\
	xbuild		\
	csharp		\
	corcompare	\
	mono-api-html \
	compiler-tester	\
	mono-xmltool	\
	mono-shlib-cop	\
	sgen		\
	mconfig		\
	installutil	\
	nunitreport	\
	pdb2mdb		\
	sqlsharp	\
	sqlmetal	\
	svcutil		\
	ictool		\
	disco		\
	soapsuds	\
	browsercaps-updater	\
	cil-strip	\
	macpack		\
	dtd2rng		\
	dtd2xsd		\
	mdoc		\
	mod		\
	installvst	\
	lc		\
	mono-configuration-crypto \
	ccrewrite	\
	cccheck		\
	security	\
	mdbrebase	\
	mdb2ppdb	\
	ikdasm		\
	mono-symbolicate	\
	linker-analyzer

build_SUBDIRS = resgen gacutil security culevel cil-stringreplacer commoncryptogenerator resx2sr linker
net_4_5_SUBDIRS = gacutil
net_4_5_PARALLEL_SUBDIRS = $(net_4_5_dirs)

SUBDIRS = $(build_SUBDIRS) $(net_4_5_dirs)

include ../build/rules.make

DISTFILES = \
	assemblies.xml			\
	mono-win32-setup-dark.bmp	\
	mono-win32-setup-light.bmp	\
	mono-win32-setup.nsi		\
	scan-tests.pl			\
	tinderbox/smtp.c		\
	tinderbox/tinderbox.sh		\
	removecomments.sh

test-local csproj-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local doc-update-local:
	@:

dist-local: dist-default

clean-local:
	rm -f *.exe *.dll *.pdb cormissing.xml
