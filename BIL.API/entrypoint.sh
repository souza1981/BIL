#!/bin/bash

set -e
run_cmd="dotnet run --server.urls http://*:80"

until dotnet ef database update; do
echo "SQL Server is starting up"
sleep 1
done

echo "SQL Server is up - executing command"
exec $run_cmd