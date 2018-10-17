-- ------------------------------------------------------------------------------------------------------------------------------------
-- Estrutura
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela ItemImagem
CREATE TABLE `ItemMatch` (
  `Id` char(36) NOT NULL,
  `Data` datetime NOT NULL,
  `UsuarioId` char(36) NOT NULL,
  `NecessidadeId` char(36) NOT NULL,
  `DoacaoId` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_ItemMatch_ND` (`NecessidadeId`,`DoacaoId`),
  KEY `IX_ItemMatch_Data` (`Data`),
  KEY `IX_ItemMatch_Usuario` (`UsuarioId`),
  KEY `IX_ItemMatch_Doacao` (`DoacaoId`),
  KEY `IX_ItemMatch_Necessidade` (`NecessidadeId`),
  CONSTRAINT `FK_ItemMatch_Doacao` FOREIGN KEY (`DoacaoId`) REFERENCES `Item` (`Id`),
  CONSTRAINT `FK_ItemMatch_Necessidade` FOREIGN KEY (`NecessidadeId`) REFERENCES `Item` (`Id`),
  CONSTRAINT `FK_ItemMatch_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (2, now(), 'Criação de mecanismo de Match.');