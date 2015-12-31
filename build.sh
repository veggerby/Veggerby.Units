#!/bin/bash

# Copied from omnisharp https://github.com/OmniSharp/omnisharp-roslyn/blob/master/build.sh

if (! $TRAVIS) then
    pushd "$(dirname "$0")"
fi

rm -rf artifacts
if ! type dnvm > /dev/null 2>&1; then
    curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_BRANCH=dev sh && source ~/.dnx/dnvm/dnvm.sh
fi

# work around restore timeouts on Mono
[ -z "$MONO_THREADS_PER_CPU" ] && export MONO_THREADS_PER_CPU=50

dnvm update-self
dnvm install 1.0.0-rc1-update1
dnvm use 1.0.0-rc1-update1
dnu restore
rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi

pushd test/Veggerby.Units.Tests
dnx test -parallel none
rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi
popd

dnvm use 1.0.0-rc1-update1

dnu pack src/Veggerby.Units --configuration Release --out artifacts/build/nuget --quiet

dnu publish src/Veggerby.Units --configuration Release --no-source --out artifacts/build/Veggerby.Units --runtime dnx-mono.1.0.0-rc1-update1

# work around for kpm bundle returning an exit code 0 on failure
grep "Build failed" buildlog
rc=$?; if [[ $rc == 0 ]]; then exit 1; fi

tree -if artifacts/build/Veggerby.Units | grep .nupkg | xargs rm
pushd artifacts/build/Veggerby.Units
tar -zcf ../../../Veggerby.Units.tar.gz .
popd

tree artifacts

if (! $TRAVIS) then
    popd
fi
