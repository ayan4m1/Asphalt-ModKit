#!/usr/bin/bash

if [ -z $TRAVIS_BUILD_NUMBER ]; then
	VERSION=1
else
	VERSION=$TRAVIS_BUILD_NUMBER
fi

sed -i "s^\(Assembly\(Informational\|File\)Version(\d34[0-9]\+\.[0-9]\+\.[0-9]\+\.\)[0-9]\+^\1$VERSION^" "Asphalt-ModKit/Properties/AssemblyInfo.cs"
