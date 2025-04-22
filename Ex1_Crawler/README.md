
# Crawler

The goal of this exercise is to create a simple "crawler", which searches through a website for email addresses.

## Functionality

The program receives a singular argument, which is the URL address of the website that the crawler should search through. A list of unique email addresses, which were found on the website, are to be output to the console.

## Requirements

- If no argument is given, an ArgumentNullException should be thrown.
- If the argument given is not a correct URL address, an ArgumentException should be thrown.
- If there was an error while loading the page, an Exception should be thrown with the appropriate information.
- If no email addresses were found, an Exception should be thrown with the appropriate information.