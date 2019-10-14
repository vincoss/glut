## Resources
https://hub.docker.com/_/microsoft-dotnet-core-sdk/
https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
https://docs.microsoft.com/en-us/virtualization/windowscontainers/manage-docker/manage-windows-dockerfile

## Build
docker build --no-cache -t vincoss/glutsvr:1.0.0 .

## Tag image (before publish to docker hub)
docker image tag vincoss/glutsvr:1.0.0 vincoss/glutsvr:1.0.1

## Push to docker hub
docker image push vincoss/glutsvr:1.0.1

## Run
docker run -it --rm -p 8000:80 --name glut -h glut -v glutData:C:/Glut/Data fl/glut:latest
docker container run -it --rm -p 8000:80 --name glut -h glut -v glutData:C:/Glut/Data fl/glut:latest

## Browse
http://glut