# aad-secure-client

In this project there is a client which calls an API which needs to be authorised. The API is registered with active directory and needs to be authenticated with a token. The client will call the AAD using the application id, secret etc. from there it will get the token if all the details provided to aad is correct. Then after that api request is made with bearer token in the request header which will be validated by api and once it is validated the response is sent back.

#### Architecture ####

![alt text](https://github.com/arpitfs/aad-secure-client/blob/main/screenshots/AuthClient.png)
