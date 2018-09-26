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
  `Nome` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Usuario_DtInclusao` (`DataInclusao`),
  KEY `IX_Usuario_DtAlteracao` (`DataAlteracao`),
  KEY `IX_Usuario_Nome` (`Nome`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Tabela UsuarioSenha
CREATE TABLE `UsuarioSenha` (
  `UsuarioId` char(36) NOT NULL,
  `Senha` char(34) NOT NULL,
  KEY `FK_Senha_Usuario_Id` (`UsuarioId`),
  CONSTRAINT `FK_Senha_Usuario_Id` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (1, now(), 'Criação inicial do banco. Criação da tabela de usuários.');

-- Tabelas de Usuario
INSERT INTO Usuario (Id, DataInclusao, Nome) VALUES (UUID(), NOW(), 'admin');
INSERT INTO UsuarioSenha (UsuarioId, Senha) SELECT Id, MD5('SHelena') FROM Usuario;	