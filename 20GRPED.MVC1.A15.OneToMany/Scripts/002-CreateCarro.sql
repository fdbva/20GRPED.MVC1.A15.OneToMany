CREATE TABLE Carro(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Modelo VARCHAR(250) NOT NULL,
	PessoaId INT NOT NULL FOREIGN KEY REFERENCES Pessoa(Id)
);

SELECT * FROM Carro;

--DROP TABLE Carro;

INSERT INTO Carro 
	(Modelo, PessoaId) VALUES
	('Honda Fit', 1);
INSERT INTO Carro 
	(Modelo, PessoaId) VALUES
	('Toyota Etios', 1);