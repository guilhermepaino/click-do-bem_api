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
  UNIQUE KEY `UK_Usuario_CpfCnpj` (`CpfCnpj`),
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

-- Tabela de UsuarioPerfil
CREATE TABLE `UsuarioPerfil` (
  `UsuarioId` char(36) NOT NULL,
  `Perfil` varchar(50) NOT NULL,
  PRIMARY KEY (`UsuarioId`, `Perfil`),
  CONSTRAINT `FK_UP_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
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

-- Tabela de DocumentoHabilitado
CREATE TABLE `DocumentoHabilitado` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `CpfCnpj` varchar(14) NOT NULL,
  `Ativo` bit NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_DH_CpfCnpj` (`CpfCnpj`),
  KEY `IX_DH_DtInclusao` (`DataInclusao`),
  KEY `IX_DH_DtAlteracao` (`DataAlteracao`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Tabela TipoItem
CREATE TABLE `TipoItem` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `Descricao` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_TipoItem_Descricao` (`Descricao`),
  KEY `IX_TipoItem_DtInclusao` (`DataInclusao`),
  KEY `IX_TipoItem_DtAlteracao` (`DataAlteracao`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Tabela Item
CREATE TABLE `Item` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `Sku` int(11) NOT NULL AUTO_INCREMENT,
  `Titulo` varchar(50) NOT NULL,
  `Descricao` varchar(1000) DEFAULT NULL,
  `TipoItemId` char(36) NOT NULL,
  `CategoriaId` char(36) NOT NULL,
  `UsuarioId` char(36) NOT NULL,
  `Anonimo` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_Item_Sku` (`Sku`),
  KEY `IX_Item_DtInclusao` (`DataInclusao`),
  KEY `IX_Item_DtAlteracao` (`DataAlteracao`),
  KEY `IX_Item_Descricao` (`Descricao`),
  KEY `IX_Item_TipoItemId` (`TipoItemId`),
  KEY `IX_Item_CategoriaId` (`CategoriaId`),
  KEY `IX_Item_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Item_Categoria` FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`),
  CONSTRAINT `FK_Item_TipoItem` FOREIGN KEY (`TipoItemId`) REFERENCES `TipoItem` (`Id`),
  CONSTRAINT `FK_Item_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Tabela ItemImagem
CREATE TABLE `ItemImagem` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `ItemId` char(36) NOT NULL,
  `NomeOriginal` varchar(50) NOT NULL,
  `Caminho` varchar(2000) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ItemImagem_DtInclusao` (`DataInclusao`),
  KEY `IX_ItemImagem_DtAlteracao` (`DataAlteracao`),
  KEY `IX_ItemImagem_ItemId` (`ItemId`),
  CONSTRAINT `FK_ItemImagem_Item` FOREIGN KEY (`ItemId`) REFERENCES `Item` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (1, now(), 'Criação inicial do banco.');

-- Tabelas de Usuario
INSERT INTO Usuario (Id, DataInclusao, CpfCnpj, Nome) VALUES ('f763727d-c426-11e8-a7a4-0242ac110006', NOW(), '11111111111', 'admin');
INSERT INTO UsuarioLogin (UsuarioId, Login, Senha) VALUES ('f763727d-c426-11e8-a7a4-0242ac110006', 'admin', MD5('SHelena'));

-- Tabela de UsuarioPerfil
INSERT INTO UsuarioPerfil (`UsuarioId`, `Perfil`) VALUES ('f763727d-c426-11e8-a7a4-0242ac110006', 'Admin');

-- Tabela de Categoria
INSERT INTO Categoria (Id, DataInclusao, Descricao, Pontuacao, GerenciadaRh) VALUES ('2ef307a6-c4a5-11e8-8776-0242ac110006', NOW(), 'Higiene e limpeza', 10, 0);
INSERT INTO Categoria (Id, DataInclusao, Descricao, Pontuacao, GerenciadaRh) VALUES ('340c1a33-c4a5-11e8-8776-0242ac110006', NOW(), 'Bebê', 100, 0);
INSERT INTO Categoria (Id, DataInclusao, Descricao, Pontuacao, GerenciadaRh) VALUES ('38f292cc-c4a5-11e8-8776-0242ac110006', NOW(), 'Telefonia e acessórios', 10, 1);

-- Tabela de TipoItem
INSERT INTO TipoItem (`Id`, `DataInclusao`, `Descricao`) VALUES ('0acd2b81-c5a5-11e8-ab80-0242ac110006', NOW(), 'Necessidade');
INSERT INTO TipoItem (`Id`, `DataInclusao`, `Descricao`) VALUES ('0acd2bb5-c5a5-11e8-ab80-0242ac110006', NOW(), 'Doação');