# Identity2Vue
This is a demo for showing how IdentityServer4 and VUE apps communicate with each other.

It contains the following four parts:

### Web API Part (C#)
- Identity2Vue.IdentityServer: 
  It's an authentication center,  which is implemented by IdentityServer4.
  
- Identity2Vue.WebApi:

  It's a protected API resource, which is a common ASPNET core web API project. 


### Web UI Part (VUE)
- IdentityUI:

  It's a home app, which may sign in by navigating to Identity2Vue.IdentityServer. When the authentication is passed in Identity2Vue.IdentityServer, it'll go back the home app again. At the same time, it may access products from Identity2Vue.WebApi.

- WebApp:

  It's another app, which one user may navigate from home app to. It implements single-sign-on by oidc-client.js

  At the same time, both of the UI parts support access token(1min) and refresh token (6mins).

### How to run?

You may open it with visual studio code.

1. In Run and Debug, run ".NET Core Launch (identity server)", ".NET Core Launch (identity api)","Launch via Yarn (identity ui)" and "Launch via Yarn (web app)".
2. open your browser and access http://localhost:8080.
3. click Login button, and enter username(allen) and password(321456)
4. you may expand menu, and query product list.
