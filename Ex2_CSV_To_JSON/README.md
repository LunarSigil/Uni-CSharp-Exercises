
# CSV to JSON

The goal of this exercise is to create a simple console application that will convert data, imported from a CSV file, into JSON format.

## Functionality

The program receives two arguments: the CSV file path and the destination file path. The output file must already exist in the given destination file path.

Additionally, it is known beforehand that each record, representing a student, in the CSV file is described by 9 columns separated with commas. The columns represent the following data:
- First name
- Last name
- Field of study
- Mode of study
- Student index
- Date of birth
- Email
- Mother's name
- Father's name

The timestamp of export, the author (can be hardcoded), the list of students from the CSV file and a list of fields of studies, in which students are currently studying, must be written into the file indicated by the second argument received by the program. Moreover, any errors should be saved into a log file.

## Requirements

The CSV file can contain errors:
- If a student isn't described by 9 different values, this is an error and must be logged into the log file.
- If one or more of the values describing a student is empty, this is an error and must be logged into the log file.
- If there are duplicates (meaning the first and last name, index as well as the field of studies is the same), the copy should be ignored and raised as an error in the log file.

Should a duplicate of a student appear, but with a different field of study, that field of study should be added to the student's list of studies in JSON format.

## Additional notes

A sample CSV file has been added to the project, along with an already filled out log and output file for the sample data.