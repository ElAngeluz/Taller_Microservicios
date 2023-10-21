FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf
WORKDIR /app
EXPOSE 8080
COPY . .

ENTRYPOINT ["dotnet", "api.dll"]