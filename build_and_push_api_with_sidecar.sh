#!/usr/bin/env bash

MVC_APP_PATH="sidecar-dependent-app"
AUTH_SERVER_SIDECAR_PATH="auth-server-sidecar"
PUBLISH_PATH="bin/publish"

function clean() {
    rm -rf "${MVC_APP_PATH}/${PUBLISH_PATH}"
    rm -rf "${AUTH_SERVER_SIDECAR_PATH}/${PUBLISH_PATH}"
}

function build() {
    pushd ${MVC_APP_PATH} > /dev/null
        dotnet publish -f netcoreapp2.2 -r linux-x64 -o ${PUBLISH_PATH}
    popd > /dev/null

    mkdir -p "${MVC_APP_PATH}/${PUBLISH_PATH}/auth-server"
    pushd ${AUTH_SERVER_SIDECAR_PATH} > /dev/null
        dotnet publish -f netcoreapp2.2 -r linux-x64 -o ${PUBLISH_PATH}
    popd > /dev/null
    cp -r "${AUTH_SERVER_SIDECAR_PATH}/${PUBLISH_PATH}/." "${MVC_APP_PATH}/${PUBLISH_PATH}/auth-server"
}

function push() {
    cf v3-create-app sidecar-dependent-app
    cf v3-apply-manifest -f manifest.yaml
    cf v3-push sidecar-dependent-app -p "${MVC_APP_PATH}/${PUBLISH_PATH}"
}

function main()
{
    clean
    build
    push
}

main