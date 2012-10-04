InsertFromCSVGenerator
======================

a small program that generates insert statements from a CSV file.

How it works
======================

It uses the filename and places it as the name of the table to be inserted to. It reads the first row as the fieldnames and places it insert fieldnames. The rows other than the first are then placed as the values part.

script generation structure:

insert into {filename} ( {columns of the first row} ) values ( {columns of the next row} ), ( {columns of the next row} ), ( {columns of the next row} ), ( {columns of the next row} ), ( {columns of the next row} )