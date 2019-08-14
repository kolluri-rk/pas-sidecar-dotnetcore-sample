# pcf-sidecar-dotnetcore-sample

**Sidecars** feature in PCF allows to run additional processes in the same container as your app. Learn more about sidecars [here](http://v3-apidocs.cloudfoundry.org/version/release-candidate/#sidecars). 

## Sample Apps

This repository contains sample apps to demonstrate sidecars feature in PCF. 

### auth-server-sidecar
A simple asp.net core web api that provides autherization inforamtion of logged-in user. Parent/main app will make calls to this web api (runs in sidecar process) using `localhost:{configured-port}`.

`/api/auth/{appid}/users/{userid}` endpoint provides logged-in user's scopes for the given app.

### sidecar-dependent-app
A simple asp.net core mvc app that calls out to the `auth-server-sidecar` to get logged-in user scopes. This emulates user logged into multiple apps (App1, App2 and App3), and displays user scopes for the respective app.

## Build and Push to PCF

`build_and_push_app_with_sidecar.sh` script will build both webapi and mvc apps, and push to PCF.  

*Note:Just be sure to target your Org and Space using `cf cli` before running this script.* 

MVC app uses `AuthzBaseUrl` config setting to call out to webapi. This setting is passed as environment varibale in `manifest.yml`. In this sample AuthzBaseUrl was set to `http://localhost:8081`.

Make sure to start the app on the **same port** in sidecar process. auth-server-sidecar api will be started with command: `cd auth-server && exec ./auth-server-sidecar --urls http://0.0.0.0:8081`
