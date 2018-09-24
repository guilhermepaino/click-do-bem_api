-- Criar login:
create login shclickappuser with password = 'SH-Cl1ck.D0.B3m';

-- Criar Usuário no servidor
create user shclickappuser for login shclickappuser;

-- Criar Base de dados
create database clickdobem;
alter database clickdobem collate latin1_general_ci_ai;

-- Habilitar snapshot no banco
use master;
alter database clickdobem set single_user;
alter database clickdobem set allow_snapshot_isolation on;
alter database clickdobem set read_committed_snapshot on;
alter database clickdobem set multi_user;

-- Criar  usuário no banco
use clickdobem;
create user shclickappuser for login shclickappuser with default_schema=dbo;

-- Colocar usuario como owner do banco
use clickdobem;
alter role db_owner add member shclickappuser

/*
-- Exclusoes
use master;
alter database clickdobem set single_user;
drop database clickdobem;
drop login shclickappuser;
*/