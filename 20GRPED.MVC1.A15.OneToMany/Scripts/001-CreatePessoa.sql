CREATE TABLE Pessoa(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(250)
);

INSERT INTO Pessoa 
	(Nome) 
	OUTPUT INSERTED.Id
	VALUES 
	('Felipe Barcelos');
INSERT INTO Pessoa (Nome) VALUES ('Felipe Damião');

SELECT * FROM Pessoa;

--DROP TABLE Pessoa;
DELETE FROM Pessoa WHERE Id > 0;