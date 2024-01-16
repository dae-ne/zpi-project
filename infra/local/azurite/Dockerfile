FROM mcr.microsoft.com/azure-storage/azurite
ENTRYPOINT ["azurite-blob", "--blobHost", "0.0.0.0", "--blobPort", "10000", "--location", "/workspace", "--debug", "/workspace/debug.log"]
EXPOSE 10000
