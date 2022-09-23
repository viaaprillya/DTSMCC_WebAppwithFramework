CREATE DATABASE DTSMCC02;

CREATE TABLE Divisions (
Id int PRIMARY KEY,
Name varchar(30)
)

CREATE TABLE Department (
Id int PRIMARY KEY,
Name varchar(30),
Division_Id int,
CONSTRAINT FK_DeptDiv FOREIGN KEY (Division_Id) REFERENCES Divisions(Id)
)

INSERT INTO Divisions VALUES (1,'Technical Division'),(2, 'Non-Technical Division')
INSERT INTO Department VALUES (1, 'Department A', 1),(2, 'Department B', 2),(3, 'Department C', 2)