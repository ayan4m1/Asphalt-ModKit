#!/usr/bin/bash

if [ -z "$TRAVIS_BUILD_NUMBER" ]; then
	VERSION=1
else
	VERSION=$TRAVIS_BUILD_NUMBER
fi

if [ ! -z "$TRAVIS_BRANCH" ]; then
	sed -i "s#\(AssemblyConfiguration(\d34\).*\d34#\1$TRAVIS_BRANCH\d34#" "Asphalt-ModKit/Properties/AssemblyInfo.cs"
fi

sed -i "s#\(Assembly\(Informational\|File\)\?Version(\d34[0-9]\+\.[0-9]\+\.[0-9]\+\.\)[0-9]\+#\1$VERSION#" "Asphalt-ModKit/Properties/AssemblyInfo.cs"

ASSEMBLY_VERSION=$(cat Asphalt-ModKit/Properties/AssemblyInfo.cs | grep -oP 'AssemblyVersion\("\K[0-9]+.[0-9]+.[0-9]+.[0-9]+')
OUTPUT_FILE="AsphaltModKit.$ASSEMBLY_VERSION.nupkg"

nuget pack "Asphalt-ModKit/Asphalt.nuspec" -Version $ASSEMBLY_VERSION
