FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /BackEnd/app/

# Copy your application files (csproj and source code)
COPY *.csproj ./
COPY . .
# Restore dependencies and publish for deployment
RUN dotnet restore ./BackEnd/BackEnd.csproj && dotnet publish ./BackEnd/BackEnd.csproj -c Release -o /app/publish


# Stage 2: Final image (slim runtime)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /BackEnd/app/

# Copy the published output from the previous stage
COPY --from=build /app/publish .

# Expose port 80 to make the application accessible
EXPOSE 80

# Command to run the application when the container starts
CMD ["dotnet", "BackEnd.dll"]
