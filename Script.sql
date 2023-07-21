CREATE DATABASE CRUD
GO

USE CRUD
GO

CREATE TABLE UF
(
	Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_UF PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL,
	Sigla CHAR(2) NOT NULL,
)

INSERT INTO UF (Nome, Sigla) VALUES ('Acre', 'AC')
INSERT INTO UF (Nome, Sigla) VALUES ('Alagoas', 'AL')
INSERT INTO UF (Nome, Sigla) VALUES ('Amapá', 'AP')
INSERT INTO UF (Nome, Sigla) VALUES ('Amazonas', 'AM')
INSERT INTO UF (Nome, Sigla) VALUES ('Bahia', 'BA')
INSERT INTO UF (Nome, Sigla) VALUES ('Ceará', 'CE')
INSERT INTO UF (Nome, Sigla) VALUES ('Distrito Federal', 'DF')
INSERT INTO UF (Nome, Sigla) VALUES ('Espírito Santo', 'ES')
INSERT INTO UF (Nome, Sigla) VALUES ('Goiás', 'GO')
INSERT INTO UF (Nome, Sigla) VALUES ('Maranhão', 'MA')
INSERT INTO UF (Nome, Sigla) VALUES ('Mato Grosso', 'MT')
INSERT INTO UF (Nome, Sigla) VALUES ('Mato Grosso do Sul', 'MS')
INSERT INTO UF (Nome, Sigla) VALUES ('Minas Gerais', 'MG')
INSERT INTO UF (Nome, Sigla) VALUES ('Pará', 'PA')
INSERT INTO UF (Nome, Sigla) VALUES ('Paraíba', 'PB')
INSERT INTO UF (Nome, Sigla) VALUES ('Paraná', 'PR')
INSERT INTO UF (Nome, Sigla) VALUES ('Pernambuco', 'PE')
INSERT INTO UF (Nome, Sigla) VALUES ('Piauí', 'PI')
INSERT INTO UF (Nome, Sigla) VALUES ('Rio de Janeiro', 'RJ')
INSERT INTO UF (Nome, Sigla) VALUES ('Rio Grande do Norte', 'RN')
INSERT INTO UF (Nome, Sigla) VALUES ('Rio Grande do Sul', 'RS')
INSERT INTO UF (Nome, Sigla) VALUES ('Rondônia', 'RO')
INSERT INTO UF (Nome, Sigla) VALUES ('Roraima', 'RR')
INSERT INTO UF (Nome, Sigla) VALUES ('Santa Catarina', 'SC')
INSERT INTO UF (Nome, Sigla) VALUES ('São Paulo', 'SP')
INSERT INTO UF (Nome, Sigla) VALUES ('Sergipe', 'SE')
INSERT INTO UF (Nome, Sigla) VALUES ('Tocantins', 'TO')

CREATE TABLE Cliente
(
	Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Cliente PRIMARY KEY,
	Cpf CHAR(14) NOT NULL,
	Nome VARCHAR(100) NOT NULL,
	Rg CHAR(12),
	DataExpedicao DATETIME,
	OrgaoExpedicao VARCHAR(100),
	UfExpedicaoId INT CONSTRAINT FK_ClienteUF FOREIGN KEY REFERENCES UF(Id),
	DataNascimento DATETIME NOT NULL,
	Sexo CHAR(1) NOT NULL,
	EstadoCivil VARCHAR(20) NOT NULL
)

CREATE TABLE Endereco
(
	Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Endereco PRIMARY KEY,
	ClienteId INT NOT NULL CONSTRAINT FK_EnderecoCliente FOREIGN KEY REFERENCES Cliente(Id),
	Cep CHAR(9) NOT NULL,
	Logradouro VARCHAR(100) NOT NULL,
	Numero VARCHAR(10) NOT NULL,
	Complemento VARCHAR(100),
	Bairro VARCHAR(100) NOT NULL,
	Cidade VARCHAR(100) NOT NULL,
	UfId INT CONSTRAINT FK_EnderecoUF FOREIGN KEY REFERENCES UF(Id)
)