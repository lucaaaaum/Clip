{ pkgs ? import <nixpkgs> {} }:
    pkgs.mkShell {
        nativeBuildInputs = with pkgs.buildPackages; [
            dotnet-sdk_8
            dotnet-runtime_8
            dotnet-aspnetcore_8
        ];
    }
