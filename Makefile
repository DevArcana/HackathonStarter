publish: clean backend frontend
	dotnet publish src/WebApi/WebApi.csproj -c Release -o out --no-restore --no-build --sc
	mv src/Client/dist/* out/wwwroot/

clean:
	rm -rf out
	rm -rf src/Client/dist

backend:
	dotnet restore
	dotnet build
	dotnet test

frontend:
	cd src/Client; pnpm build

