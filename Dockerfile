# base image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime

ARG HTTP_PORT=80
ARG GLUT_HOME=/inetpub/wwwroot

WORKDIR ${GLUT_HOME}
COPY ["/src/GlutSvrWeb/bin/Release/netcoreapp3.0/publish/.", "./"]

ENV HOME ${GLUT_HOME}

# for main web interface:
EXPOSE ${HTTP_PORT}

VOLUME C:/Glut/Data

CMD ["dotnet", "GlutSvr.dll"]