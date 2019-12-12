## Resources
https://hub.docker.com/_/microsoft-dotnet-core-sdk/
https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
https://docs.microsoft.com/en-us/virtualization/windowscontainers/manage-docker/manage-windows-dockerfile

## Build
docker build --no-cache -t vincoss/glutsvr:1.0.0 .

## Tag image (before publish to docker hub)
docker image tag vincoss/glutsvr:1.0.0 vincoss/glutsvr:1.0.0

## Push to docker hub
docker image push vincoss/glutsvr:1.0.1

## Run
docker run -it --rm -p 8000:80 --name glutsvr -h glut --ip 10.1.2.3 -v glutData:C:/Glut/Data vincoss/glutsvr:1.0.0

## Error logs
docker logs --tail 50 --follow --timestamps glutsvr

## Browse
http://glut:8000/
http://localhost:8000/

##------------------------------------------------ Test

# grab image
docker pull vincoss/glutsvr:1.0.0

# run
docker run -it --rm -p 8000:80 --name glutsvr -h glut --ip 10.1.2.3 -v glutData:C:/Glut/Data vincoss/glutsvr:1.0.0

#Browse
http://glut