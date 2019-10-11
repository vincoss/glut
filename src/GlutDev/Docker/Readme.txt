## Resources
https://hub.docker.com/_/microsoft-dotnet-core-sdk/
https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
https://docs.microsoft.com/en-us/virtualization/windowscontainers/manage-docker/manage-windows-dockerfile

## Build
docker build --no-cache -t fl/glut:latest .

## Run
docker run -it --rm -p 8000:80 --name glut -h glut -v glutData:C:/Glut/Data fl/glut:latest

## Browse
http://glut