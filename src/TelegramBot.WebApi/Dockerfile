FROM microsoft/aspnetcore:1.0.1
ENTRYPOINT ["dotnet", "TelegramBot.WebApi.dll"]
EXPOSE 5001
WORKDIR /app
COPY . /app
RUN ["mkdir", "/data/school-bot"]
ENV ASPNETCORE_ENVIRONMENT Production