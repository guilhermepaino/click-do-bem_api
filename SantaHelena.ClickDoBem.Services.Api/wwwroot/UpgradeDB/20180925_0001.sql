-- ------------------------------------------------------------------------------------------------------------------------------------
-- Estrutura
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela __ControleVersaoUpgradeDB
CREATE TABLE `__ControleVersaoUpgradeDB` (
    `Versao` INT NOT NULL,
	`DtCadastro` datetime NOT NULL,
	`Descricao` varchar(200) NOT NULL,
    CONSTRAINT `PK___ControleVersaoUpgradeDB` PRIMARY KEY (`Versao`)
);

-- Tabela Usuario
CREATE TABLE `Usuario` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `CpfCnpj` varchar(14) NOT NULL,
  `Nome` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Usuario_DtInclusao` (`DataInclusao`),
  KEY `IX_Usuario_DtAlteracao` (`DataAlteracao`),
  KEY `IX_Usuario_Nome` (`Nome`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Tabela UsuarioLogin
CREATE TABLE `UsuarioLogin` (
  `UsuarioId` char(36) NOT NULL,
  `Login` varchar(150) NOT NULL,
  `Senha` char(34) NOT NULL,
  PRIMARY KEY (`UsuarioId`),
  UNIQUE KEY `UK_Login_Login` (`Login`),
  CONSTRAINT `FK_Login_Usuario_Id` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


-- Tabela de UsuarioDados
CREATE TABLE `UsuarioDados` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `UsuarioId` char(36) NOT NULL,
  `DataNascimento` date DEFAULT NULL,
  `Logradouro` varchar(100) DEFAULT NULL,
  `Numero` varchar(30) DEFAULT NULL,
  `Complemento` varchar(50) DEFAULT NULL,
  `Bairro` varchar(80) DEFAULT NULL,
  `Cidade` varchar(100) DEFAULT NULL,
  `UF` char(2) DEFAULT NULL,
  `CEP` char(8) DEFAULT NULL,
  `TelefoneCelular` varchar(20) DEFAULT NULL,
  `TelefoneFixo` varchar(20) DEFAULT NULL,
  `Email` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Usuario_DtInclusao` (`DataInclusao`),
  KEY `IX_Usuario_DtAlteracao` (`DataAlteracao`),
  KEY `FK_UsuarioDados_Usuario_Id` (`UsuarioId`),
  CONSTRAINT `FK_UsuarioDados_Usuario_Id` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Tabela de Categoria
CREATE TABLE `Categoria` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `Descricao` varchar(150) NOT NULL,
  `Pontuacao` int(5) NOT NULL,
  `GerenciadaRh` bit NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_Categoria_DtInclusao` (`DataInclusao`),
  KEY `IX_Categoria_DtAlteracao` (`DataAlteracao`),
  KEY `IX_Categoria_Descricao` (`Descricao`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (1, now(), 'Cria��o inicial do banco. Cria��o da tabela de usu�rios.');

-- Tabelas de Usuario
INSERT INTO Usuario (Id, DataInclusao, CpfCnpj, Nome) VALUES (UUID(), NOW(), '11111111111', 'admin');
INSERT INTO UsuarioLogin (UsuarioId, Login, Senha) SELECT Id, 'admin', MD5('SHelena') FROM Usuario WHERE Nome = 'admin';

-- Tabela de Categoria
INSERT INTO Categoria (Id, DataInclusao, Descricao, Pontuacao, GerenciadaRh) VALUES (UUID(), NOW(), 'Higiene e limpeza', 10, 0);
INSERT INTO Categoria (Id, DataInclusao, Descricao, Pontuacao, GerenciadaRh) VALUES (UUID(), NOW(), 'Beb�', 100, 0);
INSERT INTO Categoria (Id, DataInclusao, Descricao, Pontuacao, GerenciadaRh) VALUES (UUID(), NOW(), 'Telefonia e acess�rios', 10, 1);