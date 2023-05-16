#!/bin/bash
set -e

# Wait for SQL Server to start
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P o0b3lr70 -Q "SELECT 1;" &> /dev/null
echo "SQL Server is up and running"

# Apply database migrations
dotnet ef database update

# Start the application
exec "$@"