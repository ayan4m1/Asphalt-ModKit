ASSEMBLY_VERSION=$(cat Asphalt/Properties/AssemblyInfo.cs | grep -oP 'AssemblyVersion\("\K[0-9]+.[0-9]+.[0-9]+.[0-9]+')
echo "Extracted version $ASSEMBLY_VERSION"

if [ -z "$TRAVIS_COMMIT" ]; then
	TRAVIS_COMMIT=$(git rev-parse HEAD)
	TRAVIS_COMMIT="${TRAVIS_COMMIT:0:8}"
fi

if [ ! -z "$TRAVIS_BRANCH" ]; then
	sed -i "s#AssemblyConfiguration(\d34.*\d34)#AssemblyConfiguration(\d34$TRAVIS_BRANCH\d34)#" "Asphalt/Properties/AssemblyInfo.cs"
fi

ASSEMBLY_INFO_VERSION="$ASSEMBLY_VERSION-$TRAVIS_COMMIT"
echo "Setting informational version to $ASSEMBLY_INFO_VERSION"

sed -i "s#AssemblyInformationalVersion(\d34.*\d34)#AssemblyInformationalVersion(\"$ASSEMBLY_INFO_VERSION\")#" "Asphalt/Properties/AssemblyInfo.cs"

OUTPUT_FILE="Asphalt.$ASSEMBLY_INFO_VERSION.nupkg"
echo "Preparing to build $OUTPUT_FILE"

./nuget.exe pack "Asphalt/Asphalt.nuspec" -Version "$ASSEMBLY_INFO_VERSION"

if [ -z "$NUGET_API_KEY" ] || [ -z "$NUGET_FEED_URL" ]; then
	echo "No NuGet API key/feed URL provided, skipping deploy!"
else
	./nuget.exe push $OUTPUT_FILE $NUGET_API_KEY -source $NUGET_FEED_URL
fi
