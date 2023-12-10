#!/bin/bash

echo "Waiting for postgres to come online..."

for i in {1..100}; do ./efbundle && break || sleep 5; done

if [ $? -ne 0 ]; then
  echo "Failed to connect to postgres."
  exit 1
fi

dotnet ./Recipes.WebApi.dll
