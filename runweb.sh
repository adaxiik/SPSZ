#!/bin/sh
set -xe
cp SPSZdatabase.db SPSZui/.
cp SPSZdatabase.db SPSZdatabase.db.backup
cp -r CsvDatabases SPSZui/.
cp -r CsvDatabases CsvDatabases.backup

dotnet watch run --project SPSZui

rm SPSZdatabase.db
cp SPSZui/SPSZdatabase.db SPSZdatabase.db
rm -r CsvDatabases
cp -r SPSZui/CsvDatabases CsvDatabases