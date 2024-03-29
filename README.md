<img src="https://raw.githubusercontent.com/wellingtonpoll/aes-cipher-encryptation/master/Assets/Header_Encryption.png" alt="AES Cipher Encryptation"> 


What is the AES Cipher Encryptation?
=====================
The AES Cipher Encryptation is a open-source project written in .NET Core to encrypt and decrypt using AES cipher algorithm.
By default configurations like, mode CBC, 128 bits crypto key size and Pkcs7 padding.
There are other modes and paddings to use with this algorithm but they are not implemented. 

[![Build status](https://ci.appveyor.com/api/projects/status/rl2ja69994rt3ei6?svg=true)](https://ci.appveyor.com/project/wellingtonpoll/aes-cipher-encryptation)

## Give a Star! :star:
If you liked the project or if AES Cipher Encryptation helped you, please give a star ;)

## How to use:
- You will need the latest VSCode and the latest .NET Core SDK.
- **Please check if you have installed the same runtime version (SDK) described in global.json**
- The latest SDK and tools can be downloaded from https://dot.net/core.
To build the docker image on terminal/cmd find "Encrypt.IO.API" folder and execute the commands below:

Build image: ```docker build -t aes-cipher-encryptation-api .```

Run container: ```docker run -e ASPNETCORE_ENVIRONMENT=production -p 80:80 aes-cipher-encryptation-api```

Also you can run the AES Cipher Encryptation in Visual Studio Code (Windows, Linux or MacOS).

To know more about how to setup your enviroment visit the [Microsoft .NET Download Guide](https://www.microsoft.com/net/download)

## Technologies implemented:

- ASP.NET Core 2.2 (with .NET Core 2.2)
 - ASP.NET MVC Core 
 - ASP.NET WebApi Core
- Docker
- Serilog
- Swagger UI

## Architecture:

- Microservice

## Pull-Requests 
Make a contact! Don't submit PRs for extra features, all new features is coming in V2

## About:
The AES Cipher Encryptation was developed by [Wellington Luiz do Nascimento](https://github.com/wellingtonpoll) under the [MIT license](LICENSE).
